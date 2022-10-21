namespace EG.LinkedInNet.Models;

public record Locale
{
    /// <summary>
    ///     If present, an uppercase two-letter country code as defined by ISO-3166.
    /// </summary>
    public string? Country { get; init; }

    /// <summary>
    ///     A lowercase two-letter language code as defined by ISO-639.
    /// </summary>
    public string? Language { get; init; }

    /// <summary>
    ///     If present, a vendor or browser-specific code.
    /// </summary>
    public string Variant { get; init; } = string.Empty;
}
