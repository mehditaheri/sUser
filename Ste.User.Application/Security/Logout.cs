using MediatR;
using Ste.Framework.Common;
using Ste.User.Domain.Interface;

namespace Ste.User.Application.Security
{

    public class Logout
    {
        public record Request : IRequest<Result>
        {
            public string RefreshToken { get; set; }
        }

        public class Handler : IRequestHandler<Request, Result>
        {
            private ISecurityPersist _securityPersist;
            private IUserContextService _userContextService;


            public Handler( IUserContextService userContextService, ISecurityPersist securityPersist)
            {
                _userContextService = userContextService;
                _securityPersist = securityPersist;
            }

            public async Task<Result> Handle(Request request, CancellationToken cancellationToken)
            {
                var token = await _userContextService.GetToken();
                var refreshToken = SymmetricEncryption.Decrypt(request.RefreshToken, "TokenAes");
                return new Result
                {
                    Success = true
                };
            }
        }
    }
}
