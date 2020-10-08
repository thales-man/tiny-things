//-----------------------------------------------------------------------------
// copyright (c) 2016, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
//-----------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;

namespace Tiny.Framework.Utility
{
    /// <summary>
    /// offers a number of encapsulated enum operations.
    /// </summary>
    /// <typeparam name="TSet">the type of the set.</typeparam>
    public static class FromSet<TSet>
        where TSet : struct, IComparable, IFormattable
    {
        /// <summary>
        /// gets the specified item.
        /// </summary>
        /// <param name="item">the item.</param>
        /// <returns>a item of <typeparamref name="TSet"/>.</returns>
        public static TSet Get(string item)
        {
            return Get(item, default);
        }

        /// <summary>
        /// gets the specified item.
        /// </summary>
        /// <param name="item">the item.</param>
        /// <returns>a item of <typeparamref name="TSet"/>.</returns>
        public static TSet Get(int item)
        {
            return Get(item.ToString(), default);
        }

        /// <summary>
        /// gets the specified item.
        /// </summary>
        /// <param name="item">the item.</param>
        /// <param name="defaultValue">the default value.</param>
        /// <returns>
        /// the item as in the TSet or the default.
        /// </returns>
        public static TSet Get(string item, TSet defaultValue)
        {
            TSet value;
            var result = Enum.TryParse(item, true, out value);
            return result ? value : defaultValue;
        }

        /// <summary>
        /// gets the 'names' of the items from underlying set.
        /// </summary>
        /// <returns>
        /// a collection of string.
        /// </returns>
        public static IEnumerable<string> GetNames()
        {
            return Enum.GetNames(typeof(TSet)).AsSafeList();
        }

        /// <summary>
        /// gets the items.
        /// </summary>
        /// <returns>
        /// a collection of TSet.
        /// </returns>
        public static IEnumerable<TSet> GetItems()
        {
            return Enum.GetValues(typeof(TSet)).OfType<TSet>();
        }
    }
}
