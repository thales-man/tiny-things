using System.Composition;
using Hanselman.Models;
using Hanselman.ViewModels;
using Tiny.Framework.Views;
using Xamarin.Forms;

namespace Hanselman.Views
{
    /// <summary>
    /// Developer life page.
    /// </summary>
    [Shared]
    [Export(typeof(IMenuItemPage<MenuType>))]
    public partial class DeveloperLifePage :
        ContentPage,
        IModelBoundView<IDeveloperLifeViewModel>,
        IMenuItemPage<MenuType>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeveloperLifePage"/> class.
        /// </summary>
        public DeveloperLifePage() => InitializeComponent();

        /// <summary>
        /// Gets the menu identifier.
        /// </summary>
        public MenuType MenuID => MenuType.DeveloperLife;

        /// <summary>
        /// Gets or sets the view model.
        /// </summary>
        [Import]
        public IDeveloperLifeViewModel ViewModel { get; set; }

        /// <summary>
        /// Initialise this instance.
        /// </summary>
        [OnImportsSatisfied]
        public void Compose() => BindingContext = ViewModel;
    }
}
