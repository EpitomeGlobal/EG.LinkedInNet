namespace EG.LinkedInNet;

using Models;

public record LearningAssetRequest
{
    /// <summary>
    ///     The types of learning assets to search. The search results will include only learning assets of these types.
    ///     If omitted, the search results will include learning assets of any type. Since this parameter is an array,
    ///     you will need to specify a zero-based index per value.
    /// </summary>
    public AssetType[]? AssetType { get; init; }

    /// <summary>
    ///     An array of learning classification URNs to search learning assets.
    ///     The search results will include only learning assets tagged with these classifications.
    ///     The supported URN types are "urn:li:lyndaCategory" and "urn:li:skill".
    ///     Classification URNs can be discovered using /v2/learningClassifications endpoint.
    ///     If omitted, the search results will include learning assets tagged with any classification.
    ///     Since this parameter is an array, you will need to specify a zero-based index per value.
    /// </summary>
    public string? Classification { get; init; }

    /// <summary>
    ///     An array of difficulty levels of learning assets to search.
    ///     The search results will include only learning assets of these difficulty levels.
    ///     If omitted, the search results will include learning assets of any difficulty level.
    ///     Since this parameter is an array, you will need to specify a zero-based index per value.
    /// </summary>
    public DifficultyLevel[]? DifficultyLevel { get; init; }

    /// <summary>
    ///     The keyword string to search learning assets.
    ///     The search results will include only learning assets matching this keyword string, as determined by LinkedIn
    ///     Learning's relevance algorithm.
    ///     The value of this parameter is case-insensitive.If omitted, the search results will include learning assets
    ///     matching any keyword string.
    /// </summary>
    public string? Keyword { get; init; }

    /// <summary>
    ///     The time after which the assets were changed i.e. released, modified or retired. Represented by number of
    ///     milliseconds since midnight, January 1, 1970 UTC.
    /// </summary>
    public DateTime? LastModifiedAfter { get; init; }

    /// <summary>
    ///     An array of locale countries from which results should be filtered on.
    ///     Since the locales parameter is an array, you will need to specify a zero-based index per value.
    ///     The supported values correspond to the locales "de_DE", "en_US", "es_ES", "fr_FR", "ja_JP", "zh_CN" and "pt_BR".
    /// </summary>
    public Country[]? Countries { get; init; } = { Country.US };


    /// <summary>
    ///     An array of languages from which results should be filtered on.
    ///     The supported values correspond to the locales "de_DE", "en_US", "es_ES", "fr_FR", "ja_JP", "zh_CN" and "pt_BR".
    ///     Since the locales parameter is an array, you will need to specify a zero-based index per value.
    /// </summary>
    public Language[]? Languages { get; init; } = { Language.EN };

    /// <summary>
    ///     Boolean that indicates results should be filtered to only include learningAssets the caller is licensed to access.
    ///     If this parameter is set to true and assetFilteringCriteria.locales parameter is omitted the locale values are
    ///     implied by the callers licensed access.
    /// </summary>
    public bool? LicensedOnly { get; init; }

    /// <summary>
    ///     How to sort the learning assets in the search results.
    ///     Relevance sorts the learning assets by LinkedIn Learning's relevance algorithm.
    ///     Popularity sorts the learning assets by view count. Recency sorts the learning assets by publish date.
    /// </summary>
    public SortyBy SortyBy { get; init; } = SortyBy.RELEVANCE;

    /// <summary>
    ///     The number of levels in the learning asset hierarchy to include asset details.
    ///     This parameter is optional;
    ///     please see the "Specifying the level of asset details" section for an explanation with examples.
    /// </summary>
    public long? ExpandDepth { get; init; }

    /// <summary>
    ///     Whether to include retired learning assets. To retrieve active and retired learning assets, set this parameter to
    ///     true.
    /// </summary>
    public bool? IncludeRetired { get; init; }

    /// <summary>
    ///     The start index of learning assets for the page.
    /// </summary>
    public long Start { get; init; } = 0;

    /// <summary>
    ///     The number of learning assets to include in the page. The maximum count is 100 assets per page.
    /// </summary>
    public long Count { get; init; } = 20;
}
