// -----------------------------------------------------------------------------
// copyright (c) 2019, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
// -----------------------------------------------------------------------------
using System.Composition;
using System.Threading.Tasks;
using Tiny.Framework.Models;
using Tiny.Framework.Services;
using Tiny.Framework.Views;
using Xamarin.Forms;

namespace Tiny.Framework.Scaffolding
{
    public abstract class RssFeedViewModel<TFeedItem, TFeedContract, TDisplayFeedItem> :
        RssFeedViewModel<TFeedItem, TFeedContract>
            where TFeedContract : class, IContentFeedItem
            where TFeedItem : class, TFeedContract
            where TDisplayFeedItem : class, IDisplayFeedItems<TFeedContract>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RssFeedViewModel{TFeedItem, TFeedContract, TDisplayFeedItem}"/> class.
        /// </summary>
        /// <param name="feedPath">the feed path</param>
        protected RssFeedViewModel(string feedPath)
                : base(feedPath)
        {
        }

        /// <summary>
        /// Gets or sets the media manager
        /// </summary>
        [Import]
        public IProvideMediaPlayback MediaManager { get; set; }

        /// <summary>
        /// Gets or sets the 'page' navigator
        /// </summary>
        [Import]
        public IProvideNavigationContext Navigator { get; set; }

        [Import]
        public ICreateFeedItems FeedItem { get; set; }

        /// <summary>
        /// Loads the feed item
        /// </summary>
        /// <param name="thisItem">This item</param>
        protected override async Task LoadFeedItem(TFeedContract thisItem)
        {
            using (var page = DisplayPage.CreateExport())
            {
                page.Value.SetModel(thisItem);
                await Navigator.PushAsync(page.Value as Page);
                MediaManager.Play(thisItem);
            }
        }

        /// <summary>
        /// Gets or sets the display page (export factory).
        /// </summary>
        [Import]
        public ExportFactory<TDisplayFeedItem> DisplayPage { get; set; }
    }
}

