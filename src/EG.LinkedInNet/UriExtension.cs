namespace EG.LinkedInNet;

using System.Text;

public static class UriExtension
{
    public static StringBuilder AddParameter(this StringBuilder builder, string name, object? value)
    {
        return builder.Append(Uri.EscapeDataString(name) + "=").Append(Uri.EscapeDataString(ConvertToString(value, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
    }

    private static string ConvertToString(object? value, System.Globalization.CultureInfo cultureInfo)
    {
        switch (value)
        {
            case null:
                return "";
            case Enum:
            {
                var name = Enum.GetName(value.GetType(), value);
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

                    var converted = Convert.ToString(Convert.ChangeType(value, Enum.GetUnderlyingType(value.GetType()), cultureInfo));
                    return converted == null ? string.Empty : converted;
                }

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
                    var array = Enumerable.OfType<object>((Array) value);
                    return string.Join(",", Enumerable.Select(array, o => ConvertToString(o, cultureInfo)));
                }

                break;
            }
        }

        var result = Convert.ToString(value, cultureInfo);
        return result == null ? "" : result;
    }
}
