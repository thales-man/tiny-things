//-----------------------------------------------------------------------------
// copyright (c) 2019, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
//-----------------------------------------------------------------------------
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace Tiny.Framework.Views
{
    /// <summary>
    /// Custom content view.
    /// </summary>
    public sealed class CustomContentView : ContentView
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:Tiny.Framework.Views.CustomContentView"/> class.
        /// </summary>
        /// <param name="parent">Parent.</param>
        public CustomContentView(CustomControlView parent)
        {
            VerticalOptions = LayoutOptions.FillAndExpand;
            HorizontalOptions = LayoutOptions.FillAndExpand;
            Parent = parent;
            IsClippedToBounds = parent.RequiresMask;
        }

        /// <summary>
        /// Gets the parent (which should always be a custom control view)
        /// </summary>
        /// <value>The parent.</value>
        public new CustomControlView Parent { get; }

        /// <summary>
        /// On property changed.
        /// </summary>
        /// <param name="propertyName">Property name.</param>
        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == ContentProperty.PropertyName)
            {
                Parent.RequiresMask = Content != null;
            }
        }
    }
}
