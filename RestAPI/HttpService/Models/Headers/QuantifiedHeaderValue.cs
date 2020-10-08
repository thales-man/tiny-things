//-----------------------------------------------------------------------------
// copyright (c) 2016, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
//-----------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Http.Service.Model.Headers
{

    /// <summary>
    /// a quantified header value
    /// </summary>
    public class QuantifiedHeaderValue
    {

        /// <summary>
        /// The qualit y_ key
        /// </summary>
        private static string QUALITY_KEY = "q";

        /// <summary>
        /// Gets the header value.
        /// </summary>
        internal string HeaderValue { get; }

        /// <summary>
        /// Gets the quantifiers.
        /// </summary>
        internal IDictionary<string, string> Quantifiers { get; }

        /// <summary>
        /// Gets the quality.
        /// </summary>
        internal decimal Quality { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="QuantifiedHeaderValue"/> class.
        /// </summary>
        /// <param name="headerValue">The header value.</param>
        /// <param name="quantifiers">The quantifiers.</param>
        public QuantifiedHeaderValue(string headerValue, IDictionary<string, string> quantifiers)
        {
            HeaderValue = headerValue;
            Quantifiers = quantifiers;
            ExtractQuality();
        }

        /// <summary>
        /// Extracts the quality.
        /// </summary>
        private void ExtractQuality()
        {
            Quality = 0;
            if (Quantifiers.ContainsKey(QUALITY_KEY))
            {
                decimal qualityAsDec;
                if (decimal.TryParse(Quantifiers[QUALITY_KEY], out qualityAsDec))
                {
                    Quality = Math.Min(Math.Max(qualityAsDec, 0), 1);
                }
            }
            else
            {
                Quality = 1;
            }
        }

        /// <summary>
        /// Finds the quantifier value.
        /// </summary>
        /// <param name="quantifierKey">The quantifier key.</param>
        /// <returns>a string</returns>
        internal string FindQuantifierValue(string quantifierKey)
        {
            if (Quantifiers.ContainsKey(quantifierKey))
            {
                return Quantifiers[quantifierKey];
            }
            return null;
        }
    }
}
