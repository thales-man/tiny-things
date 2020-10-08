using System.Collections.Generic;
using NGraphics;

namespace Tiny.Framework.Services
{
    /// <summary>
    /// I resolve geometry path operations.
    /// </summary>
    public interface IResolveGeometryPathOperations :
        IResolvePathOperations<IReadOnlyCollection<PathOp>>
    {
    }
}
