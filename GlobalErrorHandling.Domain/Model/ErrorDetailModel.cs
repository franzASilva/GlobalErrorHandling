using GlobalErrorHandling.Domain.Exceptions.Constants;

namespace GlobalErrorHandling.Domain.Model;

public class ErrorDetailModel(
        string error,
        ExceptionType type = ExceptionType.BusinessValidation,
        string? detail = default)
{
    public string Type { get; private set; } = type.ToString();
    public string Error { get; private set; } = error;
    public string? Detail { get; private set; } = detail;
}
