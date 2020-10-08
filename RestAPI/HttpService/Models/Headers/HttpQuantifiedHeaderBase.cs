//-----------------------------------------------------------------------------
// copyright (c) 2016, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
//-----------------------------------------------------------------------------
using System.Collections.Generic;
using System.Linq;

namespace Http.Service.Model.Headers
{

    /// <summary>
    /// the quantified header base, splits up quality headers
    /// </summary>
    public abstract class HttpQuantifiedHeaderBase
    {

        /// <summary>
        /// Extracts the quantified headers.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>a collection of quality headers</returns>
        public IEnumerable<QuantifiedHeaderValue> ExtractQuantifiedHeaders(string value)
        {
            var headerValues = value.Split(',');
            var quantifiedValues = headerValues.Select(h => ExtractQuantifiedHeader(h));
            return quantifiedValues.OrderByDescending(q => q.Quality).ToArray();
        }

        /// <summary>
        /// Extracts the quantified header.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>a quality header value</returns>
        public QuantifiedHeaderValue ExtractQuantifiedHeader(string value)
        {
            string headerValue = null;
            var extractedQuantifiers = new Dictionary<string, string>();
            var quantifiers = value.Split(';');
            if (quantifiers.Length > 0)
            {
                headerValue = quantifiers[0].Trim();
            }
            if (quantifiers.Length > 1)
            {
                foreach (var quantifier in quantifiers.Skip(1))
                {
                    var parts = quantifier.Split('=');
                    if (parts.Length > 1)
                    {
                        string qKey = parts[0].Trim();
                        string qValue = parts[1].Trim();
                        extractedQuantifiers.Add(qKey, qValue);
                    }
                }
            }
            return new QuantifiedHeaderValue(headerValue, extractedQuantifiers);
        }
    }
}
