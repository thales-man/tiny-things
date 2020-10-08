//-----------------------------------------------------------------------------
// copyright (c) 2016, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
//-----------------------------------------------------------------------------
using System;

namespace Tiny.Framework.Flow
{
    /// <summary>
    /// i manage timed actions
    /// this is a cloaking device for a system timer.
    /// </summary>
    public interface IManageTimedActions
    {
        /// <summary>
        /// Sets the invocation interval.
        /// </summary>
        /// <param name="seconds">the seconds.</param>
        void SetInvocationInterval(int seconds);

        /// <summary>
        /// Sets the managed action.
        /// </summary>
        /// <param name="managedAction">the managed action.</param>
        void SetManagedAction(Action managedAction);

        /// <summary>
        /// Starts this instance.
        /// </summary>
        void Start();

        /// <summary>
        /// Starts this instance.
        /// </summary>
        /// <param name="seconds">wait period.</param>
        /// <param name="managedAction">the maanged action.</param>
        void Start(int seconds, Action managedAction);

        /// <summary>
        /// Stops this instance.
        /// </summary>
        void Stop();

        /// <summary>
        /// Finisheds this instance.
        /// </summary>
        void Finished();
    }
}
