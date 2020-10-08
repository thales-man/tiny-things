//-----------------------------------------------------------------------------
// copyright (c) 2016, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
//-----------------------------------------------------------------------------
using System;

namespace Tiny.Framework.Utility
{
    /// <summary>
    /// the IOS linking kludge to get 'unused' referenced libraries across to the device.
    /// send in one light class from each desired inclusion. this fix will
    ///  maintain the smart linking whilst ensuring essential things get passed over.
    /// </summary>
    public static class SmartLinking
    {
        /// <summary>
        /// Includes the libraries.
        /// </summary>
        /// <param name="containingTheseTypes">Containing these types.</param>
        public static void IncludeLibraries(params Type[] containingTheseTypes)
        {
            // we don't want to do anything with these types, we're just enforcing deployment
            containingTheseTypes.ForEach(x => x.GetType());
        }
    }
}
