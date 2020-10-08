using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Routing;

namespace Tiny.Framework.Web.Shell.Registration
{
    /// <summary>
    /// Transforms route values to strings for use in URIs by slugifying the value.
    /// For example, an action "GetValue" will be represented in the URI as "get-value".
    /// See <see href="https://docs.microsoft.com/en-us/aspnet/core/mvc/controllers/routing?view=aspnetcore-3.1#use-a-parameter-transformer-to-customize-token-replacement"/>.
    /// 'inherited' code, should be in a common library.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public sealed class SlugifyParameterTransformer :
        IOutboundParameterTransformer
    {
        private readonly Regex parameterPattern = new Regex("([a-z])([A-Z])", RegexOptions.Compiled);

        /// <inheritdoc/>
        public string TransformOutbound(object value)
        {
            if (value == null)
            {
                return null;
            }

            return parameterPattern.Replace(value.ToString(), "$1-$2").ToLower();
        }
    }
}