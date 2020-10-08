//-----------------------------------------------------------------------------
// copyright (c) 2016, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
//-----------------------------------------------------------------------------
using System;

namespace Http.Service.Contract
{

    /// <summary>
    /// the unsupported behaviour exception
    /// </summary>
    /// <seealso cref="System.Exception" />
    public class UnsupportedBehaviourException : Exception
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="UnsupportedBehaviourException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public UnsupportedBehaviourException(string message) : base(message) { }
    }
}
