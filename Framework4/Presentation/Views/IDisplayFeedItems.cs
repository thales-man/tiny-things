// -----------------------------------------------------------------------------
// copyright (c) 2019, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
// -----------------------------------------------------------------------------
using Tiny.Framework.Models;

namespace Tiny.Framework.Views
{
    /// <summary>
    /// I Display feed items contract definition.
    /// </summary>
    public interface IDisplayFeedItems<in TFeedItem> :
        IView
        where TFeedItem : class, IRssFeedItem
    {
        /// <summary>
        /// Sets the model.
        /// </summary>
        /// <param name="thisModel">This model.</param>
        void SetModel(TFeedItem thisModel);
    }
}
