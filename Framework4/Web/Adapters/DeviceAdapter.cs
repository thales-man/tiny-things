//-----------------------------------------------------------------------------
// copyright (c) 2016, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
//-----------------------------------------------------------------------------
using System;
using System.Threading.Tasks;
using Tiny.Framework.Flow;
using Tiny.Framework.Registration;
using Tiny.Framework.Utility;

namespace Tiny.Framework.Web.Adapters
{
    /// <summary>
    /// Device adapter.
    /// on the basis you have more than one of these loading,
    /// make sure the one you want is in the last
    /// library loaded by the container bootstrapper.
    /// </summary>
    internal sealed class DeviceAdapter :
        IDeviceAdapter,
        ISupportServiceRegistration
    {
        /// <summary>
        /// Begins the invocation of.
        /// </summary>
        /// <param name="theAction">the action.</param>
        public void Begin(Action theAction)
        {
            It.IsNull(theAction)
                .AsGuard<ArgumentNullException>(nameof(theAction));

            Task.Run(() => theAction());
        }
    }
}
