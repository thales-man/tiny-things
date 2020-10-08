// -----------------------------------------------------------------------------
// copyright (c) 2019, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
// -----------------------------------------------------------------------------
using System;

namespace Tiny.Framework.Views
{
    /// <summary>
    /// Menu item page.
    /// the <typeparamref name="TMenuItem"/> holds the page id's
    /// </summary>
    public interface IMenuItemPage<out TMenuItem> :
        IView
        where TMenuItem : struct, IComparable
    {
        /// <summary>
        /// Gets the menu identifier.
        /// </summary>
        TMenuItem MenuID { get; }
    }
}

