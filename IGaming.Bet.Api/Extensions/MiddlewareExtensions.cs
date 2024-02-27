using Microsoft.AspNetCore.Diagnostics;
using Shared.ErrorModel;
using Shared.Exceptions;

namespace IGaming.Bet.Solution.Extensions;

public static class MiddlewareExtensions
{
    public static void ConfigureExceptionHandler(this WebApplication app)
    {
        app.UseExceptionHandler(appBuilder =>
        {
            appBuilder.Run(async context =>
            {
                context.Response.ContentType = "application/json";

                var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                if (contextFeature is null) return;

                context.Response.StatusCode = contextFeature.Error switch
                {
                    NotFoundException => StatusCodes.Status404NotFound,
                    BadRequestException => StatusCodes.Status400BadRequest,
                    UnauthorizedAccessException => StatusCodes.Status401Unauthorized,
                    _ => StatusCodes.Status500InternalServerError
                };

                var errorDetail = new ErrorDetail
                    { StatusCode = context.Response.StatusCode, Message = contextFeature.Error.Message };

                if (context.Response.StatusCode == StatusCodes.Status500InternalServerError)
                    errorDetail.Message = "INTERNAL_ERROR";

                await context.Response.WriteAsync(errorDetail.ToString());
            });
        });
    }
}