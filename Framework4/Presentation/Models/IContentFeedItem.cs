// -----------------------------------------------------------------------------
// copyright (c) 2019, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
// -----------------------------------------------------------------------------
namespace Tiny.Framework.Models
{
    /// <summary>
    /// I Blog feed item.
    /// </summary>
    public interface IContentFeedItem :
        IRssFeedItem
    {
        /// <summary>
        /// Gets the link command.
        /// </summary>
        string LinkCommand { get; }

        /// <summary>
        /// Gets or sets the category.
        /// </summary>
        string Category { get; }

        /// <summary>
        /// Gets the item URL.
        /// </summary>
        string ContentPath { get; }
    }
}
