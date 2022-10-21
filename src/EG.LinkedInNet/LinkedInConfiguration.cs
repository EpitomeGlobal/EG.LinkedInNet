namespace EG.LinkedInNet;

public class LinkedInConfiguration
{
    /// <summary>
    ///     Gets or sets token endpoint.
    /// </summary>
    public string TokenEndpoint { get; set; } = "https://www.linkedin.com/oauth/v2/accessToken";

    /// <summary>
    ///     Gets or sets api endpoint.
    /// </summary>
    public string ApiEndpoint { get; set; } = "https://api.linkedin.com";

    /// <summary>
    ///     Gets or sets oauth client.
    /// </summary>
    public string Client { get; set; }

    /// <summary>
    ///     Gets or sets oauth secret.
    /// </summary>
    public string Secret { get; set; }
}
