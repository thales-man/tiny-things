// -----------------------------------------------------------------------------
// copyright (c) 2019, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
// -----------------------------------------------------------------------------
using System.Composition;
using System.Threading.Tasks;
using Tiny.Framework.Models;
using Xamarin.Forms;

namespace Tiny.Framework.Views
{
    /// <summary>
    /// Master detail shell page.
    /// </summary>
    [Shared]
    [Export]
    public sealed class MasterDetailShellPage :
        MasterDetailPage,
        IMasterDetailShellView
    {
        /// <summary>
        /// The model.
        /// </summary>
        private readonly IMasterDetailShellViewModel _model;

        /// <summary>
        /// Initializes a new instance of the <see cref="MasterDetailShellPage"/> class.
        /// </summary>
        /// <param name="model">Model.</param>
        [ImportingConstructor]
        public MasterDetailShellPage(IMasterDetailShellViewModel model)
        {
            _model = model;
            _model.SetShell(this);
        }

        /// <summary>
        /// Initialise this instance.
        /// </summary>
        [OnImportsSatisfied]
        public void Initialise()
        {
            Master = _model.Master;
            Detail = _model.Detail;
        }

        /// <summary>
        /// Navigates to.
        /// </summary>
        /// <returns>The task.</returns>
        /// <param name="newPage">New page.</param>
        public async Task NavigateTo(Page newPage)
        {
            IsPresented &= Device.Idiom == TargetIdiom.Tablet;

            if (Device.RuntimePlatform == Device.Android)
            {
                await Task.Delay(300);
            }

            if (newPage != null)
            {
                Detail = newPage;
            }
        }
    }
}
