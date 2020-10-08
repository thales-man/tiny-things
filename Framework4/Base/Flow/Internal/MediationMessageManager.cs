//-----------------------------------------------------------------------------
// copyright (c) 2016, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
//-----------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Composition;
using System.Linq;
using Tiny.Framework.Registration;
using Tiny.Framework.Utility;

namespace Tiny.Framework.Flow.Internal
{
    /// <summary>
    /// the mediator class.
    /// </summary>
    [Shared]
    [Export(typeof(IManageMessageMediation))]
    internal sealed class MediationMessageManager :
        IManageMessageMediation,
        ISupportServiceRegistration
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MediationMessageManager"/> class.
        /// </summary>
        /// <param name="dispatcher">the dispatcher.</param>
        public MediationMessageManager(IDeviceAdapter dispatcher)
        {
            It.IsNull(dispatcher)
                .AsGuard<ArgumentNullException>(nameof(dispatcher));

            Dispatcher = dispatcher;
        }

        /// <summary>
        /// gets the actions.
        /// </summary>
        internal IDictionary<Type, MediatorActionList> Actions { get; }
            = new Dictionary<Type, MediatorActionList>();

        /// <summary>
        /// gets the dispatcher.
        /// </summary>
        internal IDeviceAdapter Dispatcher { get; }

        /// <summary>
        /// Registers the specified callback.
        /// </summary>
        /// <typeparam name="T">the message type.</typeparam>
        /// <param name="callback">the callback.</param>
        /// <param name="isViewModel">if set to <c>true</c> [is view model].</param>
        public void Register<T>(Action<T> callback, bool isViewModel = false)
            where T : class, IMediationMessage
        {
            var thisType = typeof(T);
            var newAction = MakeAction(callback, isViewModel);

            if (!Actions.ContainsKey(typeof(T)))
            {
                Actions.Add(thisType, new MediatorActionList());
            }

            Actions[thisType].Add(newAction);
        }

        /// <summary>
        /// Publishes the specified message.
        /// </summary>
        /// <typeparam name="T">the message type.</typeparam>
        /// <param name="message">the message.</param>
        public void Publish<T>(T message)
            where T : class, IMediationMessage
        {
            var thisType = typeof(T);

            if (Actions.Any(x => x.Key == thisType))
            {
                Actions[thisType].ForEach(x =>
                {
                    var action = x as WeakMessageAction<T>;

                    if (action.IsAlive)
                    {
                        if (action.UIListener)
                        {
                            Dispatch(() => action.Do(message));
                            return;
                        }

                        action.Do(message);
                        return;
                    }

                    Dispatch(() => Unregister<T>(x));
                });
            }
        }

        /// <summary>
        /// Dispatch the Action.
        /// </summary>
        /// <param name="theAction">the action.</param>
        private void Dispatch(Action theAction)
        {
            Dispatcher.Begin(theAction);
        }

        /// <summary>
        /// Makes the action.
        /// </summary>
        /// <typeparam name="T">the type to act upon.</typeparam>
        /// <param name="caller">the caller.</param>
        /// <param name="listener">if set to <c>true</c> [listener].</param>
        /// <returns>the action of T.</returns>
        private object MakeAction<T>(Action<T> caller, bool listener)
            where T : class, IMediationMessage =>
            new WeakMessageAction<T>(caller, listener);

        /// <summary>
        /// Unregisters the specified reference.
        /// </summary>
        /// <typeparam name="T">the type to unregister.</typeparam>
        /// <param name="reference">the reference.</param>
        private void Unregister<T>(object reference)
        {
            Actions[typeof(T)].Remove(reference);
        }
    }
}
