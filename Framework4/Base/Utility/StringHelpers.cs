//-----------------------------------------------------------------------------
// copyright (c) 2016, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
//-----------------------------------------------------------------------------
using System;

namespace Tiny.Framework.Utility
{
    /// <summary>
    /// a collection of routine grafts for strings.
    /// </summary>
    public static class StringHelpers
    {
        /// <summary>
        /// Compares with, provides a case insensitive string comparison.
        /// </summary>
        /// <param name="source">the source.</param>
        /// <param name="candidate">the candidate.</param>
        /// <returns>true or false.</returns>
        public static bool ComparesWith(this string source, string candidate)
        {
            return string.Compare(source, candidate, true) == 0;
        }

        /// <summary>
        /// the source holds the candidate using a case insensitive check
        /// this way it doesn't conflict with the many 'contains' knocking around.
        /// </summary>
        /// <param name="source">the source.</param>
        /// <param name="candidate">the candidate.</param>
        /// <returns>true or false.</returns>
        public static bool Holds(this string source, string candidate)
        {
            return source?.IndexOf(candidate, StringComparison.OrdinalIgnoreCase) >= 0;
        }
    }
}
