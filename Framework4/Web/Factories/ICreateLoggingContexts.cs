// -----------------------------------------------------------------------------
// copyright (c) 2020, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
// -----------------------------------------------------------------------------
namespace Tiny.Framework.Web.Factories
{
    /// <summary>
    /// i create logging contexts.
    /// </summary>
    public interface ICreateLoggingContexts
    {
        /// <summary>
        /// begin logging for...
        /// </summary>
        /// <param name="scope">the scope.</param>
        /// <returns>a logging scope.</returns>
        ILoggingContextScope BeginLoggingFor(string scope);
    }
}