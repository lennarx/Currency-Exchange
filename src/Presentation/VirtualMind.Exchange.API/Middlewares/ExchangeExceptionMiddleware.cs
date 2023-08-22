using Microsoft.AspNetCore.Diagnostics;
using VirtualMind.Exchange.API.Exceptions;
using VirtualMind.Exchange.Application.Services.Exceptions;
using static System.Net.Mime.MediaTypeNames;

namespace VirtualMind.Exchange.API.Middlewares
{
    public static class ExchangeExceptionMiddleware
    {
        public static void UseExchangeExceptionMiddleware(this IApplicationBuilder app, ILoggerFactory loggerFactory) 
        {
            app.UseExceptionHandler(exceptionHadlerApp =>
            {
                exceptionHadlerApp.Run(async context =>
                {
                    context.Response.ContentType = Text.Plain;

                    var exceptionHandlerPathFeatre = context.Features.Get<IExceptionHandlerPathFeature>();

                    var exception = exceptionHandlerPathFeatre?.Error;

                    var logger = loggerFactory.CreateLogger(string.Empty);
                    logger.LogError(exception, exception.Message);

                    switch (exception!.GetType().Name)
                    {
                        case nameof(InvalidISOCodeException):
                            await WriteBadRequestResponse(context, exception);
                            break;
                        case nameof(NoCurrencyRateFoundException):
                            await WriteNotFoundResponse(context, exception);
                            break;
                        case nameof(CurrencyPurchaseLimitExceededException):
                            await WriteBadRequestResponse(context, exception);
                            break;
                        default:
                            await WriteInternalErrorMessage(context, exception);
                            break;
                    }
                });
            });
        }

        private static async Task WriteBadRequestResponse(HttpContext context, Exception exception)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            await context.Response.WriteAsync(exception.Message);
        }

        private static async Task WriteNotFoundResponse(HttpContext context, Exception exception)
        {
            context.Response.StatusCode = StatusCodes.Status404NotFound;
            await context.Response.WriteAsync(exception.Message);
        }

        private static async Task WriteInternalErrorMessage(HttpContext context, Exception exception)
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await context.Response.WriteAsync("An exception was thrown. ");
            await context.Response.WriteAsync(string.Concat(exception.InnerException, exception.Message));
        }
    }
}
