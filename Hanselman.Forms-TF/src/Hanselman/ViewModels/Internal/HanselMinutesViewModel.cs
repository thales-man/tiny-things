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
    /// Hansel minutes view model.
    /// </summary>
    [Shared]
    [Export(typeof(IHanselMinutesViewModel))]
    sealed class HanselMinutesViewModel :
        RssFeedViewModel<FeedItem, IContentFeedItem, IProvideAudioPlayback>,
        IHanselMinutesViewModel
    {
        const string feedPath = "http://feeds.podtrac.com/9dPm65vdpLL1";

        /// <summary>
        /// Initializes a new instance of the <see cref="HanselMinutesViewModel"/> class.
        /// </summary>
        public HanselMinutesViewModel()
            : base(feedPath)
        {
            Icon = "hm.png";
            Title = "Hanselminutes";
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