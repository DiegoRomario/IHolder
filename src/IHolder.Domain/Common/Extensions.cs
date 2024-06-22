namespace IHolder.Domain.Common;
public static class Extensions
{
    public static decimal ToFloor(this decimal value)
    {
        return Math.Round(value, 2, MidpointRounding.ToZero);
    }
}