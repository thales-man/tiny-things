using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Tiny.Framework.Utility;

namespace Http.Service.Models.Boundaries
{
    /// <summary>
    /// Query parameters.
    /// </summary>
    internal static class ParametersFromQuery
    {
        /// <summary>
        /// The query string regex.
        /// </summary>
        private static readonly Regex queryStringRegex;

        /// <summary>
        /// Initializes the <see cref="T:Http.Service.Models.Boundaries.QueryParameters"/> class.
        /// </summary>
        static ParametersFromQuery()
        {
            queryStringRegex = new Regex(@"[\?&](?<name>[^&=]+)=(?<value>[^&=]+)", RegexOptions.Compiled);
        }

        private static IEnumerable<KeyValuePair<string, string>> ParseQuery(string candidate)
        {
            It.IsEmpty(candidate)
                .AsGuard<ArgumentNullException>(nameof(candidate));

            var matches = queryStringRegex.Matches(candidate);
            for (int i = 0; i < matches.Count; i++)
            {
                var match = matches[i];
                yield return new KeyValuePair<string, string>(match.Groups["name"].Value, match.Groups["value"].Value);
            }
        }

        internal static IDictionary<string, string> Get(string candidate) =>
            ParseQuery(candidate).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
    }
}
