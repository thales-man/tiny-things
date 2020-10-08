using System.Collections.Generic;
using System.Linq;
using Tiny.Framework.Models;

namespace Hanselman.Models
{
    /// <summary>
    /// Video feed item.
    /// </summary>
    public class VideoFeedItem : 
        IContentFeedItem
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets the publish date.
        /// </summary>
        public string PublishDate { get; set; }

        /// <summary>
        /// Gets or sets the category.
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// Gets or sets the link.
        /// </summary>
        public string LinkCommand { get; set; }

        /// <summary>
        /// Gets the content URL.
        /// </summary>
        public string ContentPath => Videos.FirstOrDefault()?.Url;

        /// <summary>
        /// Gets or sets the image path.
        /// </summary>
        public string ImagePath { get; set; }

        /// <summary>
        /// Gets or sets the 'collection' of videos.
        /// </summary>
        public IReadOnlyCollection<VideoContentItem> Videos { get; set; }
    }
}