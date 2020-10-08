// -----------------------------------------------------------------------------
// copyright (c) 2020, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
// -----------------------------------------------------------------------------
using System;
using System.Threading.Tasks;

namespace Tiny.Framework.Web.Factories
{
    /// <summary>
    /// I logging context scope.
    /// </summary>
    public interface ILoggingContextScope :
        IDisposable
    {
        /// <summary>
        /// Log...
        /// </summary>
        /// <param name="message">the message.</param>
        /// <returns>the currently running task.</returns>
        Task Log(string message);

        /// <summary>
        /// Log...
        /// </summary>
        /// <param name="exception">the exception.</param>
        /// <returns>the currently running task.</returns>
        Task Log(Exception exception);
    }
}