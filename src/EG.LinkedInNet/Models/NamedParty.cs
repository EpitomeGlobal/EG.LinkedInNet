namespace EG.LinkedInNet.Models;

public record NamedParty
{
    /// <summary>
    /// The name of a person or organization, localized if available.
    /// </summary>
    public LocaleString Name { get; init; }

    /// <summary>
    /// The URN identifying a person or organization.
    /// </summary>
    public string Urn { get; init; } = string.Empty;
}
