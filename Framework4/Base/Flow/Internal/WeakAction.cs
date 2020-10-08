//-----------------------------------------------------------------------------
// copyright (c) 2016, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
//-----------------------------------------------------------------------------
using System;

namespace Tiny.Framework.Flow.Internal
{
    /// <summary>
    /// a weak referenced action.
    /// </summary>
    internal sealed class WeakAction :
        WeakActionReferenceBase
    {
        /// <summary>
        /// the _action.
        /// </summary>
        private readonly Action _action;

        /// <summary>
        /// Initializes a new instance of the <see cref="WeakAction"/> class.
        /// </summary>
        /// <param name="action">the action.</param>
        public WeakAction(Action action)
            : base(action.Target.GetType().DeclaringType, action.Target)
        {
            _action = action;
        }

        /// <summary>
        /// gets the do part of the weak action.
        /// </summary>
        public Action Do
        {
            get { return IsAlive ? _action : null; }
        }
    }
}
