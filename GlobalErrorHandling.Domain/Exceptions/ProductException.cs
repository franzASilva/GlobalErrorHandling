using System.Text;

namespace GlobalErrorHandling.Domain.Exceptions;

[Serializable]
public sealed class ProductException(string message) : BusinessValidationException(Apply(message))
{
    private static string Apply(string message) => new StringBuilder("Produt has a problem!!! ").Append(message).ToString();
}
