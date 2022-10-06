namespace EG.LinkedInNet.Models;

using System.Text.Json.Serialization;

public class LocaleString
{
    /// <summary>
    /// The locale of the localized string.
    /// </summary>
    public Locale Locale { get; init; }

    /// <summary>
    /// 	The localized string.
    /// </summary>
    public string Value { get; init; }
}
