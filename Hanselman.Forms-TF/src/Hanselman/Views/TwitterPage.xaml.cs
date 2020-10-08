using System.Composition;
using Hanselman.Models;
using Hanselman.ViewModels;
using Tiny.Framework.Views;
using Xamarin.Forms;

namespace Hanselman.Views
{
    /// <summary>
    /// Twitter page.
    /// </summary>
    [Shared]
    [Export(typeof(IMenuItemPage<MenuType>))]
    public partial class TwitterPage :
        ContentPage,
        IModelBoundView<ITwitterViewModel>,
        IMenuItemPage<MenuType>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TwitterPage"/> class.
        /// </summary>
        public TwitterPage() => InitializeComponent();

        /// <summary>
        /// Gets the menu identifier.
        /// </summary>
        /// <value>The menu identifier.</value>
        public MenuType MenuID => MenuType.Twitter;

        /// <summary>
        /// Gets or sets the view model.
        /// </summary>
        [Import]
        public ITwitterViewModel ViewModel { get; set; }

        /// <summary>
        /// Initialise this instance.
        /// </summary>
        [OnImportsSatisfied]
        public void Compose() => BindingContext = ViewModel;
    }
}
