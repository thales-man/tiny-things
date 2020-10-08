// -----------------------------------------------------------------------------
// copyright (c) 2019, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
// -----------------------------------------------------------------------------
using System.Composition;
using Tiny.Framework.Models;

namespace Tiny.Framework.Views
{
    /// <summary>
    /// Content page <typeparamref name="TViewModel"/> contract definition.
    /// the compostion attributes won't work here, they are just indicators of intent.
    /// </summary>
    public interface IModelBoundView<TViewModel> 
        where TViewModel : IViewModel
    {
        /// <summary>
        /// Gets or sets the view model.
        /// </summary>
        /// <value>The view model.</value>
        [Import]
        TViewModel ViewModel { get; set; }

        /// <summary>
        /// Initialise this instance.
        /// </summary>
        [OnImportsSatisfied]
        void Compose();
    }
}
