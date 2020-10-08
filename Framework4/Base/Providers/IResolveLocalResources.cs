//-----------------------------------------------------------------------------
// copyright (c) 2016, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
//-----------------------------------------------------------------------------
namespace Tiny.Framework.Providers
{
    /// <summary>
    /// the local resource resolver.
    /// </summary>
    /// <seealso cref="IResolveGroupedResources" />
    public interface IResolveLocalResources
    {
        /// <summary>
        /// gets the string.
        /// </summary>
        /// <param name="usingKey">the using key.</param>
        /// <returns>a resource string.</returns>
        string GetString(string usingKey);

        /// <summary>
        /// gets the string.
        /// the key is an object that will need to
        ///  resolve down to the resource keys value.
        /// </summary>
        /// <param name="usingKey">the using key.</param>
        /// <returns>a resource string.</returns>
        string GetString(object usingKey);
    }
}
