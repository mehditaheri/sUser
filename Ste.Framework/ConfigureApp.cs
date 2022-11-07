using System.Text.Json;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Ste.Framework.Common;

namespace Ste.Framework;

public static class ConfigureApp
{
    public static void Init(IApplicationBuilder app)
    {
        app.UseExceptionHandler(a => a.Run(async context =>
        {
            Result? result;
            var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
            if (exceptionHandlerPathFeature?.Error is ValidationException fluentValidation)
            {
                result = new UnValidatedModel(fluentValidation.Errors);
            }
            else
            {
                var exception = exceptionHandlerPathFeature?.Error;
                result = new BadRequest { Message = exception?.Message };
            }
            
            context.Response.StatusCode = 200;
            context.Response.ContentType = "application/json; charset=utf-8";
            await context.Response.WriteAsync(JsonSerializer.Serialize(result));
        }));
    }
}
