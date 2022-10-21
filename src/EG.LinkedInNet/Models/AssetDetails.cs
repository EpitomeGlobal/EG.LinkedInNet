namespace EG.LinkedInNet.Models;

public class AssetDetails
{
    /// <summary>
    ///     The availability of the learning asset.
    /// </summary>
    public Availability? Availability { get; init; }

    /// <summary>
    ///     The locales the learning asset is available in.
    /// </summary>
    public Locale[] AvailableLocales { get; init; } = Array.Empty<Locale>();

    /// <summary>
    ///     The type of the learning asset..
    /// </summary>
    public AssetType Type { get; init; }

    public LocaleString? Title { get; init; }

    /// <summary>
    ///     The learning classifications the learning asset is tagged with.
    /// </summary>
    public AssetClassification[]? Classifications { get; init; } = Array.Empty<AssetClassification>();

    /// <summary>
    ///     The contributors involved in the lifecycle of the learning asset - for example, authors or publishers.
    /// </summary>
    public Contributor[]? Contributors { get; init; } = Array.Empty<Contributor>();

    /// <summary>
    ///     If present, the text-only description of the learning asset, localized if available. Any HTML markup will be
    ///     stripped from this description.
    /// </summary>
    public LocaleString? Description { get; init; }

    /// <summary>
    ///     If present, the description - including any HTML markup - of the learning asset, localized if available.
    /// </summary>
    public LocaleString? DescriptionIncludingHtml { get; init; }

    /// <summary>
    ///     The images that can be used to represent the learning asset.
    /// </summary>
    public AssetImages? Images { get; init; }

    /// <summary>
    ///     The epoch time in milliseconds indicating when the learning asset was last updated.
    /// </summary>
    public long LastUpdatedAt { get; init; }

    /// <summary>
    ///     If present, the difficulty level of the learning asset.
    /// </summary>
    public DifficultyLevel Level { get; init; }

    /// <summary>
    ///     The epoch time in milliseconds indicating when the learning asset was published.
    /// </summary>
    public long? PublishedAt { get; init; }

    /// <summary>
    ///     If present, the epoch time in milliseconds indicating when the learning asset was retired.
    /// </summary>
    public long? RetiredAt { get; init; }

    /// <summary>
    ///     Localized text only short description of the learning asset.
    /// </summary>
    public LocaleString? ShortDescription { get; init; }

    /// <summary>
    ///     If present, the time span indicating how long the learning asset takes to complete.
    /// </summary>
    public TimeSpan? TimeToComplete { get; init; }

    /// <summary>
    ///     The URLs that can be used to launch the learning asset.
    /// </summary>
    public AssetUrls? Urls { get; init; }
}
