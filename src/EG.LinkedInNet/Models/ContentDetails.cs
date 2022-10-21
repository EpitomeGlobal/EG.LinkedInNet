namespace EG.LinkedInNet.Models;

public class ContentDetails
{
    /// <summary>
    ///     Title of the learningAsset. For example, “Excel Essential Training”.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    ///     ISO 639-1 language code of the content, including locale information.
    /// </summary>
    public LocaleString Locale { get; set; }

    /// <summary>
    ///     The name of the provider for this content.
    /// </summary>
    public string? ContentProviderName { get; set; }

    /// <summary>
    ///     The launch URL of the INTERNAL learning asset in the LinkedIn Learning web application.
    /// </summary>
    public string? WebsiteUrl { get; set; }

    /// <summary>
    ///     This is the primary content entity identifier
    /// </summary>
    public string ContentUrn { get; set; } = string.Empty;
}
