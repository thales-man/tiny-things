using System.Collections.Generic;
using System.Composition;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using Hanselman.Models;
using Tiny.Framework.Models;
using Tiny.Framework.Scaffolding;

namespace Hanselman.ViewModels.Internal
{
    /// <summary>
    /// Developer life view model
    /// </summary>
    [Shared]
    [Export(typeof(IDeveloperLifeViewModel))]
    sealed class DeveloperLifeViewModel :
        RssFeedViewModel<FeedItem, IContentFeedItem, IProvideAudioPlayback>,
        IDeveloperLifeViewModel
    {
        const string feedPath = "http://feeds.feedburner.com/ThisDevelopersLife?format=xml";

        /// <summary>
        /// Initializes a new instance of the <see cref="DeveloperLifeViewModel"/> class.
        /// </summary>
        public DeveloperLifeViewModel()
            : base(feedPath)
        {
            Icon = "tdl.png";
            Title = "This Developer Life";
        }

        protected override async Task<IEnumerable<IContentFeedItem>> ParseFeed(string usingResponse) =>
            await Task.Run(() =>
            {
                var xdoc = XDocument.Parse(usingResponse);
                var id = 0;

                return (from item in xdoc.Descendants("item")
                        let enclosure = item.Element("enclosure")
                        where enclosure != null
                        select FeedItem.Create(item, Icon, id++, enclosure));
            });
    }
}