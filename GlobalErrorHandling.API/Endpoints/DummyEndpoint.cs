using GlobalErrorHandling.Domain.Exceptions;
using GlobalErrorHandling.Domain.Model;
using GlobalErrorHandling.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace GlobalErrorHandling.API.Endpoints;

public static class DummyEndpoint
{
    public static void RegisterDummyEndpoints(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/dummy", (IDummyService dummyService) =>
        {
            return dummyService.GetDummyValue();
        })
        .WithName("GetDummiesValues")
        .WithTags("Dummy API")
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
        })
        .WithName("GetDummy")
        .WithTags("Dummy API");        

        endpoints.MapGet("/dummy/one",
        [ProducesResponseType(typeof(DummyModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(NotFound), StatusCodes.Status404NotFound)]
        (IDummyService dummyService, int id) => dummyService.GetDummyValue(id))
        .WithName("GetOneDummy")
        .WithTags("Dummy API");        
    }
}
