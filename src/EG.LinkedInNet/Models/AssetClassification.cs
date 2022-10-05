using System;

namespace EG.LinkedInNet.Models;

public record AssetClassification
{
    /// <summary>
    /// The person or organization who tagged the learning asset with the learning classification.
    /// </summary>
    public NamedParty Assigner { get; init; } = new ();
    
    /// <summary>
    /// The learning classification the learning asset is tagged with.
    /// </summary>
    public Classification AssociatedClassification { get; init; } = new ();

    /// <summary>
    /// The parent learning classifications of the associated learning classification.
    /// </summary>
    public Classification[] Path { get; init; } = Array.Empty<Classification>();

}