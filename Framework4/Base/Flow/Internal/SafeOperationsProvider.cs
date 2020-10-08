//-----------------------------------------------------------------------------
// copyright (c) 2016, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
//-----------------------------------------------------------------------------
using System;
using System.Composition;
using System.Threading.Tasks;
using Tiny.Framework.Registration;

namespace Tiny.Framework.Flow.Internal
{
    /// <summary>
    /// the safe operations providers.
    /// </summary>
    [Shared]
    [Export(typeof(IProvideSafeOperations))]
    internal sealed class SafeOperationsProvider :
        IProvideSafeOperations,
        ISupportServiceRegistration
    {
        /// <summary>
        /// try...
        /// </summary>
        /// <param name="doAction">do action.</param>
        /// <param name="handleError">handle error (can be null, optional).</param>
        /// <returns>the currently running task.</returns>
        public async Task Try(Func<Task> doAction, Func<Exception, Task> handleError)
        {
            try
            {
                await doAction();
            }
            catch (Exception e)
            {
                await handleError?.Invoke(e);
            }
        }

        /// <summary>
        /// try...
        /// </summary>
        /// <typeparam name="TResult">the return type for the action and the error.</typeparam>
        /// <param name="doAction">do action.</param>
        /// <param name="handleError">handle error (cannot be null).</param>
        /// <returns>the currently running task with the requested result type.</returns>
        public async Task<TResult> Try<TResult>(Func<Task<TResult>> doAction, Func<Exception, Task<TResult>> handleError)
        {
            try
            {
                return await doAction();
            }
            catch (Exception e)
            {
                return await handleError(e);
            }
        }
    }
}
