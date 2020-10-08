// -----------------------------------------------------------------------------
// copyright (c) 2019, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
// -----------------------------------------------------------------------------
using System.Threading.Tasks;
using Tiny.Framework.Container;
using Xamarin.Forms;

namespace Tiny.Framework.Views
{
    /// <summary>
    /// Master detail shell view contract definition.
    /// </summary>
    public interface IMasterDetailShellView :
        IShell,
        IView
    {
        /// <summary>
        /// Navigates to.
        /// </summary>
        /// <returns>The <see langword="async"/> task call.</returns>
        /// <param name="newPage">New page.</param>
        Task NavigateTo(Page newPage);
    }
}
