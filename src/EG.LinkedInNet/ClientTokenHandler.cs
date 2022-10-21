namespace EG.LinkedInNet;

using System.Net;
using System.Net.Http.Headers;

/// <summary>
///     Delegating handler that injects a client access token into an outgoing request
/// </summary>
public class ClientAccessTokenHandler : DelegatingHandler
{
    private readonly IClientTokenManagementService tokenManager;

    /// <summary>
    ///     ctor
    /// </summary>
    /// <param name="tokenManager">The Access Token Management Service</param>
    public ClientAccessTokenHandler(IClientTokenManagementService tokenManager)
    {
        this.tokenManager = tokenManager;
    }

    /// <inheritdoc />
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        await this.SetTokenAsync(request, false, cancellationToken);
        HttpResponseMessage response = await base.SendAsync(request, cancellationToken);

        // retry if 401
        if (response.StatusCode == HttpStatusCode.Unauthorized)
        {
            response.Dispose();

            await this.SetTokenAsync(request, true, cancellationToken);
            return await base.SendAsync(request, cancellationToken);
        }

        return response;
    }

    /// <summary>
    ///     Set an access token on the HTTP request
    /// </summary>
    /// <param name="request"></param>
    /// <param name="forceRenewal"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    protected virtual async Task SetTokenAsync(HttpRequestMessage request, bool forceRenewal,
        CancellationToken cancellationToken)
    {
        string token = await this.tokenManager.GetTokenAsync(forceRenewal, cancellationToken);

        if (!string.IsNullOrWhiteSpace(token))
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
    }
}
