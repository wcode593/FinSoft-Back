using System.Globalization;

namespace Application.Commons.Helpers;

public static class StringHelper
{
    public static string ToTitleCase(string value)
    {
        if (string.IsNullOrWhiteSpace(value)) return string.Empty;
        return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(value.Trim().ToLower());
    }

    public static string GenerateAccountNumber()
    {
        return $"ACC-{DateTime.UtcNow:yyyyMMddHHmmss}-{Random.Shared.Next(100, 999)}";
    }
}
