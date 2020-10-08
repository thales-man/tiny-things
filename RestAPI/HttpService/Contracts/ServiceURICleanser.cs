//-----------------------------------------------------------------------------
// copyright (c) 2016, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
//-----------------------------------------------------------------------------
using Tiny.Framework.Utility;
using System;

namespace Http.Service.Contract
{
    public static class ServiceURICleanser
    {
        /// <summary>
        /// Clean the specified uri.
        /// </summary>
        /// <returns>The clean.</returns>
        /// <param name="uri">URI.</param>
        public static string Clean(this string uri)
        {
            return It.IsEmpty(uri) ? uri : uri.Trim('/');
        }

        /// <summary>
        /// To regex compatible
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <returns>a regex compatible character 'escaped' string</returns>
        public static string ToRegexCompatible(this string uri)
        {
            return It.IsEmpty(uri) ? uri : uri.Replace("?", "\\?");
        }

        /// <summary>
        /// To relative string
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <returns>returns 'fixed' relative uri string</returns>
        public static string ToServicePrefix(this string uri) =>
            It.IsEmpty(uri) ? string.Empty : $"/{uri.Clean()}";
    }
}
