// -----------------------------------------------------------------------------
// copyright (c) 2019, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
// -----------------------------------------------------------------------------
using System.Composition;
using Tiny.Framework.Models;
using Tiny.Framework.Models.Internal;
using Tiny.Framework.Views;

namespace Tiny.Framework.Factories.Internal
{
    /// <summary>
    /// Change detail page request message factory.
    /// </summary>
    [Shared]
    [Export(typeof(ICreateChangeDetailPageRequestMessages))]
    internal sealed class ChangeDetailPageRequestMessageFactory :
        ICreateChangeDetailPageRequestMessages
    {
        /// <summary>
        /// Create the specified thisPage.
        /// </summary>
        /// <returns>The message.</returns>
        /// <param name="thisPage">This page.</param>
        public IChangeDetailRequestMessage Create(IView thisPage)
        {
            return new ChangeDetailRequestMessage { Payload = thisPage };
        }
    }
}
