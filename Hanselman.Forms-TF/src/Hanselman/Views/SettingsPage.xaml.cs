using System.Composition;
using Hanselman.Models;
using Hanselman.ViewModels;
using Tiny.Framework.Views;
using Xamarin.Forms;

namespace Hanselman.Views
{
    /// <summary>
    /// About page.
    /// </summary>
    [Shared]
    [Export(typeof(IMenuItemPage<MenuType>))]
    public partial class SettingsPage :
        ContentPage,
        IModelBoundView<ISettingsPageViewModel>,
        IRootDetailPage<MenuType>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AboutPage"/> class.
        /// </summary>
        public SettingsPage() => InitializeComponent();

        /// <summary>
        /// Gets the menu identifier.
        /// </summary>
        /// <value>The menu identifier.</value>
        public MenuType MenuID => MenuType.Settings;

        /// <summary>
        /// Gets or sets the view model.
        /// </summary>
        [Import]
        public ISettingsPageViewModel ViewModel { get; set; }

        /// <summary>
        /// Initialise this instance.
        /// </summary>
        [OnImportsSatisfied]
        public void Compose() => BindingContext = ViewModel;
    }
}
