using System.Composition;
using Hanselman.Models;
using Hanselman.ViewModels;
using Tiny.Framework.Views;
using Xamarin.Forms;

namespace Hanselman.Views
{
    /// <summary>
    /// Hansel minutes page.
    /// </summary>
    [Shared]
    [Export(typeof(IMenuItemPage<MenuType>))]
    public partial class HanselMinutesPage :
        ContentPage,
        IModelBoundView<IHanselMinutesViewModel>,
        IMenuItemPage<MenuType>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HanselMinutesPage"/> class.
        /// </summary>
        public HanselMinutesPage() => InitializeComponent();

        /// <summary>
        /// Gets the menu identifier.
        /// </summary>
        public MenuType MenuID => MenuType.Hanselminutes;

        /// <summary>
        /// Gets or sets the view model.
        /// </summary>
        [Import]
        public IHanselMinutesViewModel ViewModel { get; set; }

        /// <summary>
        /// Initialise this instance.
        /// </summary>
        [OnImportsSatisfied]
        public void Compose() => BindingContext = ViewModel;
    }
}
