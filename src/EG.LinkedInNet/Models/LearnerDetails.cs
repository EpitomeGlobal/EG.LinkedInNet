namespace EG.LinkedInNet.Models;

public class LearnerDetails
{
    /// <summary>
    ///     Name of the enterprise entity. An enterprise entity can be an account, group or individual.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Email address of the enterprise entity.
    /// </summary>
    public string? Email { get; set; }

    /// <summary>
    /// A list of groups that the enterprise profile belongs to.
    /// </summary>
    public string[] EnterpriseGroups { get; set; }

    /// <summary>
    /// If present, a unique and immutable user identifier.
    /// This value is generally provided to LinkedIn Learning through a manual or automated user provisioning process during account configuration.
    /// This value is often used to make an association between learner profiles in other enterprise applications like learning management systems (LMSs) or business intelligence (BI) tools.
    /// </summary>
    public string? UniqueUserId { get; set; }

    /// <summary>
    /// LinkedIn Learning specific unique entity identifier.
    /// </summary>
    public string Entity { get; set; } = string.Empty;

    /// <summary>
    /// Additional profile attributes provided by the enterprise.
    /// This data is generally provided to LinkedIn Learning through a manual or automated user provisioning process during account configuration.
    /// </summary>
    public dynamic? CustomAttributes { get; set; }
}
