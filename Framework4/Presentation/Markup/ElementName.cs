//-----------------------------------------------------------------------------
// copyright (c) 2016, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
//-----------------------------------------------------------------------------
using System;
using Tiny.Framework.Utility;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Tiny.Framework.Markup
{
    /// <summary>
    /// a markup extension as releative source is not currently available
    /// see: https://forums.xamarin.com/discussion/comment/105285/#Comment_105285 (keith rome)
    /// </summary>
    /// <seealso cref="IMarkupExtension" />
    public class ElementSource : 
        IMarkupExtension
    {
        public string ElementName { get; set; }

        /// <summary>
        /// Returns the object created from the markup extension.
        /// </summary>
        /// <param name="serviceProvider">To be added.</param>
        /// <returns>
        /// The object
        /// </returns>
        public object ProvideValue(IServiceProvider serviceProvider)
        {
            var rootProvider = serviceProvider.GetService(typeof(IRootObjectProvider)) as IRootObjectProvider;
            if (It.IsNull(rootProvider) || !It.IsType<Element>(rootProvider.RootObject))
            {
                return null;
            }

            var rootElement = rootProvider.RootObject as Element;

            return rootElement.FindByName<Element>(ElementName);
        }
    }
}
