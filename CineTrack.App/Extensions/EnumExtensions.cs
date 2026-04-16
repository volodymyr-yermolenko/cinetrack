namespace CineTrack.App.Extensions;

public static class EnumExtensions
{
    public static bool IsValidEnum<TEnum>(this TEnum value) where TEnum : struct, Enum
    {
        return Enum.IsDefined<TEnum>(value);
    }
}