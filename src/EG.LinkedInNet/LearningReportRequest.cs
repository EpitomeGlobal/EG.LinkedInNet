namespace EG.LinkedInNet;

using Models;

public class LearningReportRequest
{
    /// <summary>
    /// 	Specifies the beginning time for the range of records to be returned or summarized by the report.
    ///     Represented by number of milliseconds since midnight, January 1, 1970 UTC.
    /// </summary>
    public DateTime StartedAt { get; set; }

    /// <summary>
    /// An engagement type metric by which the results should be sorted.
    /// </summary>
    public EngagementMetricType? MetricType { get; set; }

    /// <summary>
    /// An engagement type metric by which the results should be sorted.
    /// </summary>
    public EngagementMetricQualifier? MetricQualifier { get; set; }

    /// <summary>
    /// The order of the results.
    /// </summary>
    public SortOrder SortOrder { get; set; } = SortOrder.DESCENDING;

    /// <summary>
    /// Describes the offset from start parameter to be used.
    /// </summary>
    public TimeOffset OffsetUnit { get; set; }

    /// <summary>
    /// 	Describes the offset from start parameter to be used.
    ///     Duration is limited to a max of 14 days/2 weeks from start date.
    /// </summary>
    public long Duration { get; set; }

    /// <summary>
    ///     Locale of the report. Report content will be localized based on this value.
    /// </summary>
    public Language? LanguageType { get; init; } = Language.EN;

    /// <summary>
    ///     Type of asset for the content whose report is generated.
    /// </summary>
    public AssetType? AssetType { get; init; }

    /// <summary>
    /// 	Denotes whether the content is proprietary to the LinkedIn Learning account, from LinkedIn Learning, or from a third-party provider.
    /// If it is admin created "custom content" the source will be ORGANIZATION.
    /// If it is LinkedIn Learning content the source will be LINKEDIN_LEARNING. For third-party content the source will be THIRD_PARTY.
    /// </summary>
    public ContentSource ContentSource { get; init; } = ContentSource.LINKEDIN_LEARNING;

    /// <summary>
    /// The primary aggregation level for the report.
    /// </summary>
    public AggregationCriteria? Primary { get; init; } = AggregationCriteria.ACCOUNT;

    /// <summary>
    /// The secondary aggregation level for the report.
    /// </summary>
    public AggregationCriteria? Secondary { get; init; }

    /// <summary>
    ///     The start index of learning report for the page.
    /// </summary>
    public long Start { get; init; } = 0;

    /// <summary>
    ///     The number of learning report to include in the page. The maximum count is 1000 report per page.
    /// </summary>
    public long Count { get; init; } = 20;
}
