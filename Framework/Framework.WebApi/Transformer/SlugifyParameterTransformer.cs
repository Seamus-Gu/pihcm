using Microsoft.AspNetCore.Routing;
using System.Text.RegularExpressions;

namespace Framework.WebApi
{
    public class SlugifyParameterTransformer : IOutboundParameterTransformer
    {
        public string? TransformOutbound(object? value)
        {
            if (value == null) { return null; }

            var str = value.ToString();

            if (string.IsNullOrEmpty(str)) { return string.Empty; }
            return Regex.Replace(str,
                                 "([a-z])([A-Z])",
                                 "$1-$2",
                                 RegexOptions.CultureInvariant,
                                 TimeSpan.FromMilliseconds(100)).ToLowerInvariant();
        }
    }
}