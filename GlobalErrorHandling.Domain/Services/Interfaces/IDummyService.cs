using GlobalErrorHandling.Domain.Model;

namespace GlobalErrorHandling.Domain.Services.Interfaces;

public interface IDummyService
{
    DummyModel[] GetDummyValue();
    DummyModel GetDummyValue(int id);
}
