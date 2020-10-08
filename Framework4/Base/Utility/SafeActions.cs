//-----------------------------------------------------------------------------
// copyright (c) 2019, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
//-----------------------------------------------------------------------------
using System;
using System.Threading.Tasks;

namespace Tiny.Framework.Utility
{
    /// <summary>
    /// Safe actions.
    /// </summary>
    public static class SafeActions
    {
        /// <summary>
        /// Try the specified action and handler.
        /// </summary>
        /// <param name="action">Action.</param>
        /// <param name="handler">Handler.</param>
        public static void Try(Action action, Action<Exception> handler = null)
        {
            try
            {
                action?.Invoke();
            }
            catch (Exception e)
            {
                handler?.Invoke(e);
            }
        }

        /// <summary>
        /// Try the specified action and handler.
        /// </summary>
        /// <returns>the try.</returns>
        /// <param name="action">Action.</param>
        /// <param name="handler">Handler.</param>
        public static async Task Try(Func<Task> action, Func<Exception, Task> handler = null)
        {
            try
            {
                await action?.Invoke();
            }
            catch (Exception e)
            {
                await handler?.Invoke(e);
            }
        }

        /// <summary>
        /// Try the specified action and handler.
        /// </summary>
        /// <returns>the try.</returns>
        /// <typeparam name="TResult">the result type.</typeparam>
        /// <param name="action">Action.</param>
        /// <param name="handler">Handler.</param>
        /// <returns>the result of the action or an error.</returns>
        public static async Task<TResult> Try<TResult>(Func<Task<TResult>> action, Func<Exception, Task<TResult>> handler = null)
        {
            try
            {
                return await action?.Invoke();
            }
            catch (Exception e)
            {
                return await handler?.Invoke(e);
            }
        }

        /// <summary>
        /// Tries the specified action.
        /// </summary>
        /// <typeparam name="TResult">the type of the result.</typeparam>
        /// <param name="action">the action.</param>
        /// <param name="handler">the error handler.</param>
        /// <returns>the result of the action or an error.</returns>
        public static TResult Try<TResult>(Func<TResult> action, Action<Exception> handler = null)
        {
            try
            {
                return action.Invoke();
            }
            catch (Exception e)
            {
                handler?.Invoke(e);
                return default;
            }
        }

        /// <summary>
        /// Try the specified action and handler.
        /// </summary>
        /// <typeparam name="TResult">the type of the result.</typeparam>
        /// <param name="action">the action.</param>
        /// <param name="handler">the error handler.</param>
        /// <returns>the result of the action or an error.</returns>
        public static TResult Try<TResult>(Func<TResult> action, Func<TResult> handler)
        {
            try
            {
                return action.Invoke();
            }
            catch
            {
                return handler.Invoke();
            }
        }
    }
}
