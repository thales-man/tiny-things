//-----------------------------------------------------------------------------
// copyright (c) 2016, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
//-----------------------------------------------------------------------------
using System;
using System.Runtime.CompilerServices;

namespace Tiny.Framework.Utility
{
    /// <summary>
    /// Contains methods to guard against invalid input.
    /// </summary>
    public static class Guard
    {
        /// <summary>
        /// As guard.
        /// </summary>
        /// <typeparam name="TExceptionType">the type of the exception type.</typeparam>
        /// <param name="failedEvaluation">if set to <c>true</c> [failed evaluation].</param>
        /// <param name="source">the source.</param>
        /// <param name="callerName">Name of the caller.</param>
        public static void AsGuard<TExceptionType>(this bool failedEvaluation, string source = null, [CallerMemberName] string callerName = null)
            where TExceptionType : Exception
        {
            if (failedEvaluation)
            {
                throw GetException<TExceptionType>(source ?? $"an item in this routine ({callerName}) was invalid");
            }
        }

        /// <summary>
        /// gets the exception.
        /// </summary>
        /// <typeparam name="TExceptionType">the type of the exception type.</typeparam>
        /// <param name="args">the arguments.</param>
        /// <returns><see cref="Exception"/>.</returns>
        private static Exception GetException<TExceptionType>(params string[] args)
            where TExceptionType : Exception
        {
            return (Exception)Activator.CreateInstance(typeof(TExceptionType), args);
        }
    }
}
