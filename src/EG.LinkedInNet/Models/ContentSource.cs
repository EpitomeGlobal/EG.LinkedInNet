namespace EG.LinkedInNet.Models;

public enum ContentSource
{
    /// <summary>
    ///     Combines LINKEDIN_LEARNING, ORGANIZATION, and THIRD_PARTY.
    /// </summary>
    ALL_SOURCES,

    /// <summary>
    ///     Include content published by LinkedIn Learning.
    /// </summary>
    LINKEDIN_LEARNING,

    /// <summary>
    ///     Include content published by the caller's enterprise organization, not intended for use outside of the enterprise
    ///     organization.
    /// </summary>
    ORGANIZATION,

    /// <summary>
    ///     Include content published by any organization for external use, including user-supplied content providers and the
    ///     caller's own organization.
    /// </summary>
    THIRD_PARTY,

    /// <summary>
    ///     Resource is proprietary. It is only available to the LinkedIn Learning account making the request.
    /// </summary>
    [Obsolete("Please use LINKEDIN_LEARNING")]
    INTERNAL,

    /// <summary>
    ///     Resource is available to all LinkedIn Learning customers.
    /// </summary>
    [Obsolete("Please use THIRD_PARTY or ORGANIZATION")]
    EXTERNAL
}
