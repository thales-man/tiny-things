using System.Composition;
using Hanselman.ViewModels;
using MediaManager;
using Tiny.Framework.Models;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Hanselman.Views
{
    /// <summary>
    /// Podcast playback page.
    /// deliberately not shared
    /// </summary>
    [Export(typeof(IProvideAudioPlayback))]
    public partial class PodcastPlaybackPage :
        ContentPage,
        IProvideAudioPlayback
    {
        /// <summary>
        /// Gets the playback controller.
        /// </summary>
        /// <value>The playback controller.</value>
        IMediaManager PlaybackController => CrossMediaManager.Current;

        /// <summary>
        /// Initializes a new instance of the <see cref="PodcastPlaybackPage"/> class.
        /// </summary>
        public PodcastPlaybackPage()
        {
            InitializeComponent();

            //CrossMediaManager.Current.PlayingChanged += (sender, args) =>
            //{
            //    Device.BeginInvokeOnMainThread(() => ProgressBar.Progress = args.Position);
            //};

            play.Clicked += (sender, args) => PlaybackController.Play();
            pause.Clicked += (sender, args) => PlaybackController.Pause();
            stop.Clicked += (sender, args) => PlaybackController.Stop();
        }

        /// <summary>
        /// Sets the model.
        /// </summary>
        /// <param name="model">Model.</param>
        public void SetModel(IContentFeedItem model)
        {
            BindingContext = model;

            webView.Source = new HtmlWebViewSource
            {
                Html = model.Text
            };

            var share = new ToolbarItem
            {
                IconImageSource = "ic_share.png",
                Text = "Share",
                Command = new Command(() => Launcher.OpenAsync(model.LinkCommand))
            };

            ToolbarItems.Add(share);
        }

        /// <summary>
        /// On disappearing.
        /// </summary>
        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            PlaybackController.Stop();
        }
    }
}
