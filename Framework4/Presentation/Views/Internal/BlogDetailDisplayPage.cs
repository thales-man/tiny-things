using System.Composition;
using Tiny.Framework.Models;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Tiny.Framework.Views.Internal
{
    /// <summary>
    /// Blog details view.
    /// </summary>
    [Export(typeof(IDisplayBlogDetails))]
    internal sealed class BlogDetailDisplayPage :
        ContentPage,
        IDisplayBlogDetails
    {
        /// <summary>
        /// Sets the model.
        /// </summary>
        /// <param name="thisModel">This model.</param>
        public void SetModel(IContentFeedItem thisModel)
        {
            var webView = new WebView
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };

            webView.Source = new HtmlWebViewSource
            {
                Html = thisModel.Text
            };

            Content = new StackLayout
            {
                Children = { webView }
            };

            var share = new ToolbarItem
            {
                IconImageSource = "ic_share.png",
                Text = "Share",
                Command = new Command(() => Share.RequestAsync(new ShareTextRequest(thisModel.Title, thisModel.LinkCommand)))
            };

            ToolbarItems.Add(share);
        }
    }
}

