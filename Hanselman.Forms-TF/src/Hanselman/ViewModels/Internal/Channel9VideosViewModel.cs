using System;
using System.Collections.Generic;
using System.Composition;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using Hanselman.Models;
using Hanselman.Views;
using Tiny.Framework.Models;
using Tiny.Framework.Scaffolding;
using Tiny.Framework.Utility;

namespace Hanselman.ViewModels.Internal
{
    /// <summary>
    /// Channel9 videos view model.
    /// </summary>
    [Shared]
    [Export(typeof(IChannel9VideosViewModel))]
    sealed class Channel9VideosViewModel :
        RssFeedViewModel<VideoFeedItem, IContentFeedItem, IProvideVideoPlayback>,
        IChannel9VideosViewModel
    {
        /// <summary>
        /// The feed path.
        /// </summary>
        const string feedPath = "https://channel9.msdn.com/Shows/Azure-Friday/feed";

        /// <summary>
        /// The media (name space).
        /// </summary>
        static XNamespace media = "http://search.yahoo.com/mrss/";

        /// <summary>
        /// The itunes (name space).
        /// </summary>
        static XNamespace itunes = "http://www.itunes.com/dtds/podcast-1.0.dtd";

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="T:Hanselman.ViewModels.Internal.Channel9VideosViewModel"/> class.
        /// </summary>
        public Channel9VideosViewModel()
                : base(feedPath)
        {
            Title = "Channel 9 Videos";
            Icon = "channel9.png";
        }

        /// <summary>
        /// Parses the feed.
        /// </summary>
        /// <returns>The feed.</returns>
        /// <param name="usingResponse">Using response.</param>
        protected override async Task<IEnumerable<IContentFeedItem>> ParseFeed(string usingResponse) =>
            await Task.Run(() =>
            {
                var xdoc = XDocument.Parse(usingResponse);
                var list = new List<VideoFeedItem>();

                foreach (var item in xdoc.Descendants("item"))
                {
                    var videoFeedItem = SafeActions.Try(() => GetItemFromFeed(item));

                    if (It.Has(videoFeedItem))
                    {
                        list.Add(videoFeedItem);
                    }
                }

                return list;
            });

        /// <summary>
        /// Gets the item from feed.
        /// </summary>
        /// <returns>The item from feed.</returns>
        /// <param name="thisItem">This item.</param>
        public VideoFeedItem GetItemFromFeed(XElement thisItem)
        {
            var foundTheMagicKeyword = thisItem.ToString().Holds("hanselman");

            if (!foundTheMagicKeyword)
            {
                return null;
            }

            var mediaGroup = thisItem.Element(media + "group");
            if (mediaGroup == null)
            {
                return null;
            }

            var videoUrls = new List<VideoContentItem>();

            foreach (var mediaUrl in mediaGroup.Elements())
            {
                var duration = mediaUrl.Attribute("duration").Value;
                var fileSize = mediaUrl.Attribute("fileSize").Value;
                var url = mediaUrl.Attribute("url").Value;

                videoUrls.Add(new VideoContentItem
                {
                    Duration = TimeSpan.FromSeconds(Convert.ToInt32(duration)),
                    FileSize = long.Parse(fileSize),
                    Url = url,
                });
            }

            return new VideoFeedItem
            {
                Videos = videoUrls.OrderByDescending(url => url.FileSize).ToList(),
                Title = thisItem.Element("title").Value,
                Text = thisItem.Element(itunes + "summary")?.Value,
                LinkCommand = thisItem.Element("link").Value,
                PublishDate = thisItem.Element("pubDate").Value,
                Category = thisItem.Element("category").Value,
                ImagePath = thisItem.Element(media + "thumbnail")?.Attribute("url")?.Value
            };
        }
    }
}
