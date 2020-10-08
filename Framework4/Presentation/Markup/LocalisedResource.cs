//-----------------------------------------------------------------------------
// copyright (c) 2016, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
//-----------------------------------------------------------------------------
using System;
using Tiny.Framework.Providers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Tiny.Framework.Markup
{
    /// <summary>
    /// a markup extension for retrieving localised resource strings 
    /// </summary>
    /// <seealso cref="IMarkupExtension" />
    [ContentProperty("ResourceKey")]
    public class LocalisedResource : 
        IMarkupExtension
    {
        public string ResourceKey { get; set; }

        // [Import]
        // public IResolveLocalResources Resolver [ get: set: ] 

        // public LocalisedResource()
        // [
        //    // yuk.. (i know...)
        //    Bootstrap.ResolveImports(this)
        // ]

        /// <summary>
        /// Returns the object created from the markup extension.
        /// </summary>
        /// <param name="serviceProvider">To be added.</param>
        /// <returns>
        /// The object
        /// </returns>
        public object ProvideValue(IServiceProvider serviceProvider)
        {
            var resources = serviceProvider.GetService<IResolveLocalResources>();
            return resources.GetString(ResourceKey);
            // return Resolver.GetString(ResourceKey)
        }
    }
}
