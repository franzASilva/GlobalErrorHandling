using GlobalErrorHandling.Domain.Model;
using GlobalErrorHandling.Domain.Services.Interfaces;

namespace GlobalErrorHandling.Domain.Services;

public class DummyService : IDummyService
{
    public DummyModel[] GetDummyValue() =>
        Enumerable
        .Range(1, 5)
        .Select(index =>
        new DummyModel
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            Random.Shared.Next(1, 100)
        ))
        .ToArray();

    public DummyModel GetDummyValue(int id) =>
        new
        (
            DateOnly.FromDateTime(DateTime.Now),
            Random.Shared.Next(-20, 55),
            id
        );
}
