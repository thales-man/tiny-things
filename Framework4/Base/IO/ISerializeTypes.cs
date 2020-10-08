//-----------------------------------------------------------------------------
// copyright (c) 2016, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
//-----------------------------------------------------------------------------
using System;

namespace Tiny.Framework.IO
{
    /// <summary>
    /// the datacontract serialiser class contract.
    /// </summary>
    public interface ISerializeTypes
    {
        /// <summary>
        /// gets the type of the serialiser.
        /// </summary>
        SerializerType SerialiserType { get; }

        /// <summary>
        /// Returns a <see cref="string" /> that represents this instance.
        /// </summary>
        /// <typeparam name="T">the type to operate on.</typeparam>
        /// <param name="item">the item.</param>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        string ToString<T>(T item)
            where T : class;

        /// <summary>
        /// To string.
        /// </summary>
        /// <returns>the string.</returns>
        /// <param name="item">Item.</param>
        /// <param name="itemType">Item type.</param>
        string ToString(object item, Type itemType);

        /// <summary>
        /// From string.
        /// </summary>
        /// <param name="item">the item.</param>
        /// <param name="classType">Type of the class.</param>
        /// <returns>a re-hydrated class of T.</returns>
        object FromString(string item, Type classType);

        /// <summary>
        /// converts to type T from the string.
        /// requires UTF-8 encoded strings only.
        /// </summary>
        /// <typeparam name="T">the type to operate on.</typeparam>
        /// <param name="item">the item.</param>
        /// <returns>a re-hydrated class of T.</returns>
        T FromString<T>(string item)
            where T : class;
    }
}
