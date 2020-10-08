//-----------------------------------------------------------------------------
// copyright (c) 2020, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
//-----------------------------------------------------------------------------
using System;

namespace Tiny.Framework.Utility
{
    /// <summary>
    /// Contains methods to guard against invalid input.
    /// </summary>
    public static class FireAction
    {
        /// <summary>
        /// the fire action result.
        /// </summary>
        public enum FireActionResult
        {
            /// <summary>
            /// action not executed.
            /// </summary>
            NotExecuted,

            /// <summary>
            /// action executed.
            /// </summary>
            Executed,
        }

        /// <summary>
        /// then do.
        /// </summary>
        /// <param name="meetsEvaluation">if set to <c>true</c> [failed evaluation].</param>
        /// <param name="doAction">the action.</param>
        /// <returns>the fire ation result.</returns>
        public static FireActionResult ThenDo(this bool meetsEvaluation, Action doAction)
        {
            if (meetsEvaluation)
            {
                doAction();
                return FireActionResult.Executed;
            }

            return FireActionResult.NotExecuted;
        }

        /// <summary>
        /// else do.
        /// </summary>
        /// <param name="lastActionresult">the last action result.</param>
        /// <param name="doAction">do action.</param>
        public static void ElseDo(this FireActionResult lastActionresult, Action doAction)
        {
            if (lastActionresult == FireActionResult.NotExecuted)
            {
                doAction();
                return;
            }

            throw new InvalidOperationException($"fire action result failed to execute method {doAction.Method.Name}");
        }
    }
}
