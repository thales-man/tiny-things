using System.Collections.Generic;
using System.Composition;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using Hanselman.Models;
using Tiny.Framework.Models;
using Tiny.Framework.Scaffolding;
using Tiny.Framework.Utility;

namespace Hanselman.ViewModels.Internal
{
    /// <summary>
    /// Ratchet and geek view model.
    /// </summary>
    [Shared]
    [Export(typeof(IRatchetAndGeekViewModel))]
    sealed class RatchetAndGeekViewModel :
        RssFeedViewModel<FeedItem, IContentFeedItem, IProvideAudioPlayback>,
        IRatchetAndGeekViewModel
    {
        const string feedPath = "http://feeds.feedburner.com/RatchetAndTheGeek?format=xml";

        /// <summary>
        /// Initializes a new instance of the <see cref="RatchetAndGeekViewModel"/> class.
        /// </summary>
        public RatchetAndGeekViewModel()
                : base(feedPath)
        {
            Icon = "ratchet.png";
            Title = "Ratchet & The Geek";
        }

        protected override async Task<IEnumerable<IContentFeedItem>> ParseFeed(string usingResponse) =>
            await Task.Run(() =>
            {
                var xdoc = XDocument.Parse(usingResponse);
                var id = 0;

                return (from item in xdoc.Descendants("item")
                        let enclosure = item.Element("enclosure")
                        where It.Has(enclosure)
                        select FeedItem.Create(item, Icon, id++, enclosure));
            });
    }
}