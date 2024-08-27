using ErrorOr;

namespace IHolder.Domain.Common;

public static class EventualConsistencyError
{
    public const int EventualConsistencyType = 1000;

    public static Error From(string code, string description)
    {
        return Error.Custom(EventualConsistencyType, code, description);
    }
}