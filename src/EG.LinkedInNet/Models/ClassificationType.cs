namespace EG.LinkedInNet.Models;

public enum ClassificationType
{
    /// <summary>
    ///     Indicates this is a skill that should be acquired or strengthened by completing this learning asset.
    /// </summary>
    SKILL,

    /// <summary>
    ///     Library of content that this learning asset is associated with. Represents a broad grouping of content, e.g.
    ///     Business
    /// </summary>
    LIBRARY,

    /// <summary>
    ///     Content subject that this learning asset is associated with. Subjects are children of libraries and represent a
    ///     group of related topics, e.g. Marketing.
    /// </summary>
    SUBJECT,

    /// <summary>
    ///     Content topic that this learning asset is associated with. Topics are children of subject and represent a single
    ///     granular topic, e.g. Email Marketing
    /// </summary>
    TOPIC,

    /// <summary>
    ///     Continuing Education Unit associated with this content, e.g. Continuing Professional Education
    /// </summary>
    CONTINUING_EDUCATION_UNIT
}
