using System.Composition;
using Hanselman.Models;
using Hanselman.ViewModels;
using Tiny.Framework.Views;
using Xamarin.Forms;

namespace Hanselman.Views
{
    /// <summary>
    /// Blog page.
    /// </summary>
    [Shared]
    [Export(typeof(IMenuItemPage<MenuType>))]
    public partial class BlogPage :
        ContentPage,
        IModelBoundView<IBlogFeedViewModel>,
        IMenuItemPage<MenuType>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BlogPage"/> class.
        /// </summary>
        public BlogPage() => InitializeComponent();

        /// <summary>
        /// Gets the menu identifier.
        /// </summary>
        /// <value>The menu identifier.</value>
        public MenuType MenuID => MenuType.Blog;

        /// <summary>
        /// Gets or sets the view model.
        /// </summary>
        [Import]
        public IBlogFeedViewModel ViewModel { get; set; }

        /// <summary>
        /// Initialise this instance.
        /// </summary>
        [OnImportsSatisfied]
        public void Compose() => BindingContext = ViewModel;
    }
}
