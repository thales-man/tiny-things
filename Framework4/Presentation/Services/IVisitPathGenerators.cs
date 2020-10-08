//-----------------------------------------------------------------------------
// copyright (c) 2016, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
//-----------------------------------------------------------------------------
using NGraphics;
using Point = NGraphics.Point;
using Rect = NGraphics.Rect;
using Rectangle = Xamarin.Forms.Rectangle;

namespace Tiny.Framework.Services
{
    /// <summary>
    /// I visit path generators.
    /// </summary>
    public interface IVisitPathGenerators :
        IPathOpVisitor
    {
        /// <summary>
        /// Gets the contents scale.
        /// </summary>
        /// <value>The contents scale.</value>
        double ContentsScale { get; }

        /// <summary>
        /// Gets the contents center.
        /// </summary>
        /// <value>The contents center.</value>
        Rect ContentsCenter { get; }

        /// <summary>
        /// Gets the current point.
        /// </summary>
        Point CurrentPoint { get; }

        /// <summary>
        /// Gets the curve continuation.
        /// </summary>
        Point CurveContinuation { get; }

        /// <summary>
        /// Draws the ellipse.
        /// </summary>
        /// <param name="frame">Frame.</param>
        void DrawEllipse(Rect frame);

        /// <summary>
        /// Sets the scale.
        /// </summary>
        /// <param name="usingViewport">Using viewport.</param>
        /// <param name="withinBounds">Within bounds.</param>
        void SetScale(Rect usingViewport, Rect withinBounds);

        /// <summary>
        /// Sets the scale.
        /// </summary>
        /// <param name="usingViewport">Using viewport.</param>
        /// <param name="withinBounds">Within bounds.</param>
        void SetScale(Rectangle usingViewport, Rect withinBounds);
    }
}
