namespace EG.LinkedInNet.Models;

public enum EngagementMetricType
{
    /// <summary>
    ///     Total number of seconds a content has been viewed by enterprise entities.
    /// </summary>
    SECONDS_VIEWED,

    /// <summary>
    ///     Number of times the content has been completed by enterprise entities.
    /// </summary>
    COMPLETIONS,

    /// <summary>
    ///     Total number of days the enterprise entities did at least one engagement for any content (day is defined as the
    ///     period of 24 hours starting from 12am PDT)
    /// </summary>
    DAYS_ACTIVE,

    /// <summary>
    ///     Number of times the content has been marked as done by enterprise entities.
    /// </summary>
    MARKED_AS_DONE,

    /// <summary>
    ///     Percentage of the content completed.
    /// </summary>
    PROGRESS_PERCENTAGE,

    /// <summary>
    ///     Number of views for the content.
    /// </summary>
    VIEWS,

    /// <summary>
    ///     Number of times the content has been engaged with by enterprise entities.
    /// </summary>
    ENGAGED_LEARNERS
}
