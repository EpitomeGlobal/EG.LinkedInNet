using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using EG.LinkedInNet.Models;
using Microsoft.Extensions.Options;

namespace EG.LinkedInNet;

public class LinkedInClient
{
    private readonly string? baseUrl;

    /// <summary>
    /// Initializes a new instance of the <see cref="LinkedInClient"/> class.
    /// </summary>
    /// <param name="httpClient">Internal http client.</param>
    /// <param name="options">Linked in config.</param>
    public LinkedInClient(HttpClient httpClient, IOptions<LinkedInConfiguration> options) => (this.Client, this.baseUrl) = (httpClient, options.Value.ApiEndpoint);
    
    /// <summary>
    /// Gets http client.
    /// </summary>
    public HttpClient Client { get; }
    
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
        
        using (var request = new HttpRequestMessage())
        {
            var disposeResponse_ = true;
            
            var urlBuilder_ = new System.Text.StringBuilder();
            urlBuilder_.Append(baseUrl != null ? baseUrl.TrimEnd('/') : "").Append("/v2/learningClassifications?");
            if (!string.IsNullOrEmpty(query))
            {
                urlBuilder_.Append(Uri.EscapeDataString("q") + "=").Append(Uri.EscapeDataString(ConvertToString(query, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
            }
            if (!string.IsNullOrEmpty(keyword))
            {
                urlBuilder_.Append(Uri.EscapeDataString("keyword") + "=").Append(Uri.EscapeDataString(ConvertToString(keyword, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
            }
            if (!string.IsNullOrEmpty(country))
            {
                urlBuilder_.Append(Uri.EscapeDataString("targetLocale.country") + "=").Append(Uri.EscapeDataString(ConvertToString(country, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
            }
            if (!string.IsNullOrEmpty(language))
            {
                urlBuilder_.Append(Uri.EscapeDataString("targetLocale.language") + "=").Append(Uri.EscapeDataString(ConvertToString(language, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
            }
            urlBuilder_.Append(Uri.EscapeDataString("start") + "=").Append(Uri.EscapeDataString(ConvertToString(start, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
            urlBuilder_.Append(Uri.EscapeDataString("count") + "=").Append(Uri.EscapeDataString(ConvertToString(count, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
            urlBuilder_.Append("fields=name,type,urn");
            var url = urlBuilder_.ToString();
            request.RequestUri = new System.Uri(url, System.UriKind.RelativeOrAbsolute);
            
            var response = await this.Client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
            try
            {
                var headers_ = System.Linq.Enumerable.ToDictionary(response.Headers, h_ => h_.Key, h_ => h_.Value);
                if (response.Content != null && response.Content.Headers != null)
                {
                    foreach (var item_ in response.Content.Headers)
                        headers_[item_.Key] = item_.Value;
                }
                var status = (int)response.StatusCode;
                if (status == 200)
                {
                    var objectResponse_ = await ReadObjectResponseAsync<IList<Classification>>(response, headers_, cancellationToken).ConfigureAwait(false);
                    if (objectResponse_.Object == null)
                    {
                        throw new ApiException("Response was null which was not expected.", status, objectResponse_.Text, headers_, null);
                    }
                    return objectResponse_.Object;
                }
                else
                {
                    var responseData_ = response.Content == null ? null : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    throw new ApiException("The HTTP status code of the response was not expected (" + status + ").", status, responseData_, headers_, null);
                }
            } 
            finally
            {
                if (disposeResponse_)
                    response.Dispose();
            }
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
    
    protected struct WrappedResponse<T>
    {
        public T Elements { get; }
    }
    
    private string ConvertToString(object value, System.Globalization.CultureInfo cultureInfo)
    {
        if (value == null)
        {
            return "";
        }

        if (value is System.Enum)
        {
            var name = System.Enum.GetName(value.GetType(), value);
            if (name != null)
            {
                var field = System.Reflection.IntrospectionExtensions.GetTypeInfo(value.GetType()).GetDeclaredField(name);
                if (field != null)
                {
                    var attribute = System.Reflection.CustomAttributeExtensions.GetCustomAttribute(field, typeof(System.Runtime.Serialization.EnumMemberAttribute))
                        as System.Runtime.Serialization.EnumMemberAttribute;
                    if (attribute != null)
                    {
                        return attribute.Value != null ? attribute.Value : name;
                    }
                }

                var converted = System.Convert.ToString(System.Convert.ChangeType(value, System.Enum.GetUnderlyingType(value.GetType()), cultureInfo));
                return converted == null ? string.Empty : converted;
            }
        }
        else if (value is bool)
        {
            return System.Convert.ToString((bool)value, cultureInfo).ToLowerInvariant();
        }
        else if (value is byte[])
        {
            return System.Convert.ToBase64String((byte[]) value);
        }
        else if (value.GetType().IsArray)
        {
            var array = System.Linq.Enumerable.OfType<object>((System.Array) value);
            return string.Join(",", System.Linq.Enumerable.Select(array, o => ConvertToString(o, cultureInfo)));
        }

        var result = System.Convert.ToString(value, cultureInfo);
        return result == null ? "" : result;
    }
    
    protected virtual async Task<ObjectResponseResult<T>> ReadObjectResponseAsync<T>(
        HttpResponseMessage response,
        IReadOnlyDictionary<string, IEnumerable<string>> headers, 
        CancellationToken cancellationToken)
    {
        if (response == null || response.Content == null)
        {
            return new ObjectResponseResult<T>(default(T), string.Empty);
        }

        if (ReadResponseAsString)
        {
            var responseText = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);
            try
            {
                var typedBody = JsonSerializer.Deserialize<WrappedResponse<T>>(responseText);
                return new ObjectResponseResult<T>(typedBody.Elements, responseText);
            }
            catch (JsonException exception)
            {
                var message = "Could not deserialize the response body string as " + typeof(T).FullName + ".";
                throw new ApiException(message, (int)response.StatusCode, responseText, headers, exception);
            }
        }
        else
        {
            try
            {
                using (var responseStream = await response.Content.ReadAsStreamAsync(cancellationToken).ConfigureAwait(false))
                using (var streamReader = new System.IO.StreamReader(responseStream))
                {
                    var typedBody = JsonSerializer.Deserialize<WrappedResponse<T>>(await streamReader.ReadToEndAsync());
                    return new ObjectResponseResult<T>(typedBody.Elements, string.Empty);
                }
            }
            catch (JsonException exception)
            {
                var message = "Could not deserialize the response body stream as " + typeof(T).FullName + ".";
                throw new ApiException(message, (int)response.StatusCode, string.Empty, headers, exception);
            }
        }
    }

}