//-----------------------------------------------------------------------------
// copyright (c) 2016, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
//-----------------------------------------------------------------------------
using System;

namespace Tiny.Framework.Flow
{
    /// <summary>
    /// i synchronise actions
    /// this is a cloaking device for Auto Reset Events.
    /// </summary>
    public interface ISynchroniseActions
    {
        /// <summary>
        /// gets a value indicating whether this instance is synchronising.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is synchronising; otherwise, <c>false</c>.
        /// </value>
        bool IsSynchronising { get; }

        /// <summary>
        /// gets a value indicating whether this instance is waiting.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is waiting; otherwise, <c>false</c>.
        /// </value>
        bool IsWaiting { get; }

        /// <summary>
        /// gets a value indicating whether this instance is closed.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is closed; otherwise, <c>false</c>.
        /// </value>
        bool IsClosed { get; }

        /// <summary>
        /// Finishes this instance.
        /// </summary>
        /// <returns>if it works.</returns>
        bool Finish();

        /// <summary>
        /// Starts this instance.
        /// </summary>
        /// <returns>if it's in a runnable state and it works.</returns>
        bool Start();

        /// <summary>
        /// Waits the till ready.
        /// </summary>
        /// <returns>true once the auto reset event is complete.</returns>
        bool WaitTillReady();

        /// <summary>
        /// Consolidated race condition queuing operation.
        /// </summary>
        /// <param name="actionDo">the action to do.</param>
        void Update(Action actionDo);
    }
}
