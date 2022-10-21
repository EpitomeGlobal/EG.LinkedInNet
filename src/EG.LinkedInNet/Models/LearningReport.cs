namespace EG.LinkedInNet.Models;

public class LearningReport
{
    /// <summary>
    /// Details corresponding to the learner like the name, email and uniqueUserId.
    /// </summary>
    public LearnerDetails? LearnerDetails { get; init; }

    /// <summary>
    /// Details corresponding to the content like the name, locale and ID.
    /// </summary>
    public ContentDetails? ContentDetails { get; init; }

    /// <summary>
    /// All the relevant content engagements that occurred like the number of completions and unique views.
    /// </summary>
    public EngagementMetric[]? Activities { get; init; }

    /// <summary>
    /// Milliseconds since epoch for the latest data on which the report is based.
    /// </summary>
    public long? LatestDataAt { get; init; }
}
