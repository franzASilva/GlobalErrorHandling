namespace GlobalErrorHandling.Domain.Exceptions;

[Serializable]
public sealed class NotFoundException(string message) : BusinessValidationException(message)
{
}
