using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using EG.LinkedInNet.Models;
using Microsoft.Extensions.Options;

namespace EG.LinkedInNet;

using System.Net.Http.Json;

public class LinkedInClient
{
    private readonly string? baseUrl;

    private readonly JsonSerializerOptions options = new JsonSerializerOptions()
    {
        PropertyNameCaseInsensitive = true,
    };

    /// <summary>
    /// Initializes a new instance of the <see cref="LinkedInClient"/> class.
    /// </summary>
    /// <param name="httpClient">Internal http client.</param>
    /// <param name="options">Linked in config.</param>
    public LinkedInClient(HttpClient httpClient, IOptions<LinkedInConfiguration> options) => (this.Client, this.baseUrl) = (httpClient, options.Value.ApiEndpoint);

    /// <summary>
    /// Gets http client.
    /// </summary>
    private HttpClient Client { get; }

    public bool ReadResponseAsString { get; set; }

    public async Task<IList<Classification>> GetClassifications(
        string query,
        string keyword,
        string country,
        string language,
        int start = 0,
        int count = 100,
        CancellationToken cancellationToken = default
        )
    {
        using var request = new HttpRequestMessage();
        var urlBuilder = new System.Text.StringBuilder();
        urlBuilder.Append(this.baseUrl != null ? this.baseUrl.TrimEnd('/') : "").Append("/v2/learningClassifications?");
        urlBuilder.AddParameter("q", query)
            .AddParameter("keyword", keyword)
            .AddParameter("targetLocale.country", country)
            .AddParameter("targetLocale.language", language)
            .AddParameter("start", start)
            .AddParameter("count", count)
            .Append("fields=name,type,urn");
        request.RequestUri = new Uri(urlBuilder.ToString(), UriKind.RelativeOrAbsolute);

        var response = await this.Client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
        try
        {
            var headers = response.Headers.ToDictionary(h => h.Key, h => h.Value);
            var status = (int)response.StatusCode;
            if (status == 200)
            {
                var objectResponse = await this.ReadObjectResponseAsync<IList<Classification>>(response, headers, cancellationToken).ConfigureAwait(false);
                if (objectResponse.Object == null)
                {
                    throw new ApiException("Response was null which was not expected.", status, objectResponse.Text, headers, null);
                }
                return objectResponse.Object;
            }
            else
            {
                var responseData = response.Content == null ? null : await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);
                throw new ApiException("The HTTP status code of the response was not expected (" + status + ").", status, responseData, headers, null);
            }
        }
        finally
        {
            response.Dispose();
        }
    }

    protected struct ObjectResponseResult<T>
    {
        public ObjectResponseResult(T responseObject, string responseText)
        {
            this.Object = responseObject;
            this.Text = responseText;
        }

        public T Object { get; }

        public string Text { get; }
    }

    protected record WrappedResponse<T>(T elements);

    protected virtual async Task<ObjectResponseResult<T>> ReadObjectResponseAsync<T>(
        HttpResponseMessage response,
        IReadOnlyDictionary<string, IEnumerable<string>> headers,
        CancellationToken cancellationToken)
    {
        if (response == null || response.Content == null)
        {
            return new ObjectResponseResult<T>(default(T), string.Empty);
        }

        var responseText = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);
        try
        {
            var typedBody = JsonSerializer.Deserialize<WrappedResponse<T>>(responseText);
            return new ObjectResponseResult<T>(typedBody.elements, responseText);
        }
        catch (JsonException exception)
        {
            var message = "Could not deserialize the response body string as " + typeof(T).FullName + ".";
            throw new ApiException(message, (int)response.StatusCode, responseText, headers, exception);
        }
    }
}
