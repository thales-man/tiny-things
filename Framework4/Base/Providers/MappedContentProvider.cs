//-----------------------------------------------------------------------------
// copyright (c) 2016, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
//-----------------------------------------------------------------------------
using System.Collections.Generic;
using Tiny.Framework.Utility;

namespace Tiny.Framework.Providers
{
    /// <summary>
    /// a generic content mapping provider abstraction.
    /// </summary>
    /// <typeparam name="TMappedKey">the type of the mapped key.</typeparam>
    /// <typeparam name="TMappedContent">the type of the mapped content.</typeparam>
    /// <seealso cref="IProvideContentMapping{TMappedKey, TMappedContent}" />
    public abstract class MappedContentProvider<TMappedKey, TMappedContent> :
        IProvideContentMapping<TMappedKey, TMappedContent>
    {
        private readonly Dictionary<TMappedKey, TMappedContent> items;

        /// <summary>
        /// Initializes a new instance of the <see cref="MappedContentProvider{TMappedKey, TMappedContent}"/> class.
        /// </summary>
        protected MappedContentProvider()
        {
            items = new Dictionary<TMappedKey, TMappedContent>();
        }

        /// <summary>
        /// Adds the specified key.
        /// </summary>
        /// <param name="theKey">the key.</param>
        /// <param name="theContent">the content.</param>
        public void Add(TMappedKey theKey, TMappedContent theContent)
        {
            items.Add(theKey, theContent);
        }

        /// <summary>
        /// Update the specified key and content.
        /// </summary>
        /// <param name="theKey">Key.</param>
        /// <param name="theContent">Content.</param>
        public void Update(TMappedKey theKey, TMappedContent theContent)
        {
            items.Remove(theKey);
            items.Add(theKey, theContent);
        }

        /// <summary>
        /// Determines whether [holds] [the specified <paramref name="theKey" />].
        /// checks if the mapped action is in the set.
        /// </summary>
        /// <param name="theKey">the key.</param>
        /// <returns>
        /// true if it's mapped.
        /// </returns>
        public bool Holds(TMappedKey theKey)
        {
            // this will account for any lazy loading before we retry for the key
            if (!items.ContainsKey(theKey))
            {
                SafeActions.Try(() => FetchDefault(theKey));
            }

            return items.ContainsKey(theKey);
        }

        /// <summary>
        /// Fetch the content for the specified key.
        /// </summary>
        /// <param name="theKey">the key.</param>
        /// <returns>
        /// mapped content.
        /// </returns>
        public TMappedContent Fetch(TMappedKey theKey)
        {
            return items.ContainsKey(theKey) ? items[theKey] : FetchDefault(theKey);
        }

        /// <summary>
        /// gets the default.
        /// </summary>
        /// <param name="theKey">the key.</param>
        /// <returns>
        ///   <see typeparam="TMappedContent"/>.
        /// </returns>
        public abstract TMappedContent FetchDefault(TMappedKey theKey);
    }
}
