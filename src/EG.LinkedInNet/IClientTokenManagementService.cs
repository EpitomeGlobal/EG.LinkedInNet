namespace EG.LinkedInNet;

public interface IClientTokenManagementService
{
    /// <summary>
    /// Gets the access token.
    /// </summary>
    /// <param name="forceRenew">Force renew of token.</param>
    /// <param name="cancellationToken">Cancel token.</param>
    /// <returns></returns>
    Task<string> GetTokenAsync(bool forceRenew = false, CancellationToken cancellationToken = default);
}
