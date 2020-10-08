using System;
using System.Composition;
using MediaManager;
using Tiny.Framework.Models;
using Tiny.Framework.Scaffolding;

namespace Hanselman.ViewModels.Internal
{
    /// <summary>
    /// Channel9 video playback page.
    /// </summary>
    [Export(typeof(IChannel9VideoPlaybackViewModel))]
    public sealed class Channel9VideoPlaybackViewModel :
        ObservableViewModel,
        IChannel9VideoPlaybackViewModel,
        IDisposable
    {
        /// <summary>
        /// The video feed item.
        /// </summary>
        IContentFeedItem videoFeedItem;

        /// <summary>
        /// The manager.
        /// </summary>
        IMediaManager manager;

        /// <summary>
        /// Initializes a new instance of the <see cref="Channel9VideoPlaybackViewModel"/> class.
        /// </summary>
        public Channel9VideoPlaybackViewModel()
        {
        }

        /// <summary>
        /// Sets the manager.
        /// </summary>
        /// <param name="theManager">The manager.</param>
        public void SetManager(IMediaManager theManager)
        {
            manager = theManager;
        }

        /// <summary>
        /// Sets the model.
        /// </summary>
        /// <param name="theItem">The item.</param>
        public void SetModel(IContentFeedItem theItem)
        {
            videoFeedItem = theItem;
            VideoUrl = videoFeedItem.ContentPath;
            //if (!manager.IsPlaying())
            //{
            //    manager.Play(videoFeedItem.ContentPath);
            //}
        }

        /// <summary>
        /// The video URL.
        /// </summary>
        string videoUrl;

        /// <summary>
        /// Gets or sets the video URL.
        /// </summary>
        /// <value>The video URL.</value>
        public string VideoUrl
        {
            get => videoUrl;
            set => SetPropertyValue(ref videoUrl, value);
        }

        public void Dispose()
        {
            manager.Stop();
            manager.Queue.Clear();
        }
    }
}