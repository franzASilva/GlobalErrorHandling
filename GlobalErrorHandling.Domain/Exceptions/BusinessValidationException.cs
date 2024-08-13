using GlobalErrorHandling.Domain.Model;

namespace GlobalErrorHandling.Domain.Exceptions;

[Serializable]
public class BusinessValidationException : Exception
{
    public ErrorDetailModel[] Errors { get; } = [];

    public BusinessValidationException(string message)
    {
        this.Errors = [new ErrorDetailModel(message)];
    }

    public BusinessValidationException(string message, Exception innerException) : base(message, innerException)
    {
    }

    public BusinessValidationException(List<string> messages)
    {
        this.Errors = messages.Select(errorMessage => new ErrorDetailModel(errorMessage)).ToArray();
    }
}
