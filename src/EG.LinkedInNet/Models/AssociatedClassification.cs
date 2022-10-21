namespace EG.LinkedInNet.Models;

public class AssociatedClassification
{
    /// <summary>
    ///     The localized name of the skill or category.
    /// </summary>
    public LocaleString Name { get; init; }

    /// <summary>
    ///     Specifies which person or organization is responsible for originally creating this classification.
    /// </summary>
    public NamedParty Owner { get; init; }

    /// <summary>
    ///     Indicates which type of classification this summary represents.
    /// </summary>
    public ClassificationType Type { get; init; }

    /// <summary>
    ///     Identifies the classification, currently supports skill, lyndaCategory, and lyndaCredentialingProgram (CEU) urns.
    /// </summary>
    public string Urn { get; init; }
}
