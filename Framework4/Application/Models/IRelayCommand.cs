// -----------------------------------------------------------------------------
// copyright (c) 2016, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
// -----------------------------------------------------------------------------
using System.Windows.Input;

namespace Tiny.Framework.Models
{
    /// <summary>
    /// the relay command contract
    /// </summary>
    /// <seealso cref="ICommand" />
    public interface IRelayCommand : ICommand
    {
        /// <summary>
        /// Raises the can execute changed.
        /// </summary>
        void RaiseCanExecuteChanged();
    }
}
