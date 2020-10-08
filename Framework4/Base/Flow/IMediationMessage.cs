//-----------------------------------------------------------------------------
// copyright (c) 2016, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
//-----------------------------------------------------------------------------
namespace Tiny.Framework.Flow
{
    /// <summary>
    /// the carry message base contract.
    /// </summary>
    public interface IMediationMessage
    {
    }

    /// <summary>
    /// the carry message contract.
    /// </summary>
    /// <typeparam name="T">the type of T being carried in the message.</typeparam>
    public interface IMediationMessage<T> :
        IMediationMessage
        where T : class
    {
        /// <summary>
        /// gets or sets the payload.
        /// </summary>
        T Payload { get; set; }
    }
}
