namespace EG.LinkedInNet.Models;

public enum AggregationCriteria
{
    /// <summary>
    ///     Aggregation at LinkedIn Learning account level. This is the highest possible level of aggregation.
    /// </summary>
    ACCOUNT,

    /// <summary>
    ///     Aggregation at individual user or learner level.
    /// </summary>
    INDIVIDUAL,

    /// <summary>
    ///     Aggregation at content level.
    /// </summary>
    CONTENT
}
