//-----------------------------------------------------------------------------
// copyright (c) 2016, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
//-----------------------------------------------------------------------------
using System;

namespace Tiny.Framework.Flow
{
    /// <summary>
    /// Device adapter.
    /// on the basis you have more than one of these loading...
    /// make sure the one you want is in the last
    ///  library loaded by the container bootstrapper.
    /// </summary>
    public interface IDeviceAdapter
    {
        /// <summary>
        /// Begins the invocation of.
        /// </summary>
        /// <param name="theAction">the action.</param>
        void Begin(Action theAction);
    }
}
