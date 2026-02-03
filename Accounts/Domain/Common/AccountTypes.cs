using System;

namespace Domain.Common;

public static class AccountTypes
{
    public const string Savings = "Ahorros";
    public const string Transactional = "Transaccional";

    private static readonly Dictionary<string, string> ValidTypes =
        new(StringComparer.OrdinalIgnoreCase)
        {
            { "ahorros", Savings },
            { "transaccional", Transactional }
        };

    public static string Parse(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new InvalidOperationException("Tipo de cuenta requerido.");

        var key = value.Trim();

        if (!ValidTypes.TryGetValue(key, out var canonical))
            throw new InvalidOperationException($"Tipo de cuenta inválido: {value}");

        return canonical;
    }
}
