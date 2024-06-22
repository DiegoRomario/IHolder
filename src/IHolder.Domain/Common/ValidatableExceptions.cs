using Throw;

namespace IHolder.Domain.Common;

public static class ValidatableExceptions
{
    private const string message = "The values exceeds the maximum value allowed for its type.";
    public static ref readonly Validatable<bool> IfValueOverflowed(this in Validatable<bool> validatable)
    {
        if (validatable.Value) throw new ArgumentException(message, validatable.ParamName);

        return ref validatable;
    }
}
