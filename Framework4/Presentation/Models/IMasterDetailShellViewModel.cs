// -----------------------------------------------------------------------------
// copyright (c) 2019, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
// -----------------------------------------------------------------------------
using Tiny.Framework.Views;
using Xamarin.Forms;

namespace Tiny.Framework.Models
{
    /// <summary>
    /// Master detail shell view model contract definition.
    /// </summary>
    public interface IMasterDetailShellViewModel :
        IViewModel
    {
        /// <summary>
        /// Gets the master.
        /// </summary>
        /// <value>The master.</value>
        Page Master { get; }

        /// <summary>
        /// Gets the detail.
        /// </summary>
        /// <value>The detail.</value>
        Page Detail { get; }

        /// <summary>
        /// Sets the shell.
        /// </summary>
        /// <param name="shell">Shell.</param>
        void SetShell(IMasterDetailShellView shell);
    }
}

