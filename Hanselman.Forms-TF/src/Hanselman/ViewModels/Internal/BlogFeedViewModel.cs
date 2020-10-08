using System.Collections.Generic;
using System.Composition;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using Hanselman.Models;
using Tiny.Framework.Models;
using Tiny.Framework.Scaffolding;
using Tiny.Framework.Views;

namespace Hanselman.ViewModels.Internal
{
    /// <summary>
    /// Blog feed view model
    /// </summary>
    [Shared]
    [Export(typeof(IBlogFeedViewModel))]
    sealed class BlogFeedViewModel :
        RssFeedViewModel<FeedItem, IContentFeedItem, IDisplayBlogDetails>,
        IBlogFeedViewModel
    {
        const string feedPath = "http://feeds.hanselman.com/ScottHanselman";

        /// <summary>
        /// Initializes a new instance of the <see cref="BlogFeedViewModel"/> class.
        /// </summary>
        public BlogFeedViewModel()
                : base(feedPath)
        {
            Title = "Blog";
            Icon = "blog.png";
        }

        protected override async Task<IEnumerable<IContentFeedItem>> ParseFeed(string usingResponse) =>
            await Task.Run(() =>
            {
                var xdoc = XDocument.Parse(usingResponse);
                var id = 0;

                return xdoc.Descendants("item").Select(x => FeedItem.Create(x, Icon, id++));
            });
    }
}