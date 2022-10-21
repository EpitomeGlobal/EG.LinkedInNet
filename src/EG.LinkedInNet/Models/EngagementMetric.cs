namespace EG.LinkedInNet.Models;

public class EngagementMetric
{
    /// <summary>
    ///     Type of asset for the content whose report is generated. Supported values include:
    ///     COURSE
    ///     VIDEO
    /// </summary>
    public AssetType? AssetType { get; set; }

    /// <summary>
    ///     An engagement type metric by which the results should be sorted.
    /// </summary>
    public EngagementMetricType EngagementType { get; set; }

    /// <summary>
    ///     Value of the engagement for the engagement type. For example, number of seconds views or course completions.
    /// </summary>
    public long EngagementValue { get; set; }

    /// <summary>
    ///     The time when the content was last engaged. For example, the date the content was last viewed or completed. For
    ///     COMPLETIONS this is the time when the content was completed.
    /// </summary>
    public long? LastEngagedAt { get; set; }
}
