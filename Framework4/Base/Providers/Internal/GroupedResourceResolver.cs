//-----------------------------------------------------------------------------
// copyright (c) 2016, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
//-----------------------------------------------------------------------------
using System.Collections.Generic;
using System.Composition;
using System.Linq;
using Tiny.Framework.Registration;
using Tiny.Framework.Utility;

namespace Tiny.Framework.Providers.Internal
{
    /// <summary>
    /// the group resource resolver.
    /// </summary>
    /// <seealso cref="IResolveGroupedResources" />
    [Shared]
    [Export(typeof(IResolveGroupedResources))]
    internal class GroupedResourceResolver :
        IResolveGroupedResources,
        ISupportServiceRegistration
    {
        /// <summary>
        /// gets or sets the resolvers.
        /// </summary>
        [ImportMany]
        public IEnumerable<IResolveLocalResources> Resolvers { get; set; }

        /// <summary>
        /// gets the string.
        /// </summary>
        /// <param name="usingKey">the using key.</param>
        /// <returns>
        /// a resource string (or null).
        /// </returns>
        public string GetString(string usingKey)
        {
            var resolver = Resolvers.FirstOrDefault(x => It.Has(x.GetString(usingKey)));
            return It.Has(resolver) ? resolver.GetString(usingKey) : null;
        }

        /// <summary>
        /// gets the string.
        /// the key is an object that will need to
        /// resolve down to the resource keys value.
        /// </summary>
        /// <param name="usingKey">the using key.</param>
        /// <returns>
        /// a resource string.
        /// </returns>
        public string GetString(object usingKey)
        {
            return GetString($"{usingKey}");
        }
    }
}
