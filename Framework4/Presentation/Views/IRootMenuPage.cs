// -----------------------------------------------------------------------------
// copyright (c) 2019, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
// -----------------------------------------------------------------------------

namespace Tiny.Framework.Views
{
    /// <summary>
    /// Root menu page.
    /// </summary>
    public interface IRootMenuPage :
        IView
    {
        /// <summary>
        /// Gets the title.
        /// </summary>
        /// <value>The title.</value>
        string Title { get; }
    }
}

