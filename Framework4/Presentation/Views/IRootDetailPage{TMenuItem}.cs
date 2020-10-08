// -----------------------------------------------------------------------------
// copyright (c) 2019, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
// -----------------------------------------------------------------------------
using System;

namespace Tiny.Framework.Views
{
    /// <summary>
    /// Root detail page.
    /// the <typeparamref name="TMenuItem"/> holds the page id's
    /// there can only be one of these...
    /// </summary>
    public interface IRootDetailPage<out TMenuItem> :
        IMenuItemPage<TMenuItem>
        where TMenuItem : struct, IComparable
    {
    }
}

