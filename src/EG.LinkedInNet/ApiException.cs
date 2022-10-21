namespace EG.LinkedInNet;

public class ApiException : Exception
{
    public ApiException(string message, int statusCode, string response,
        IReadOnlyDictionary<string, IEnumerable<string>> headers, Exception innerException)
        : base(
            message + "\n\nStatus: " + statusCode + "\nResponse: \n" + (response == null
                ? "(null)"
                : response.Substring(0, response.Length >= 512 ? 512 : response.Length)), innerException)
    {
        this.StatusCode = statusCode;
        this.Response = response;
        this.Headers = headers;
    }

    public int StatusCode { get; }

    public string Response { get; }

    public IReadOnlyDictionary<string, IEnumerable<string>> Headers { get; }

    public override string ToString()
    {
        return string.Format("HTTP Response: \n\n{0}\n\n{1}", this.Response, base.ToString());
    }
}

public class ApiException<TResult> : ApiException
{
    public ApiException(string message, int statusCode, string response,
        IReadOnlyDictionary<string, IEnumerable<string>> headers, TResult result, Exception innerException)
        : base(message, statusCode, response, headers, innerException)
    {
        this.Result = result;
    }

    public TResult Result { get; }
}
