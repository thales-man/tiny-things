using System.Composition;
using MessageRelay.ViewModels;
using Tiny.Framework.Container;
using Tiny.Framework.Views;
using Xamarin.Forms;

namespace MessageRelay.Views
{
    /// <summary>
    /// Main page.
    /// </summary>
    [Shared]
    [Export]
    public partial class MainPage :
        ContentPage,
        IModelBoundView<IMainViewModel>,
        IShell
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:MessageRelay.Views.MainPage"/> class.
        /// </summary>
        public MainPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Gets or sets the view model.
        /// </summary>
        /// <value>The view model.</value>
        [Import]
        public IMainViewModel ViewModel { get; set; }

        /// <summary>
        /// Compose this instance.
        /// </summary>
        [OnImportsSatisfied]
        public void Compose() => BindingContext = ViewModel;
    }
}