// -----------------------------------------------------------------------------
// copyright (c) 2019, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
// -----------------------------------------------------------------------------
namespace Tiny.Framework.Models
{
    /// <summary>
    /// Feed item contract definition.
    /// </summary>
    public interface IRssFeedItem
    {
        /// <summary>
        /// Gets the identifier.
        /// </summary>
        int ID { get; }

        /// <summary>
        /// Gets the title.
        /// </summary>
        string Title { get; }

        /// <summary>
        /// Gets the description.
        /// </summary>
        string Text { get; }

        /// <summary>
        /// Gets or sets the publish date.
        /// </summary>
        string PublishDate { get; }

        /// <summary>
        /// Gets or sets the image path.
        /// </summary>
        string ImagePath { get; }
    }
}
