// -----------------------------------------------------------------------------
// copyright (c) 2019, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
// -----------------------------------------------------------------------------
using NGraphics;

namespace Tiny.Framework.Services.Internal
{
    /// <summary>
    /// Path operations aggregator.
    /// </summary>
    internal sealed class GeometryPathOperations :
        IAggregateGeometryPathOperations
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:Tiny.Framework.Services.Internal.GeometryPathOperations"/> class.
        /// </summary>
        /// <param name="theVisitor">The visitor.</param>
        private GeometryPathOperations(IVisitPathGenerators theVisitor) =>
            PathOperations = theVisitor;

        /// <summary>
        /// Create the geometry path operation aggregator.
        /// </summary>
        /// <returns>The geometry path operation aggregator.</returns>
        /// <param name="theVisitor">The visitor.</param>
        public static IAggregateGeometryPathOperations Create(IVisitPathGenerators theVisitor) =>
            new GeometryPathOperations(theVisitor);

        /// <summary>
        /// Gets the path operations.
        /// </summary>
        /// <value>The path operations.</value>
        public IVisitPathGenerators PathOperations { get; }

        /// <summary>
        /// Opens the path.
        /// will not clear any existing path, this is an accumulative operation
        /// </summary>
        /// <param name="theFirstPoint">First point.</param>
        public void OpenPath(Point theFirstPoint) =>
            PathOperations.Visit(new MoveTo(theFirstPoint));

        /// <summary>
        /// Line to.
        /// </summary>
        /// <param name="theDestination">destination</param>
        public void LineTo(Point theDestination) =>
            PathOperations.Visit(new LineTo(theDestination));

        /// <summary>
        /// Arc to.
        /// </summary>
        /// <param name="destination">destination</param>
        /// <param name="radius">Radius.</param>
        /// <param name="large">If set to <c>true</c> large.</param>
        /// <param name="sweep">If set to <c>true</c> sweep.</param>
        public void ArcTo(Point destination, Size radius, bool large, bool sweep) =>
            PathOperations.Visit(new ArcTo(radius, large, sweep, destination));

        /// <summary>
        /// Bezier curve to.
        /// </summary>
        /// <param name="firstControl">First control.</param>
        /// <param name="secondControl">Second control.</param>
        /// <param name="destination">Destination.</param>
        public void BezierTo(Point firstControl, Point secondControl, Point destination) =>
            PathOperations.Visit(new CurveTo(firstControl, secondControl, destination));

        /// <summary>
        /// Quadratic bezier curve to.
        /// </summary>
        /// <param name="secondControl">Second control.</param>
        /// <param name="destination">Destination.</param>
        public void QuadraticBezierTo(Point secondControl, Point destination) =>
            PathOperations.Visit(new CurveTo(PathOperations.CurveContinuation, secondControl, destination));

        /// <summary>
        /// Close the path.
        /// </summary>
        public void ClosePath() =>
            PathOperations.Visit(new ClosePath());

        /// <summary>
        /// Last end point.
        /// </summary>
        /// <returns>The end point.</returns>
        public Point LastEndPoint() =>
            PathOperations.CurrentPoint;
    }
}
