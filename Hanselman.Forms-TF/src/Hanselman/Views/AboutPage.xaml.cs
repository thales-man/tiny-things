using System.Composition;
using Hanselman.Models;
using Hanselman.ViewModels;
using Tiny.Framework.Views;
using Xamarin.Forms;

namespace Hanselman.Views
{
    /// <summary>
    /// About page.
    /// there is only one root detail page per application
    /// this is the page that will be display first
    /// </summary>
    [Shared]
    [Export(typeof(IRootDetailPage<MenuType>))]
    [Export(typeof(IMenuItemPage<MenuType>))]
    public partial class AboutPage :
        ContentPage,
        IModelBoundView<IAboutPageViewModel>,
        IRootDetailPage<MenuType>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AboutPage"/> class.
        /// </summary>
        public AboutPage() => InitializeComponent();

        /// <summary>
        /// Gets the menu identifier.
        /// </summary>
        /// <value>The menu identifier.</value>
        public MenuType MenuID => MenuType.About;

        /// <summary>
        /// Gets or sets the view model.
        /// </summary>
        [Import]
        public IAboutPageViewModel ViewModel { get; set; }

        /// <summary>
        /// Initialise this instance.
        /// </summary>
        [OnImportsSatisfied]
        public void Compose() => BindingContext = ViewModel;
    }
}
