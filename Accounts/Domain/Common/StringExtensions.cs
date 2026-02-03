using System.Globalization;

namespace Application.Common;

public static class StringExtensions
{
    public static string ToTitleCaseSafe(this string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return string.Empty;

        value = value.Trim().ToLowerInvariant();

        return CultureInfo
            .CurrentCulture
            .TextInfo
            .ToTitleCase(value);
    }
}
