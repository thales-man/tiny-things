using System.Composition;
using Hanselman.Models;
using Hanselman.ViewModels;
using Tiny.Framework.Views;
using Xamarin.Forms;

namespace Hanselman.Views
{
    /// <summary>
    /// Ratchet and geek page.
    /// </summary>
    [Shared]
    [Export(typeof(IMenuItemPage<MenuType>))]
    public partial class RatchetAndGeekPage :
        ContentPage,
        IModelBoundView<IRatchetAndGeekViewModel>,
        IMenuItemPage<MenuType>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RatchetAndGeekPage"/> class.
        /// </summary>
        public RatchetAndGeekPage() => InitializeComponent();

        /// <summary>
        /// Gets the menu identifier.
        /// </summary>
        /// <value>The menu identifier.</value>
        public MenuType MenuID => MenuType.Ratchet;

        /// <summary>
        /// Gets or sets the view model.
        /// </summary>
        [Import]
        public IRatchetAndGeekViewModel ViewModel { get; set; }

        /// <summary>
        /// Initialise this instance.
        /// </summary>
        [OnImportsSatisfied]
        public void Compose() => BindingContext = ViewModel;
    }
}
