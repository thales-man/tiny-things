//-----------------------------------------------------------------------------
// copyright (c) 2016, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
//-----------------------------------------------------------------------------
namespace Tiny.Framework.Flow
{
    /// <summary>
    /// the publish / subscribe message abstract.
    /// </summary>
    /// <typeparam name="T">the type of payload being carried.</typeparam>
    /// <seealso cref="IMediationMessage{T}" />
    public abstract class MediationMessage<T> :
        IMediationMessage<T>
        where T : class
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MediationMessage{T}"/> class.
        /// </summary>
        protected MediationMessage()
        {
            Payload = default(T);
        }

        /// <summary>
        /// gets or sets the payload.
        /// </summary>
        public T Payload { get; set; }
    }
}
