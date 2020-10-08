//-----------------------------------------------------------------------------
// copyright (c) 2016, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
//-----------------------------------------------------------------------------
using System;

namespace Tiny.Framework.Flow.Internal
{
    /// <summary>
    /// the weak message action for types of T.
    /// </summary>
    /// <typeparam name="T">the type to act upon.</typeparam>
    /// <seealso cref="WeakAction{T}" />
    internal sealed class WeakMessageAction<T> :
        WeakAction<T>
        where T : class, IMediationMessage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WeakMessageAction{T}" /> class.
        /// </summary>
        /// <param name="action">the action.</param>
        /// <param name="uiListener">if set to <c>true</c> [UI listener].</param>
        internal WeakMessageAction(Action<T> action, bool uiListener)
            : base(action)
        {
            UIListener = uiListener;
        }

        /// <summary>
        /// gets a value indicating whether [UI listener].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [UI listener]; otherwise, <c>false</c>.
        /// </value>
        internal bool UIListener { get; private set; }
    }
}
