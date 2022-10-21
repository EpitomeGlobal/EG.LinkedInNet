namespace EG.LinkedInNet;

using System.Net.Http.Json;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

/// <summary>
///     Manage client token.
/// </summary>
public class ClientTokenManagementService : IClientTokenManagementService
{
    private readonly IOptions<LinkedInConfiguration> config;
    private readonly ILogger<ClientTokenManagementService> logger;
    private DateTime? expireAt;
    private string token;

    /// <summary>
    ///     Client token constructor.
    /// </summary>
    /// <param name="logger">ILogger instance.</param>
    /// <param name="config">Linked in config.</param>
    public ClientTokenManagementService(ILogger<ClientTokenManagementService> logger,
        IOptions<LinkedInConfiguration> config)
    {
        this.token = string.Empty;
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
        var content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                { "grant_type", "client_credentials" },
                { "client_id", this.config.Value.Client },
                { "client_secret", this.config.Value.Secret }
            }
        );

        HttpResponseMessage response = await client.PostAsync(uri, content, cancellationToken).ConfigureAwait(false);
        if (response.IsSuccessStatusCode)
        {
            TokenResponse? tokenResponse =
                await response.Content.ReadFromJsonAsync<TokenResponse>(cancellationToken: cancellationToken);
            if (tokenResponse is null)
            {
                this.logger.LogDebug("LinkedIn token failed to deserialize");
            }

            this.token = tokenResponse!.access_token;
            this.expireAt = DateTime.UtcNow.AddSeconds(tokenResponse.expires_in);
        }
        else
        {
            ErrorResponse? message =
                await response.Content.ReadFromJsonAsync<ErrorResponse>(cancellationToken: cancellationToken);
            this.logger.LogDebug($"LinkedIn token failed: {message.error}");
            throw new InvalidOperationException(message.error_description);
        }
    }

    private record TokenResponse(string access_token, int expires_in);

    private record ErrorResponse(string error, string error_description);
}
