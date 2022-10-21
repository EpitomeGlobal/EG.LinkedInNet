namespace EG.LinkedInNet;

using System.Globalization;
using System.Text;

public static class UriExtension
{
    public static StringBuilder AddParameter(this StringBuilder builder, string name, object? value)
    {
        return value is null
            ? builder
            : builder.Append(Uri.EscapeDataString(name) + "=")
                .Append(Uri.EscapeDataString(ConvertToString(value, CultureInfo.InvariantCulture))).Append("&");
    }

    private static string ConvertToString(object? value, CultureInfo cultureInfo)
    {
        switch (value)
        {
            case null:
                return "";
            case Enum:
            {
                return value.ToString();
                break;
            }
            case bool b:
                return Convert.ToString(b, cultureInfo).ToLowerInvariant();
            case byte[] bytes:
                return Convert.ToBase64String(bytes);
            default:
            {
                if (value.GetType().IsArray)
                {
                    IEnumerable<object> array = ((Array)value).OfType<object>();
                    return string.Join(",", array.Select(o => ConvertToString(o, cultureInfo)));
                }

                break;
            }
        }

        string? result = Convert.ToString(value, cultureInfo);
        return result == null ? "" : result;
    }
}
