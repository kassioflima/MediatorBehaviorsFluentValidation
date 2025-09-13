using FluentValidation;
using MediatorBehaviorsFluentValidation.Domain.Responses;
using Microsoft.AspNetCore.Diagnostics;
using System.Text;
using System.Text.Json;

namespace MediatorBehaviorsFluentValidation.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static void UseFluentValidationExceptionHalder(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(x =>
            {
                x.Run(async context =>
                {
                    var errorFeature = context.Features.Get<IExceptionHandlerFeature>();
                    var exception = errorFeature?.Error;

                    if (exception is ValidationException validationException)
                    {
                        // Retorna lista simples de erros para validação
                        var validationErrors = validationException.Errors
                            .Select(err => new ValidationError(
                                code: err.ErrorCode ?? "VALIDATION_ERROR",
                                message: err.ErrorMessage
                            ))
                            .ToList();

                        var errorResponse = new ValidationErrorResponse(validationErrors);

                        var options = new JsonSerializerOptions
                        {
                            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                            WriteIndented = true
                        };

                        var errorText = JsonSerializer.Serialize(errorResponse, options);
                        context.Response.StatusCode = 400;
                        context.Response.ContentType = "application/json";
                        await context.Response.WriteAsync(errorText, Encoding.UTF8);
                    }
                    else
                    {
                        // Para outros erros (500), usa ProblemDetails
                        var problemDetails = new ProblemDetailsResponse(
                            title: "Internal Server Error",
                            status: 500,
                            detail: "An unexpected error occurred.",
                            instance: context.Request.Path
                        );

                        // Adiciona informações de trace se disponível
                        if (context.RequestServices.GetService<IWebHostEnvironment>()?.IsDevelopment() == true)
                        {
                            problemDetails.AddExtension("traceId", context.TraceIdentifier);
                            problemDetails.AddExtension("exception", exception?.Message ?? "Unknown error");
                        }

                        var options = new JsonSerializerOptions
                        {
                            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                            WriteIndented = true
                        };

                        var errorText = JsonSerializer.Serialize(problemDetails, options);
                        context.Response.StatusCode = 500;
                        context.Response.ContentType = "application/problem+json";
                        await context.Response.WriteAsync(errorText, Encoding.UTF8);
                    }
                });
            });
        }
    }
}