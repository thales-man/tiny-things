// -----------------------------------------------------------------------------
// copyright (c) 2016, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
// -----------------------------------------------------------------------------
using System;
using System.Composition;
using Tiny.Framework.Models;
using Tiny.Framework.Models.Internal;

namespace Tiny.Framework.Factories.Internal
{
    /// <summary>
    /// the relay command 'factory', injected into viewmodels
    /// </summary>
    /// <seealso cref="IRelayCommand" />
    /// <seealso cref="ICreateRelayCommands" />
    [Shared]
    [Export(typeof(ICreateRelayCommands))]
    internal sealed class RelayCommandFactory :
        ICreateRelayCommands
    {
        /// <summary>
        /// Creates the specified execute.
        /// </summary>
        /// <param name="execute">The execute.</param>
        /// <param name="canExecute">The can execute.</param>
        /// <returns>
        /// a relay command
        /// </returns>
        IRelayCommand ICreateRelayCommands.Create(Action execute, Func<bool> canExecute)
        {
            return CommandFactory.Create(execute, canExecute);
        }

        /// <summary>
        /// Creates the specified execute.
        /// </summary>
        /// <typeparam name="TCommandParameter">The type of the command parameter.</typeparam>
        /// <param name="execute">The execute.</param>
        /// <param name="canExecute">The can execute.</param>
        /// <returns>
        /// a relay command
        /// </returns>
        public IRelayCommand Create<TCommandParameter>(Action<TCommandParameter> execute, Func<bool> canExecute)
        {
            return new RelayCommand<TCommandParameter>(execute, canExecute);
        }
    }
}
