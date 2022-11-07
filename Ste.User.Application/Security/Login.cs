using FluentValidation;
using Mapster;
using MediatR;
using Ste.Framework.Common;
using Ste.User.Domain;
using Ste.User.Domain.Interface;

namespace Ste.User.Application.Security
{

    public class Login
    {
        public record Request : IRequest<Result<Token>>
        {
            public string Username { get; set; }
            public string Password { get; set; }
            public string CaptchaKey { get; set; }
            public string CaptchaValue { get; set; }
        }

        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(v => v.Username).NotEmpty().WithMessage("نام کاربری نباید خالی باشد");
                RuleFor(v => v.Password).NotEmpty().WithMessage("کلمه عبور نباید خالی باشد");
            }
        }

        public class Handler : IRequestHandler<Request, Result<Token>>
        {
            private IUserPersist _userPersist;
            private ISecurityPersist _securityPersist;
            private ICaptchaService _captchaService;

            public Handler(ICaptchaService captchaService, IUserPersist userPersist, ISecurityPersist securityPersist)
            {
                _captchaService = captchaService;
                _userPersist = userPersist;
                _securityPersist = securityPersist;
            }

            public async Task<Result<Token>> Handle(Request request, CancellationToken cancellationToken)
            {
                UserInformation? tempUser = null;
                UserInformation? user;
                if (request.Username.StartsWith("user-"))
                {
                    tempUser = await _userPersist.GetUserInformation(request.Username.Replace("user-", ""));
                    user = await _userPersist.GetUserInformation("user");
                }
                else
                {
                    user = await _userPersist.GetUserInformation(request.Username);
                }
                if (user == null)
                {
                    //await LogInfoService.AddAsyncN(new LogInfoN
                    //{
                    //    UserId = null,
                    //    LogModuleId = (int)PartOfAppEnum.UserAccount,
                    //    LogActionId = !checkPassword ? (int)ActionEnum.LoginFromGov : (int)ActionEnum.Login,
                    //    LogResponseId = (int)ResponseEnum.UsernameIsIncorrect,
                    //    LogPriorityId = (int)LogPriorityEnum.High,
                    //    LogTypeId = (int)LogTypeEnum.Critical,
                    //    AdditionalData = PortalUtility.ObjectToJson(new { Username = request.Username }),
                    //});
                    return new Result<Token>
                    {
                        Message = "UsernameIsIncorrect",
                        Success = false,
                    };
                }

                if (user.Status != 1)
                {
                    //await LogInfoService.AddAsyncN(new LogInfoN
                    //{//2
                    //    UserId = user.UserId,
                    //    LogModuleId = (int)PartOfAppEnum.UserAccount,
                    //    LogActionId = !checkPassword ? (int)ActionEnum.LoginFromGov : (int)ActionEnum.Login,
                    //    LogResponseId = (int)ResponseEnum.AccountIsDisabled,
                    //    LogPriorityId = (int)LogPriorityEnum.High,
                    //    LogTypeId = (int)LogTypeEnum.Error,
                    //    AdditionalData = PortalUtility.ObjectToJson(new { Username = request.Username, UserStatusId = user.UserStatusId }),
                    //});
                    return new Result<Token>
                    {
                        Message = "AccountIsDisabled",
                        Success = false
                    };
                }

                if (user.UnsuccessfulLoginCount >= 4)
                {
                    if (!(await _captchaService.IsValid(new CheckCaptcha { Key = request.CaptchaKey, Value = request.CaptchaValue })).Data)
                        return new Result<Token>
                        {
                                Success = false,
                                Message = "CaptchaIsNotCorrect",
                        };
                }

                if (user.UnsuccessfulLoginCount >= 8)
                {
                    //await LogInfoService.AddAsyncN(new LogInfoN
                    //{//3
                    //    UserId = user.UserId,
                    //    LogModuleId = (int)PartOfAppEnum.UserAccount,
                    //    LogActionId = !checkPassword ? (int)ActionEnum.LoginFromGov : (int)ActionEnum.Login,
                    //    LogResponseId = (int)ResponseEnum.AccountIsLocked_BecausePasswordRepeatedlyWrong,
                    //    LogPriorityId = (int)LogPriorityEnum.High,
                    //    LogTypeId = (int)LogTypeEnum.Error,
                    //    AdditionalData = PortalUtility.ObjectToJson(new { Username = request.Username, UnsuccessfulLoginCount = user.UnsuccessfulLoginCount }),
                    //});

                    return new Result<Token>
                    {
                        Message = "AccountIsLocked_BecausePasswordRepeatedlyWrong",
                        Success = false,
                    };
                }

                if (Utility.VerifyHashedPassword(user.Password, request.Password))
                {
                    if (tempUser != null)
                        user = tempUser;

                    //if (
                    //    (user.UserLevelId == 4 ||
                    //    user.UserLevelId == 5 ||
                    //    user.UserLevelId == 6 ||
                    //    user.UserLevelId == 10)
                    //    && PortalUtility.IsValidNationalCode(user.NationalCode.Value.ToString("0000000000")) && await BlackListPersist.GetExistInBlackList(PortalUtility.GetCode(user.NationalCode + "")))
                    //{
                    //    return new Result<Token>
                    //    {
                    //        Response = new ServiceResponse
                    //        {
                    //            Success = true,
                    //            Message = "فعالیت تبلیغی شما از مرکز غیر فعال شده است",
                    //            ErrorCode = 5007
                    //        }
                    //    };
                    //}

                    var accessToken = new AccessToken
                    {
                        CreateAt = DateTime.Now,
                        ExpireAt = DateTime.Now.AddMinutes(60),
                        Ip = "127.0.0.1",
                        LoginType = LoginTypeSet.Internal,
                        User = user.Adapt<TokenUserInformation>()
                    };

                    //if (user.UnsuccessfulLoginCount > 2)
                    //    await UserPersist.SetUnsuccessfulLoginCount(user.UserId, 0);

                    //await LogInfoService.AddAsyncN(new LogInfoN
                    //{//4
                    //    UserId = user.UserId,
                    //    LogModuleId = (int)PartOfAppEnum.UserAccount,
                    //    LogActionId = !checkPassword ? (int)ActionEnum.LoginFromGov : (int)ActionEnum.Login,
                    //    LogResponseId = (int)ResponseEnum.TheOperationWasSuccess,
                    //    LogPriorityId = (int)LogPriorityEnum.Medium,
                    //    LogTypeId = (int)LogTypeEnum.Info,
                    //    AdditionalData = PortalUtility.ObjectToJson(new { Username = request.Username }),
                    //});

                    return new Result<Token>
                    {
                        Data = await _securityPersist.CreateToken(accessToken),
                        Success = true
                    };
                }

                //await _securityPersist.SetUnsuccessfulLoginCount(user.UserId, (byte)((user.UnsuccessfulLoginCount ?? 0) + 1));

                //await LogInfoService.AddAsyncN(new LogInfoN
                //{//5
                //    UserId = user.UserId,
                //    LogModuleId = (int)PartOfAppEnum.UserAccount,
                //    LogActionId = !checkPassword ? (int)ActionEnum.LoginFromGov : (int)ActionEnum.Login,
                //    LogResponseId = (int)ResponseEnum.UsernameOrPasswordIsIncorrect,
                //    LogPriorityId = (int)LogPriorityEnum.Medium,
                //    LogTypeId = (int)LogTypeEnum.Warning,
                //    AdditionalData = null,
                //});
                return new Result<Token>
                {
                        Message = "UsernameOrPasswordIsIncorrect",
                        Success = false,
                };
            }
        }
    }
}
