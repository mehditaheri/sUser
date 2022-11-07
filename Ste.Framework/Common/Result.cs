using FluentValidation.Results;

namespace Ste.Framework.Common;

public class ValidationError
{
    public string? PropertyName { get; set; }

    public string? ErrorMessage { get; set; }
}
public interface IResult
{
    public bool Success { get; set; }
    public string? StatusCode { get; set; }
    public string? Message { get; set; }

    public List<ValidationError>? Errors { get; set; }
}

public class Result : IResult
{
    public bool Success { get; set; }
    public string? StatusCode { get; set; }
    public string? Message { get; set; }
    public List<ValidationError>? Errors { get; set; }
}

public class Result<T> : IResult
{
    public T? Data { get; set; }
    public bool Success { get; set; }
    public string? StatusCode { get; set; }
    public string? Message { get; set; }
    public List<ValidationError>? Errors { get; set; }
}
public class Success<T> : Result<T>
{
    public Success()
    {
        Success = true;
        Message = "عملیات با موفقیت انجام شد";
        StatusCode = "200";
    }
}
public class Success : Result
{
    public Success()
    {
        Success = true;
        Message = "عملیات با موفقیت انجام شد";
        StatusCode = "200";
    }
}
 

public class UnValidatedModel : Result 
{
    public UnValidatedModel(IEnumerable<ValidationFailure> validationFailure)
    {
        Success = false;
        Errors = validationFailure.Select(p => new ValidationError()
        {
            PropertyName = p.PropertyName,
            ErrorMessage = p.ErrorMessage,
        }).ToList();
        Message =  "مقادیر ورودی معتبر نمی باشد";
        StatusCode = "400";
    }
}

public class HasError<T> : Result<T>
{
    public HasError(ValidationResult validationResult)
    {
        Success = validationResult.IsValid;
        Errors = validationResult.Errors.Where(validationFailure => validationFailure != null).Select(p => new ValidationError()
        {
            PropertyName = p.PropertyName,
            ErrorMessage = p.ErrorMessage,
        }).ToList();
        Message = validationResult.IsValid ? null : "خطا در انجام عملیات";
        StatusCode = "400";
    }
}
public class HasError : Result
{
    public HasError(ValidationResult validationResult)
    {
        Success = validationResult.IsValid;
        Errors = validationResult.Errors.Where(validationFailure => validationFailure != null).Select(p => new ValidationError()
        {
            PropertyName = p.PropertyName,
            ErrorMessage = p.ErrorMessage,
        }).ToList();
        Message = validationResult.IsValid ? null : "خطا در انجام عملیات";
        StatusCode = "400";
    }
}
public class BadRequest<T> : Result<T>
{
    public BadRequest()
    {
        Success = false;
        StatusCode = "400";
    }

    public BadRequest(ValidationResult validationResult)
    {
        Success = validationResult.IsValid;
        Errors = validationResult.Errors.Where(validationFailure => validationFailure != null).Select(p => new ValidationError()
        {
            PropertyName = p.PropertyName,
            ErrorMessage = p.ErrorMessage,
        }).ToList();
        Message = validationResult.IsValid ? null : "خطا در انجام عملیات";
        StatusCode = "400";
    }
}
public class BadRequest : Result
{
    public BadRequest()
    {
        Success = false;
        StatusCode = "400";
    }

    public BadRequest(ValidationResult validationResult)
    {
        Success = validationResult.IsValid;
        Errors = validationResult.Errors.Where(validationFailure => validationFailure != null).Select(p => new ValidationError()
        {
            PropertyName = p.PropertyName,
            ErrorMessage = p.ErrorMessage,
        }).ToList();
        Message = validationResult.IsValid ? null : "خطا در انجام عملیات";
        StatusCode = "400";
    }
}
public class ResponseNotFound<T> : Result<T>
{
    public ResponseNotFound()
    {
        Success = false;
        Message = "رکوردی با این مشخصات یافت نشد";
        StatusCode = "404";
    }
}
public class ResponseNotFound : Result
{
    public ResponseNotFound()
    {
        Success = false;
        Message = "رکوردی با این مشخصات یافت نشد";
        StatusCode = "404";
    }
}