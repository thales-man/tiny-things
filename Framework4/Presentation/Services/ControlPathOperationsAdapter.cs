using System.Collections.Generic;
using NGraphics;

namespace Tiny.Framework.Services
{
    /// <summary>
    /// Control path operations adapter.
    /// </summary>
    public class ControlPathOperationsAdapter :
        PathOperationsAdapter<List<PathOp>>,
        IAdaptControlPathOperations
    {
        /// <summary>
        /// Gets the current point.
        /// </summary>
        /// <value>The current point.</value>
        public override Point CurrentPoint =>
            PathOperations[PathOperations.Count - 1].EndPoint;

        /// <summary>
        /// Gets the curve continuation.
        /// </summary>
        /// <value>The curve continuation.</value>
        public override Point CurveContinuation =>
            PathOperations[PathOperations.Count - 1].GetContinueCurveControlPoint();

        /// <summary>
        /// Clears the path.
        /// </summary>
        public void ClearPath() =>
            PathOperations.Clear();

        /// <summary>
        /// Draws the ellipse.
        /// </summary>
        /// <param name="frame">Frame.</param>
        public override void DrawEllipse(Rect frame)
        {
            // TODO: find a way to add this as a 'path op'
            throw new System.NotSupportedException("circles not supported by this visitor");
        }

        /// <summary>
        /// Visit the specified operation.
        /// </summary>
        /// <param name="moveTo">Operation.</param>
        public override void Visit(MoveTo moveTo) =>
            PathOperations.Add(moveTo);

        /// <summary>
        /// Visit the specified operation.
        /// </summary>
        /// <param name="lineTo">Operation.</param>
        public override void Visit(LineTo lineTo) =>
            PathOperations.Add(lineTo);

        /// <summary>
        /// Visit the specified operation.
        /// </summary>
        /// <param name="curveTo">Operation.</param>
        public override void Visit(CurveTo curveTo) =>
            PathOperations.Add(curveTo);

        /// <summary>
        /// Visit the specified operation.
        /// </summary>
        /// <param name="arcTo">Operation.</param>
        public override void Visit(ArcTo arcTo) =>
            PathOperations.Add(arcTo);

        /// <summary>
        /// Visit the specified operation.
        /// </summary>
        /// <param name="closePath">Operation.</param>
        public override void Visit(ClosePath closePath) =>
            PathOperations.Add(closePath);

        /// <summary>
        /// Gets the path result for the visitor
        /// </summary>
        IReadOnlyCollection<PathOp> IResolvePathOperations<IReadOnlyCollection<PathOp>>.GetPathResult() => 
            GetPathResult();
    }
}
