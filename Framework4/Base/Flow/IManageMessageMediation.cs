//-----------------------------------------------------------------------------
// copyright (c) 2016, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
//-----------------------------------------------------------------------------
using System;

namespace Tiny.Framework.Flow
{
    /// <summary>
    /// the manage event publication (subscription and publication).
    /// </summary>
    public interface IManageMessageMediation
    {
        /// <summary>
        /// Registers the specified callback.
        /// </summary>
        /// <typeparam name="T">the registration action type.</typeparam>
        /// <param name="callback">the callback.</param>
        /// <param name="isViewModel">if set to <c>true</c> [is view model].</param>
        void Register<T>(Action<T> callback, bool isViewModel = false)
            where T : class, IMediationMessage;

        /// <summary>
        /// Publishes the specified message.
        /// </summary>
        /// <typeparam name="T">the message type.</typeparam>
        /// <param name="message">the message.</param>
        void Publish<T>(T message)
            where T : class, IMediationMessage;
    }
}
