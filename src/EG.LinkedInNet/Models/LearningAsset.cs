namespace EG.LinkedInNet.Models;

public class LearningAsset
{
    /// <summary>
    /// The title of the learning asset, localized if available.
    /// </summary>
    public LocaleString Title { get; init; }

    /// <summary>
    /// 	The type of the learning asset.
    /// </summary>
    public AssetType Type { get; init; }

    /// <summary>
    /// The URN of the learning asset.
    /// The URN is a unique identifier whose value should be treated as opaque.
    /// Do not use the URN to determine the type of the learning asset; use the "type" field instead.
    /// </summary>
    public string Urn { get; init; }

    /// <summary>
    /// If present, the details about the learning asset.
    /// If this field is not present, it means the request did not specify retrieving the asset details (see the "Specifying the level of asset details" section).
    /// </summary>
    public AssetDetails? Details { get; init; }

    /// <summary>
    /// The sub-assets of the learning asset.
    /// For example, a learning asset representing a course has sub-assets representing its chapters;
    /// a learning asset representing a chapter has sub-assets representing its videos.
    /// </summary>
    public SubAsset[]? Contents { get; init; }
}
