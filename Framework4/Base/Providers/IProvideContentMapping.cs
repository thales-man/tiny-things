//-----------------------------------------------------------------------------
// copyright (c) 2016, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
//-----------------------------------------------------------------------------
namespace Tiny.Framework.Providers
{
    /// <summary>
    /// the mapped content provider contract.
    /// </summary>
    /// <typeparam name="TMappedKey">the mapping type.</typeparam>
    /// <typeparam name="TMappedContent">the mapped content.</typeparam>
    public interface IProvideContentMapping<in TMappedKey, out TMappedContent>
    {
        /// <summary>
        /// Determines whether [holds] [the specified <paramref name="theKey" />].
        /// checks if the mapped action is in the set.
        /// </summary>
        /// <param name="theKey">the key.</param>
        /// <returns>
        /// true if it's mapped.
        /// </returns>
        bool Holds(TMappedKey theKey);

        /// <summary>
        /// Fetch the content for the specified key.
        /// </summary>
        /// <param name="theKey">the key.</param>
        /// <returns>mapped content.</returns>
        TMappedContent Fetch(TMappedKey theKey);
    }
}
