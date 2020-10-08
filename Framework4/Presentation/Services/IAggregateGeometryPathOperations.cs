// -----------------------------------------------------------------------------
// copyright (c) 2019, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
// -----------------------------------------------------------------------------
using NGraphics;

namespace Tiny.Framework.Services
{
    /// <summary>
    /// I aggregate path operations.
    /// </summary>
    public interface IAggregateGeometryPathOperations
    {
        /// <summary>
        /// Opens the path.
        /// will not clear any existing path, this is an accumulative operation
        /// </summary>
        /// <param name="theFirstPoint">First point.</param>
        void OpenPath(Point theFirstPoint);

        /// <summary>
        /// Line to.
        /// </summary>
        /// <param name="theDestination">Destination</param>
        void LineTo(Point theDestination);

        /// <summary>
        /// Arc to.
        /// </summary>
        /// <param name="destination">Destination</param>
        /// <param name="radius">Radius</param>
        /// <param name="large">If set to <c>true</c> large.</param>
        /// <param name="sweep">If set to <c>true</c> sweep.</param>
        void ArcTo(Point destination, Size radius, bool large, bool sweep);

        /// <summary>
        /// Bezier curve to.
        /// </summary>
        /// <param name="firstControl">First control.</param>
        /// <param name="secondControl">Second control.</param>
        /// <param name="destination">Destination.</param>
        void BezierTo(Point firstControl, Point secondControl, Point destination);

        /// <summary>
        /// Quadratic bezier curve to.
        /// </summary>
        /// <param name="secondControl">Second control.</param>
        /// <param name="destination">Destination.</param>
        void QuadraticBezierTo(Point secondControl, Point destination);

        /// <summary>
        /// Closes the path.
        /// </summary>
        void ClosePath();

        /// <summary>
        /// Last end point.
        /// </summary>
        /// <returns>The end point.</returns>
        Point LastEndPoint();
    }
}
