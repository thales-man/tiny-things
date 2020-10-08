//-----------------------------------------------------------------------------
// copyright (c) 2016, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
//-----------------------------------------------------------------------------
namespace Tiny.Framework.Services
{
    /// <summary>
    /// I resolve path operations.
    /// </summary>
    public interface IResolvePathOperations<out TPathContainer>
        where TPathContainer : class
    {
        /// <summary>
        /// Gets the path result.
        /// </summary>
        /// <returns>The path result.</returns>
        TPathContainer GetPathResult();
    }
}
