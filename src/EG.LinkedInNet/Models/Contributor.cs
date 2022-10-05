namespace EG.LinkedInNet.Models;

public class Contributor
{
    /// <summary>
    /// The type of contribution the contributor made to the learning asset.
    /// </summary>
    public ContributionType ContributionType { get; init; }
    
    /// <summary>
    /// The name of the contributor, localized if available.
    /// </summary>
    public LocaleString Name { get; init; }
    
    /// <summary>
    /// The URN identifying the contributor.
    /// </summary>
    public string Urn { get; init; }
}