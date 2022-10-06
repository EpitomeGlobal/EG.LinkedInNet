namespace EG.LinkedInNet;

using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

/// <summary>
///     Manage client token.
/// </summary>
public class ClientTokenManagementService : IClientTokenManagementService
{
    private readonly ILogger<ClientTokenManagementService> logger;
    private readonly IOptions<LinkedInConfiguration> config;
    private DateTime? expireAt;
    private string token;

    public ClientTokenManagementService(ILogger<ClientTokenManagementService> logger, IOptions<LinkedInConfiguration> config)
    {
        this.logger = logger;
        this.config = config;
    }

    public async Task<string> GetTokenAsync(bool forceRenew = false, CancellationToken cancellationToken = default)
    {
        if (!string.IsNullOrEmpty(this.token) && !forceRenew && this.expireAt > DateTime.UtcNow)
        {
            return this.token;
        }

        this.logger.LogDebug("LinkedIn token renewed on {UtcNow}", DateTime.UtcNow);
        await this.GetTokenAsync(cancellationToken);
        return this.token;
    }

    private async Task GetTokenAsync(CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(this.config.Value.Client) || string.IsNullOrEmpty(this.config.Value.Secret))
        {
            throw new InvalidOperationException("No oauth client configured.");
        }

        using var client = new HttpClient();
        var uri = new Uri(this.config.Value.TokenEndpoint, UriKind.RelativeOrAbsolute);
        var content = new FormUrlEncodedContent(new Dictionary<string,string>
            {
                { "grant_type", "client_credentials" },
                { "client_id", this.config.Value.Client },
                { "client_secret", this.config.Value.Secret }
            }
        );

        var response = await client.PostAsync(uri, content, cancellationToken).ConfigureAwait(false);
        if (response.IsSuccessStatusCode)
        {
            var tokenResponse = await response.Content.ReadFromJsonAsync<TokenResponse>(cancellationToken :cancellationToken);
            if (tokenResponse is null)
            {
                this.logger.LogDebug("LinkedIn token failed to deserialize");
            }
            this.token = tokenResponse!.access_token;
            this.expireAt = DateTime.UtcNow.AddSeconds(tokenResponse.expires_in);
        }
        else
        {
            var message = await response.Content.ReadFromJsonAsync<ErrorResponse>(cancellationToken :cancellationToken);
            this.logger.LogDebug($"LinkedIn token failed: {message.error}");
            throw new InvalidOperationException(message.error_description);
        }
    }

    private record TokenResponse(string access_token, int expires_in);

    private record ErrorResponse(string error, string error_description);

}
