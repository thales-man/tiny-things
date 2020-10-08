using System.Composition;
using Hanselman.ViewModels;
using MediaManager;
using Tiny.Framework.Models;
using Tiny.Framework.Views;
using Xamarin.Forms;

namespace Hanselman.Views
{
    /// <summary>
    /// Channel9 video playback page.
    /// </summary>
    [Export(typeof(IProvideVideoPlayback))]
    public partial class Channel9VideoPlaybackPage :
        ContentPage,
        IModelBoundView<IChannel9VideoPlaybackViewModel>,
        IProvideVideoPlayback
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Channel9VideoPlaybackPage"/> class.
        /// </summary>
        public Channel9VideoPlaybackPage() => 
            InitializeComponent();

        /// <summary>
        /// Gets or sets the view model.
        /// </summary>
        /// <value>The view model.</value>
        [Import]
        public IChannel9VideoPlaybackViewModel ViewModel { get; set; }

        /// <summary>
        /// Sets the model.
        /// </summary>
        /// <param name="model">Model.</param>
        public void SetModel(IContentFeedItem model) => 
            ViewModel.SetModel(model);

        [OnImportsSatisfied]
        public void Compose()
        {
            ViewModel.SetManager(CrossMediaManager.Current);
            BindingContext = ViewModel;
        }

        protected override void OnDisappearing()
        {
            ViewModel.Dispose();
            base.OnDisappearing();
        }
    }
}
