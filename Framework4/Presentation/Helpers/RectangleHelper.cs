using System;
using Point = NGraphics.Point;
using Rect = NGraphics.Rect;
using Rectangle = Xamarin.Forms.Rectangle;

namespace Tiny.Framework.Helpers
{
    /// <summary>
    /// Rectangle extension.
    /// </summary>
    public static class RectangleHelper
    {
        /// <summary>
        /// Gets the middle of x.
        /// </summary>
        /// <returns>The middle of x.</returns>
        /// <param name="usingSource">Using source.</param>
        public static double GetMidX(this Rectangle usingSource) => 
            usingSource.Left + (usingSource.Width / 2f);

        /// <summary>
        /// Gets the middle of x.
        /// </summary>
        /// <returns>The middle of x.</returns>
        /// <param name="usingSource">Using source.</param>
        public static double GetMidX(this Rect usingSource) =>
            usingSource.Left + (usingSource.Width / 2f);

        /// <summary>
        /// Gets the middle of y.
        /// </summary>
        /// <returns>The middle of y.</returns>
        /// <param name="usingSource">Using source.</param>
        public static double GetMidY(this Rectangle usingSource) => 
            usingSource.Top + (usingSource.Height / 2f);

        /// <summary>
        /// Gets the middle of y.
        /// </summary>
        /// <returns>The middle of y.</returns>
        /// <param name="usingSource">Using source.</param>
        public static double GetMidY(this Rect usingSource) =>
            usingSource.Top + (usingSource.Height / 2f);

        /// <summary>
        /// Gets the middle point.
        /// </summary>
        /// <returns>The middle point.</returns>
        /// <param name="usingSource">Using source.</param>
        public static Point GetMidPoint(this Rectangle usingSource) =>
            new Point(usingSource.GetMidX(), usingSource.GetMidY());

        /// <summary>
        /// Gets the middle point.
        /// </summary>
        /// <returns>The middle point.</returns>
        /// <param name="usingSource">Using source.</param>
        public static Point GetMidPoint(this Rect usingSource) =>
            new Point(usingSource.GetMidX(), usingSource.GetMidY());

        /// <summary>
        /// Gets the negative middle point.
        /// used in the 'shift' back when translating scale
        /// </summary>
        /// <returns>The negative middle point.</returns>
        /// <param name="usingSource">Using source.</param>
        public static Point GetNegativeMidPoint(this Rectangle usingSource) =>
            new Point(-usingSource.GetMidX(), -usingSource.GetMidY());

        /// <summary>
        /// Gets the negative middle point.
        /// used in the 'shift' back when translating scale
        /// </summary>
        /// <returns>The negative middle point.</returns>
        /// <param name="usingSource">Using source.</param>
        public static Point GetNegativeMidPoint(this Rect usingSource) =>
            new Point(-usingSource.GetMidX(), -usingSource.GetMidY());

        /// <summary>
        /// Gets the scale.
        /// </summary>
        /// <returns>The scale.</returns>
        /// <param name="usingSource">Using source.</param>
        /// <param name="withinBounds">Within bounds.</param>
        public static double GetScale(this Rectangle usingSource, Rect withinBounds)
        {
            var xRatio = withinBounds.Width / usingSource.Width;
            var yRatio = withinBounds.Height / usingSource.Height;
            return Math.Min(xRatio, yRatio);
        }

        /// <summary>
        /// Gets the scale.
        /// </summary>
        /// <returns>The scale.</returns>
        /// <param name="usingSource">Using source.</param>
        /// <param name="withinBounds">Within bounds.</param>
        public static double GetScale(this Rect usingSource, Rect withinBounds)
        {
            var xRatio = withinBounds.Width / usingSource.Width;
            var yRatio = withinBounds.Height / usingSource.Height;
            return Math.Min(xRatio, yRatio);
        }
    }
}
