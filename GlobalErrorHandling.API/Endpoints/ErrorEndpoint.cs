using GlobalErrorHandling.Domain.Exceptions;
using GlobalErrorHandling.Domain.Model;
using GlobalErrorHandling.Domain.Services;
using GlobalErrorHandling.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace GlobalErrorHandling.API.Endpoints;

public static class ErrorEndpoint
{
    public static void RegisterErrorEndpoints(this IEndpointRouteBuilder endpoints)
    {        
        endpoints.MapGet("/error/nf", () =>
        {
            throw new NotFoundException("Not found!!!");
        })
        .WithOpenApi(operation => new(operation)
        {
            Summary = "This is a summary: NotFoundException",
            Description = "This is a description: NotFoundException"
        })
        .WithTags("Error API");

        endpoints.MapGet("/error/product", (IDummyService dummyService) =>
        {
            throw new ProductException("Broken product", dummyService.GetDummyValue(123));
        })
        .WithOpenApi(operation => new(operation)
        {
            Summary = "This is a summary: ProductException",
            Description = "This is a description: ProductException"
        })
        .WithTags("Error API");

        endpoints.MapGet("/error/ex", () =>
        {
            throw new Exception("default exception");
        })
        .WithOpenApi(operation => new(operation)
        {
            Summary = "This is a summary: Exception",
            Description = "This is a description: Exception"
        })
        .WithTags("Error API");

        endpoints.MapGet("/error/outof", (IDummyService dummyService) =>
        {
            try
            {
                throw new ProductException("default exception", dummyService.GetDummyValue(321));
            }
            catch (Exception ex)
            {
                if (ex is BusinessValidationException busException)
                {
                    return Results.BadRequest($"Out of global error handler {ex.Message} - Business Errors: {string.Join("; ", busException.Errors.Select(e => e.Error))}");
                }
                
                return Results.BadRequest($"Out of global error handler {ex.Message}");
            }
        })
        .WithOpenApi(operation => new(operation)
        {
            Summary = "Out of global error handling",
            Description = "This is a description: Out of global error handling"
        })
        .WithTags("Error API");
    }
}
