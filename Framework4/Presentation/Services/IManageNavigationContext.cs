// -----------------------------------------------------------------------------
// copyright (c) 2016, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
// -----------------------------------------------------------------------------
using Xamarin.Forms;

namespace Tiny.Framework.Services
{
    /// <summary>
    /// I Manage navigation context contract definition.
    /// </summary>
    public interface IManageNavigationContext
    {
        /// <summary>
        /// Sets the context.
        /// </summary>
        /// <param name="thisContext">This context.</param>
        void SetContext(INavigation thisContext);
    }
}
