using GlobalErrorHandling.Domain.Model;
using System.Text;

namespace GlobalErrorHandling.Domain.Exceptions;

[Serializable]
public sealed class ProductException(string message, DummyModel dummy) : BusinessValidationException(Apply(message, dummy))
{
    private static string Apply(string message, DummyModel dummy) => new StringBuilder($"Produt has a problem: Id {dummy.Id}!!! ").Append(message).ToString();
}
