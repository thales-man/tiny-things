using System.Collections.Generic;
using NGraphics;

namespace Tiny.Framework.Services
{
    /// <summary>
    /// I adapt control path operations.
    /// </summary>
    public interface IAdaptControlPathOperations :
        IAdaptPathOperations<IReadOnlyCollection<PathOp>>
    {
    }
}
