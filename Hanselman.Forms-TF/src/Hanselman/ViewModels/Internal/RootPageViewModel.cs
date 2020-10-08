// -----------------------------------------------------------------------------
//  copyright (c) 2019, the striped lawn company limited. all rights reserved.
//  the striped lawn company licenses this file to you under the GPLv3 license.
//  see the LICENSE file in the project root for more information.
// -----------------------------------------------------------------------------
using System.Composition;
using Hanselman.Models;
using Tiny.Framework.Models;
using Tiny.Framework.Scaffolding;

namespace Hanselman.ViewModels.Internal
{
    /// <summary>
    /// Root page view model.
    /// </summary>
    [Shared]
    [Export(typeof(IMasterDetailShellViewModel))]
    sealed class RootPageViewModel :
        MasterDetailShellViewModel<MenuType>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:Hanselman.ViewModels.Internal.RootPageViewModel"/> class.
        /// </summary>
        public RootPageViewModel()
        {
            Title = "Hanselman";
            Icon = "slideout.png";
        }
    }
}
