// -----------------------------------------------------------------------------
// copyright (c) 2019, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
// -----------------------------------------------------------------------------
using System;
using Tiny.Framework.Helpers;
using Tiny.Framework.Providers;

namespace Tiny.Framework.Services.Internal
{
    /// <summary>
    /// I provide geometry parser actions.
    /// </summary>
    public interface IProvideGeometryParserActions :
        IProvideContentMapping<GeometryCommand, Func<GeometryState, IAggregateGeometryPathOperations, IWalkGeometryPathData, GeometryState>>
    { }
}
