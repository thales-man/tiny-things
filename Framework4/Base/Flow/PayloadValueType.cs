//-----------------------------------------------------------------------------
// copyright (c) 2016, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
//-----------------------------------------------------------------------------
using System;

namespace Tiny.Framework.Flow
{
    /// <summary>
    /// the value type message payload.
    /// </summary>
    /// <typeparam name="TItem">the type of the item.</typeparam>
    public sealed class PayloadValueType<TItem>
        where TItem : struct, IComparable, IConvertible
    {
        /// <summary>
        /// gets or sets the message.
        /// </summary>
        public TItem Message { get; set; }

        /// <summary>
        /// Performs an implicit conversion from a type of TItem to <see cref="PayloadValueType{TItem}" />.
        /// </summary>
        /// <param name="message">the message.</param>
        /// <returns>
        /// the result of the conversion.
        /// </returns>
        public static implicit operator PayloadValueType<TItem>(TItem message)
        {
            return new PayloadValueType<TItem> { Message = message };
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="PayloadValueType{TItem}" /> to a type of TItem />.
        /// </summary>
        /// <param name="payload">the payload.</param>
        /// <returns>
        /// the result of the conversion.
        /// </returns>
        public static implicit operator TItem(PayloadValueType<TItem> payload)
        {
            return payload.Message;
        }
    }
}
