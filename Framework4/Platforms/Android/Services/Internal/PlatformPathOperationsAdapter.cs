// -----------------------------------------------------------------------------
// copyright (c) 2019, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
// -----------------------------------------------------------------------------
using System;
using NGraphics;
using Tiny.Framework.Helpers;
using Point = NGraphics.Point;
using Rect = NGraphics.Rect;

namespace Tiny.Framework.Services.Internal
{
    /// <summary>
    /// Platform path operations adapter.
    /// </summary>
    public class PlatformPathOperationsAdapter :
        PathOperationsAdapter<Android.Graphics.Path>,
        IAdaptPlatformPathOperations
    {
        /// <summary>
        /// The last point.
        /// </summary>
        private Point _lastPoint;

        /// <summary>
        /// Gets the current point.
        /// </summary>
        /// <value>The current point.</value>
        public override Point CurrentPoint => _lastPoint;

        /// <summary>
        /// Gets the curve continuation.
        /// i don't think this is right...
        /// there may not be an operation to fulfill this call
        /// </summary>
        /// <value>The curve continuation.</value>
        public override Point CurveContinuation => _lastPoint;

        /// <summary>
        /// Is valid.
        /// </summary>
        /// <returns><c>true</c>, if valid was ised, <c>false</c> otherwise.</returns>
        /// <param name="v">V.</param>
        private static bool IsValid(double v)
        {
            return !double.IsNaN(v) && !double.IsInfinity(v);
        }

        /// <summary>
        /// Draws the ellipse.
        /// </summary>
        /// <param name="frame">Frame.</param>
        public override void DrawEllipse(Rect frame)
        {
            var radius = frame.Width / 2;
            PathOperations.AddCircle((float)frame.GetMidX(), (float)frame.GetMidY(), (float)radius, Android.Graphics.Path.Direction.Cw);
        }

        /// <summary>
        /// Visit the specified operation.
        /// </summary>
        /// <param name="moveTo">Operation.</param>
        public override void Visit(MoveTo moveTo)
        {
            _lastPoint = moveTo.Point;

            if (!IsValid(CurrentPoint.X) || !IsValid(CurrentPoint.Y))
            {
                return;
            }

            PathOperations.MoveTo((float)CurrentPoint.X, (float)CurrentPoint.Y);
        }

        /// <summary>
        /// Visit the specified operation.
        /// </summary>
        /// <param name="lineTo">Operation.</param>
        public override void Visit(LineTo lineTo)
        {
            _lastPoint = lineTo.Point;

            if (!IsValid(CurrentPoint.X) || !IsValid(CurrentPoint.Y))
            {
                return;
            }

            PathOperations.LineTo((float)CurrentPoint.X, (float)CurrentPoint.Y);
        }

        /// <summary>
        /// Visit the specified operation.
        /// </summary>
        /// <param name="curveTo">Operation.</param>
        public override void Visit(CurveTo curveTo)
        {
            _lastPoint = curveTo.Point;

            if (!IsValid(CurrentPoint.X) || !IsValid(CurrentPoint.Y))
            {
                return;
            }

            var c1 = curveTo.Control1;
            var c2 = curveTo.Control2;

            if (!IsValid(c1.X) || !IsValid(c1.Y) || !IsValid(c2.X) || !IsValid(c2.Y))
            {
                PathOperations.MoveTo((float)CurrentPoint.X, (float)CurrentPoint.Y);
                return;
            }

            PathOperations.CubicTo((float)c1.X, (float)c1.Y, (float)c2.X, (float)c2.Y, (float)CurrentPoint.X, (float)CurrentPoint.Y);
        }

        /// <summary>
        /// Visit the specified operation.
        /// </summary>
        /// <param name="arcTo">Operation.</param>
        public override void Visit(ArcTo arcTo)
        {
            var pp = CurrentPoint;
            _lastPoint = arcTo.Point;

            if (!IsValid(CurrentPoint.X) || !IsValid(CurrentPoint.Y))
            {
                return;
            }

            if (pp == CurrentPoint)
            {
                return;
            }

            arcTo.GetCircles(pp, out Point c1, out Point c2);

            var circleCenter = (arcTo.LargeArc ^ arcTo.SweepClockwise) ? c1 : c2;
            var startAngle = (float)Math.Atan2(pp.Y - circleCenter.Y, pp.X - circleCenter.X);
            var endAngle = (float)Math.Atan2(CurrentPoint.Y - circleCenter.Y, CurrentPoint.X - circleCenter.X);

            if (!IsValid(circleCenter.X) || !IsValid(circleCenter.Y) || !IsValid(startAngle) || !IsValid(endAngle))
            {
                PathOperations.MoveTo((float)CurrentPoint.X, (float)CurrentPoint.Y);
                return;
            }

            PathOperations.AddArc((float)circleCenter.X, (float)circleCenter.Y, (float)arcTo.Radius.Min, (float)arcTo.Radius.Max, startAngle, endAngle);
        }

        /// <summary>
        /// Visit the specified operation.
        /// </summary>
        /// <param name="closePath">Operation.</param>
        public override void Visit(ClosePath closePath)
        {
            PathOperations.Close();
        }
    }
}
