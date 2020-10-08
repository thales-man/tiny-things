using System.Composition;
using Tiny.Framework.Models;
using Tiny.Framework.Views;
using Xamarin.Forms;

namespace Hanselman.Views
{
    /// <summary>
    /// Menu page.
    /// the menu page has to be loaded via the constructor, 
    /// there is an implicit construction order still prevalent
    /// this type of page has to be initialised in this way
    /// you cannot use: IModelBoundView{IMenuPageViewModel}
    /// </summary>
    [Shared]
    [Export(typeof(IRootMenuPage))]
    public partial class MenuPage :
        ContentPage,
        IRootMenuPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MenuPage"/> class.
        /// </summary>
        [ImportingConstructor]
        public MenuPage(IMenuPageViewModel model)
        {
            InitializeComponent();
            BindingContext = model;
        }
    }
}
