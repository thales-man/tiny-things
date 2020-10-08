// -----------------------------------------------------------------------------
// copyright (c) 2020, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
// -----------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace Tiny.Framework.Web.Providers
{
    /// <summary>
    /// Exception maps used inside the fault response provider.
    /// </summary>
    internal sealed class ExceptionMaps :
        Dictionary<Type, Func<Exception, HttpResponseMessage>>
    {
    }
}