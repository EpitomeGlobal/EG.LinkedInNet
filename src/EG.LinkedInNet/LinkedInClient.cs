namespace EG.LinkedInNet;

using System.Text;
using Microsoft.Extensions.Options;
using Models;
using Newtonsoft.Json;

public class LinkedInClient
{
    private readonly string? baseUrl;

    /// <summary>
    ///     Initializes a new instance of the <see cref="LinkedInClient" /> class.
    /// </summary>
    /// <param name="httpClient">Internal http client.</param>
    /// <param name="options">Linked in config.</param>
    public LinkedInClient(HttpClient httpClient, IOptions<LinkedInConfiguration> options)
    {
        (this.Client, this.baseUrl) = (httpClient, options.Value.ApiEndpoint);
    }

    /// <summary>
    ///     Gets http client.
    /// </summary>
    private HttpClient Client { get; }

    public async Task<LinkedInResponse<Classification>> GetClassifications(
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
        var urlBuilder = new StringBuilder();
        urlBuilder.Append(this.baseUrl != null ? this.baseUrl.TrimEnd('/') : "").Append("/v2/learningClassifications?");
        urlBuilder.AddParameter("q", "keyword")
            .AddParameter("keyword", keyword)
            .AddParameter("targetLocale.country", country)
            .AddParameter("targetLocale.language", language)
            .AddParameter("start", start)
            .AddParameter("count", count)
            .Append("fields=name,type,urn");
        request.RequestUri = new Uri(urlBuilder.ToString(), UriKind.RelativeOrAbsolute);

        HttpResponseMessage response = await this.Client
            .SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
        try
        {
            var headers = response.Headers.ToDictionary(h => h.Key, h => h.Value);
            int status = (int)response.StatusCode;
            if (status == 200)
            {
                ObjectResponseResult<LinkedInResponse<Classification>> objectResponse =
                    await this.ReadObjectResponseAsync<Classification>(response, headers, cancellationToken)
                        .ConfigureAwait(false);
                if (objectResponse.Object == null)
                {
                    throw new ApiException("Response was null which was not expected.", status, objectResponse.Text,
                        headers, null);
                }

                return objectResponse.Object;
            }
            else
            {
                string? responseData = response.Content == null
                    ? null
                    : await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);
                throw new ApiException("The HTTP status code of the response was not expected (" + status + ").",
                    status, responseData, headers, null);
            }
        }
        finally
        {
            response.Dispose();
        }
    }

    public async Task<LinkedInResponse<LearningReport>> GetLearningActivityReports(
        LearningReportRequest request,
        CancellationToken cancellationToken = default
    )
    {
        using var requestMessage = new HttpRequestMessage();
        var urlBuilder = new StringBuilder();
        urlBuilder.Append(this.baseUrl != null ? this.baseUrl.TrimEnd('/') : "").Append("/v2/learningActivityReports?");
        urlBuilder.AddParameter("q", "criteria")
            .AddParameter("startedAt", request.StartedAt.Subtract(DateTime.UnixEpoch).TotalMilliseconds)
            .AddParameter("sortBy.engagementMetricType", request.MetricType)
            .AddParameter("sortBy.engagementMetricQualifier", request.MetricQualifier)
            .AddParameter("sortOrder", request.SortOrder)
            .AddParameter("timeOffset.unit", request.OffsetUnit)
            .AddParameter("timeOffset.duration", request.Duration)
            .AddParameter("locale.language", request.LanguageType?.ToString().ToLowerInvariant())
            .AddParameter("assetType", request.AssetType)
            .AddParameter("contentSource", request.ContentSource)
            .AddParameter("aggregationCriteria.primary", request.Primary)
            .AddParameter("aggregationCriteria.secondary", request.Secondary)
            .AddParameter("start", request.Start)
            .AddParameter("count", request.Count);
        requestMessage.RequestUri = new Uri(urlBuilder.ToString(), UriKind.RelativeOrAbsolute);

        HttpResponseMessage response = await this.Client
            .SendAsync(requestMessage, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
        try
        {
            var headers = response.Headers.ToDictionary(h => h.Key, h => h.Value);
            int status = (int)response.StatusCode;
            if (status == 200)
            {
                ObjectResponseResult<LinkedInResponse<LearningReport>> objectResponse =
                    await this.ReadObjectResponseAsync<LearningReport>(response, headers, cancellationToken)
                        .ConfigureAwait(false);
                if (objectResponse.Object == null)
                {
                    throw new ApiException("Response was null which was not expected.", status, objectResponse.Text,
                        headers, null);
                }

                return objectResponse.Object;
            }
            else
            {
                string? responseData = response.Content == null
                    ? null
                    : await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);
                throw new ApiException("The HTTP status code of the response was not expected (" + status + ").",
                    status, responseData, headers, null);
            }
        }
        finally
        {
            response.Dispose();
        }
    }

    public async Task<LinkedInResponse<LearningAsset>> GetLearningAssets(LearningAssetRequest request,
        CancellationToken cancellationToken = default)
    {
        using var requestMessage = new HttpRequestMessage();
        var urlBuilder = new StringBuilder();
        urlBuilder.Append(this.baseUrl != null ? this.baseUrl.TrimEnd('/') : "").Append("/v2/learningAssets?");
        urlBuilder
            .AddParameter("q", "criteria")
            .AddParameter("assetFilteringCriteria.keyword", request.Keyword)
            .AddParameter("assetPresentationCriteria.sortBy", request.SortyBy)
            .AddParameter("assetRetrievalCriteria.expandDepth", request.ExpandDepth)
            .AddParameter("assetRetrievalCriteria.includeRetired", request.IncludeRetired)
            .AddParameter("assetFilteringCriteria.licensedOnly", request.LicensedOnly)
            .AddParameter("assetFilteringCriteria.lastModifiedAfter", request.LastModifiedAfter?.Subtract(DateTime.UnixEpoch).TotalMilliseconds)
            .AddParameter("start", request.Start)
            .AddParameter("count", request.Count);
        if (request.AssetType is not null)
        {
            foreach (var item in request.AssetType.Select((value, i) => new { i, value }))
            {
                urlBuilder.AddParameter($"assetFilteringCriteria.assetTypes[{item.i}]", item.value);
            }
        }

        if (request.Classification is not null)
        {
            foreach (var item in request.Classification.Select((value, i) => new { i, value }))
            {
                urlBuilder.AddParameter($"assetFilteringCriteria.classifications[{item.i}]", item.value);
            }
        }

        if (request.Languages is not null)
        {
            foreach (var item in request.Languages.Select((value, i) => new { i, value }))
            {
                var country = request.Countries[item.i];
                urlBuilder.AddParameter($"assetFilteringCriteria.locales[{item.i}].language", item.value);
                urlBuilder.AddParameter($"assetFilteringCriteria.locales[{item.i}].country", country);
            }
        }

        if (request.DifficultyLevel is not null)
        {
            foreach (var item in request.DifficultyLevel.Select((value, i) => new { i, value }))
            {
                urlBuilder.AddParameter($"assetFilteringCriteria.difficultyLevels[{item.i}]", item.value);
            }
        }

        requestMessage.RequestUri = new Uri(urlBuilder.ToString(), UriKind.RelativeOrAbsolute);

        HttpResponseMessage response = await this.Client
            .SendAsync(requestMessage, HttpCompletionOption.ResponseHeadersRead, cancellationToken)
            .ConfigureAwait(false);
        try
        {
            var headers = response.Headers.ToDictionary(h => h.Key, h => h.Value);
            int status = (int)response.StatusCode;
            if (status == 200)
            {
                ObjectResponseResult<LinkedInResponse<LearningAsset>> objectResponse =
                    await this.ReadObjectResponseAsync<LearningAsset>(response, headers, cancellationToken)
                        .ConfigureAwait(false);
                if (objectResponse.Object == null)
                {
                    throw new ApiException("Response was null which was not expected.", status, objectResponse.Text,
                        headers, null);
                }

                return objectResponse.Object;
            }
            else
            {
                string? responseData = response.Content == null
                    ? null
                    : await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);
                throw new ApiException("The HTTP status code of the response was not expected (" + status + ").",
                    status, responseData, headers, null);
            }
        }
        finally
        {
            response.Dispose();
        }
    }


    protected virtual async Task<ObjectResponseResult<LinkedInResponse<T>>> ReadObjectResponseAsync<T>(
        HttpResponseMessage response,
        IReadOnlyDictionary<string, IEnumerable<string>> headers,
        CancellationToken cancellationToken)
    {
        if (response == null || response.Content == null)
        {
            return new ObjectResponseResult<LinkedInResponse<T>>(default, string.Empty);
        }

        string responseText = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);
        try
        {
            LinkedInResponse<T>? typedBody = JsonConvert.DeserializeObject<LinkedInResponse<T>>(responseText);
            return new ObjectResponseResult<LinkedInResponse<T>>(typedBody, responseText);
        }
        catch (JsonException exception)
        {
            string message = "Could not deserialize the response body string as " + typeof(T).FullName + ".";
            throw new ApiException(message, (int)response.StatusCode, responseText, headers, exception);
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
}

