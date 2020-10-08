// -----------------------------------------------------------------------------
// copyright (c) 2016, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
// -----------------------------------------------------------------------------
using System;
using Tiny.Framework.Models;

namespace Tiny.Framework.Factories
{
    /// <summary>
    /// i create relay commands
    /// </summary>
    public interface ICreateRelayCommands
    {
        /// <summary>
        /// Creates the specified execute.
        /// </summary>
        /// <param name="execute">The execute.</param>
        /// <param name="canExecute">The can execute.</param>
        /// <returns>a relay command</returns>
        IRelayCommand Create(Action execute, Func<bool> canExecute = null);

        /// <summary>
        /// Creates the specified execute.
        /// </summary>
        /// <typeparam name="TCommandParameter">The type of the command parameter.</typeparam>
        /// <param name="execute">The execute.</param>
        /// <param name="canExecute">The can execute.</param>
        /// <returns>
        /// a relay command
        /// </returns>
        IRelayCommand Create<TCommandParameter>(Action<TCommandParameter> execute, Func<bool> canExecute);
    }
}
