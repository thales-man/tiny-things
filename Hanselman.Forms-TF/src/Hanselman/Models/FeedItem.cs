using Tiny.Framework.Models;

namespace Hanselman.Models
{
    /// <summary>
    /// the feed item
    /// </summary>
    public class FeedItem :
        IContentFeedItem
    {
        /// <summary>
        /// the items id
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        ///  the items title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// the items caption
        /// </summary>
        public string Caption { get; set; }

        /// <summary>
        /// the text (content)
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// the publication date
        /// </summary>
        public string PublishDate { get; set; }

        /// <summary>
        /// the items subject category
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// the items content text link command
        /// </summary>
        public string LinkCommand { get; set; }

        /// <summary>
        /// the items content media link
        /// </summary>
        public string ContentPath { get; set; }

        /// <summary>
        /// the items icon image path
        /// </summary>
        public string ImagePath { get; set; } = @"https://secure.gravatar.com/avatar/70148d964bb389d42547834e1062c886?s=60&r=x&d=http%3a%2f%2fd1iqk4d73cu9hh.cloudfront.net%2fcomponents%2fimg%2fuser-icon.png";
    }
}
