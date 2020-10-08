//-----------------------------------------------------------------------------
// copyright (c) 2019, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
//-----------------------------------------------------------------------------
using CoreGraphics;

namespace Tiny.Framework.Services
{
    /// <summary>
    /// I adapt platform path operations.
    /// </summary>
    public interface IAdaptPlatformPathOperations :
        IAdaptPathOperations<CGPath>
    {
    }
}
