namespace GlobalErrorHandling.Domain.Model;

public record DummyModel(DateOnly Date, int Value, int Id = 0)
{
    public int ValueOffset => 32 + (int)(Value / 0.5556);
}
