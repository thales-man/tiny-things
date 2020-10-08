//-----------------------------------------------------------------------------
// copyright (c) 2019, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
//-----------------------------------------------------------------------------
using System;
using CoreGraphics;
using NGraphics;
using Tiny.Framework.Helpers;

namespace Tiny.Framework.Services.Internal
{
    /// <summary>
    /// Platform path operations adapter.
    /// </summary>
    public class PlatformPathOperationsAdapter :
        PathOperationsAdapter<CGPath>,
        IAdaptPlatformPathOperations
    {
        /// <summary>
        /// Gets the current point.
        /// </summary>
        /// <value>The current point.</value>
        public override Point CurrentPoint => PathOperations.CurrentPoint.GetPoint();

        /// <summary>
        /// Gets the curve continuation.
        /// i don't think this is right...
        /// there may not be an operation to fulfill this call
        /// </summary>
        /// <value>The curve continuation.</value>
        public override Point CurveContinuation => PathOperations.CurrentPoint.GetPoint();

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
            PathOperations.AddEllipseInRect(new CGRect(frame.GetMidX() - radius, frame.GetMidY() - radius, frame.Width, frame.Width));
        }

        /// <summary>
        /// Visit the specified operation.
        /// </summary>
        /// <param name="moveTo">Operation.</param>
        public override void Visit(MoveTo moveTo)
        {

            var p = moveTo.Point;

            if (!IsValid(p.X) || !IsValid(p.Y))
            {
                return;
            }

            PathOperations.MoveToPoint((nfloat)p.X, (nfloat)p.Y);
        }

        /// <summary>
        /// Visit the specified operation.
        /// </summary>
        /// <param name="lineTo">Operation.</param>
        public override void Visit(LineTo lineTo)
        {

            var p = lineTo.Point;

            if (!IsValid(p.X) || !IsValid(p.Y))
            {
                return;
            }

            PathOperations.AddLineToPoint((nfloat)p.X, (nfloat)p.Y);
        }

        /// <summary>
        /// Visit the specified operation.
        /// </summary>
        /// <param name="curveTo">Operation.</param>
        public override void Visit(CurveTo curveTo)
        {
            var p = curveTo.Point;

            if (!IsValid(p.X) || !IsValid(p.Y))
            {
                return;
            }

            var c1 = curveTo.Control1;
            var c2 = curveTo.Control2;

            if (!IsValid(c1.X) || !IsValid(c1.Y) || !IsValid(c2.X) || !IsValid(c2.Y))
            {
                PathOperations.MoveToPoint((nfloat)p.X, (nfloat)p.Y);
                return;
            }

            PathOperations.AddCurveToPoint((nfloat)c1.X, (nfloat)c1.Y, (nfloat)c2.X, (nfloat)c2.Y, (nfloat)p.X, (nfloat)p.Y);
        }

        /// <summary>
        /// Visit the specified operation.
        /// </summary>
        /// <param name="arcTo">Operation.</param>
        public override void Visit(ArcTo arcTo)
        {
            var p = arcTo.Point;

            if (!IsValid(p.X) || !IsValid(p.Y))
            {
                return;
            }

            var pp = CurrentPoint;

            if (pp == p)
            {
                return;
            }

            arcTo.GetCircles(pp, out Point c1, out Point c2);

            var circleCenter = (arcTo.LargeArc ^ arcTo.SweepClockwise) ? c1 : c2;
            var startAngle = (float)Math.Atan2(pp.Y - circleCenter.Y, pp.X - circleCenter.X);
            var endAngle = (float)Math.Atan2(p.Y - circleCenter.Y, p.X - circleCenter.X);

            if (!IsValid(circleCenter.X) || !IsValid(circleCenter.Y) || !IsValid(startAngle) || !IsValid(endAngle))
            {
                PathOperations.MoveToPoint((nfloat)p.X, (nfloat)p.Y);
                return;
            }

            var clockwise = !arcTo.SweepClockwise;

            PathOperations.AddArc((nfloat)circleCenter.X, (nfloat)circleCenter.Y, (nfloat)arcTo.Radius.Min, startAngle, endAngle, clockwise);
        }

        /// <summary>
        /// Visit the specified operation.
        /// </summary>
        /// <param name="closePath">Operation.</param>
        public override void Visit(ClosePath closePath)
        {
            PathOperations.CloseSubpath();
        }
    }
}
