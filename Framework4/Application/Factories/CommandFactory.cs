// -----------------------------------------------------------------------------
// copyright (c) 2016, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
// -----------------------------------------------------------------------------
using System;
using Tiny.Framework.Models;
using Tiny.Framework.Models.Internal;

namespace Tiny.Framework.Factories
{
    /// <summary>
    /// the (relay) command 'factory' for use in xaml resources / behaviours
    /// </summary>
    /// <seealso cref="IRelayCommand" />
    /// <seealso cref="ICreateRelayCommands" />
    public static class CommandFactory
    {
        /// <summary>
        /// Creates the specified execute.
        /// </summary>
        /// <param name="execute">execute.</param>
        /// <param name="canExecute">can execute.</param>
        /// <returns>
        /// a relay command
        /// </returns>
        public static IRelayCommand Create(Action execute, Func<bool> canExecute)
        {
            return new RelayCommand(execute, canExecute);
        }
    }
}
