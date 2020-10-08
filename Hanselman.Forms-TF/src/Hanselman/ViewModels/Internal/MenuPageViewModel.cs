using System.Composition;
using Hanselman.Models;
using Tiny.Framework.Models;
using Tiny.Framework.Scaffolding;

namespace Hanselman.ViewModels.Internal
{
    [Shared]
    [Export(typeof(IMenuPageViewModel))]
    sealed class MenuPageViewModel :
        MenuPageViewModel<MenuType>
    {
        public MenuPageViewModel()
        {
            Title = "Hanselman.Forms";
            Subtitle = "Hanselman.Forms";
            Icon = "slideout.png";
        }
    }
}

