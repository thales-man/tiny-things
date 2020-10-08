//-----------------------------------------------------------------------------
// copyright (c) 2016, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
//-----------------------------------------------------------------------------
namespace Tiny.Framework.Flow
{
    /// <summary>
    /// the message 'handler' contract.
    /// </summary>
    /// <typeparam name="T">the type of payload being handled in the message.</typeparam>
    public interface IHandleMessageMediation<in T>
        where T : IMediationMessage
    {
        /// <summary>
        /// gets or sets the mediator.
        /// this property will require the composition import decorator.
        /// </summary>
        IManageMessageMediation Mediator { get; set; }

        /// <summary>
        /// Registers the message handler.
        /// </summary>
        void RegisterMessageHandler();

        /// <summary>
        /// Handles the message.
        /// </summary>
        /// <param name="message">the message.</param>
        void HandleMessage(T message);
    }
}
