using System;
using System.Threading.Tasks;

namespace Tiny.Framework.Flow
{
    /// <summary>
    /// i provide safe operations.
    /// </summary>
    public interface IProvideSafeOperations
    {
        /// <summary>
        /// try...
        /// </summary>
        /// <param name="doAction">do action.</param>
        /// <param name="handleError">handle error (can be null, optional).</param>
        /// <returns>the currently running task.</returns>
        Task Try(Func<Task> doAction, Func<Exception, Task> handleError);

        /// <summary>
        /// try...
        /// </summary>
        /// <typeparam name="TResult">the return type for the action and the error.</typeparam>
        /// <param name="doAction">do action.</param>
        /// <param name="handleError">handle error (cannot be null).</param>
        /// <returns>the currently running task with the requested result type.</returns>
        Task<TResult> Try<TResult>(Func<Task<TResult>> doAction, Func<Exception, Task<TResult>> handleError);
    }
}
