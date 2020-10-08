// -----------------------------------------------------------------------------
// copyright (c) 2019, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
// -----------------------------------------------------------------------------
using Tiny.Framework.Models;
using Tiny.Framework.Views;

namespace Tiny.Framework.Factories
{
    public interface ICreateChangeDetailPageRequestMessages
    {
        IChangeDetailRequestMessage Create(IView thisPage);
    }
}

