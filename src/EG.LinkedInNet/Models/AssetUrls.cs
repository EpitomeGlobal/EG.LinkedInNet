namespace EG.LinkedInNet.Models;

public class AssetUrls
{
    /// <summary>
    /// If present, the AICC launch URL of the learning asset. This value is used to initiate course completion tracking in AICC-compliant systems. The value is only included in the response when the requestor has AICC enabled in their LinkedIn Learning admin settings.
    /// </summary>
    public string? aiccLaunch { get; init; }
    
    /// <summary>
    /// If present, the single sign-on launch URL of the learning asset. The value is only included in the response when the requestor has configured an active SSO connection in their LinkedIn Learning admin settings.
    /// </summary>
    public string? SsoLaunch { get; init; }
    
    /// <summary>
    /// If present, the launch URL of the learning asset in the LinkedIn Learning web application.
    /// </summary>
    public string? WebLaunch { get; init; }
}