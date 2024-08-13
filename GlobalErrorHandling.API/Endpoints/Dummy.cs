using GlobalErrorHandling.Domain.Exceptions;
using GlobalErrorHandling.Domain.Model;
using GlobalErrorHandling.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace GlobalErrorHandling.API.Endpoints;

public static class Product
{
    public static void RegisterDummyEndpoints(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/dummy", (IDummyService dummyService) =>
        {
            return dummyService.GetDummyValue();
        })
        .WithName("GetDummyValue")
        .WithOpenApi(operation => new(operation)
        {
            Summary = "This is a summary",
            Description = "This is a description"
        })
        .Produces<DummyModel[]>(StatusCodes.Status200OK)
        .Produces<NotFound>(StatusCodes.Status404NotFound);

        endpoints.MapGet("/dummy/{id}", Results<Ok<DummyModel>, NotFound> (IDummyService dummyService, int id) =>
        {
            var dummy = dummyService.GetDummyValue(id);
            return dummy is not null
             ? TypedResults.Ok(dummy)
             : TypedResults.NotFound();
        });

        endpoints.MapGet("/dummy/one",
        [ProducesResponseType(typeof(DummyModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(NotFound), StatusCodes.Status404NotFound)]
        (IDummyService dummyService, int id) => dummyService.GetDummyValue(id));            

        endpoints.MapGet("/dummy/nf", () =>
        {
            throw new NotFoundException("Not found!!!");
        })
        .WithOpenApi(operation => new(operation)
        {
            Summary = "This is a summary: NotFoundException",
            Description = "This is a description: NotFoundException"
        });

        endpoints.MapGet("/dummy/product", () =>
        {
            throw new ProductException("Product Id 1234");
        })
        .WithOpenApi(operation => new(operation)
        {
            Summary = "This is a summary: ProductException",
            Description = "This is a description: ProductException"
        });

        endpoints.MapGet("/dummy/ex", () =>
        {
            throw new Exception("default exception");
        })
        .WithOpenApi(operation => new(operation)
        {
            Summary = "This is a summary: Exception",
            Description = "This is a description: Exception"
        });

        endpoints.MapGet("/dummy/outof", () =>
        {
            try
            {
                throw new ProductException("default exception");
            }
            catch (Exception ex)
            {
                return Results.BadRequest($"Out of global error handler {ex.Message}");
            }
        })
        .WithOpenApi(operation => new(operation)
        {
            Summary = "Out of global error handling",
            Description = "This is a description: Out of global error handling"
        });
    }
}
