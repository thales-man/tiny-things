// -----------------------------------------------------------------------------
// copyright (c) 2019, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
// -----------------------------------------------------------------------------
using System;
using System.Composition;
using Tiny.Framework.Helpers;
using Tiny.Framework.Providers;
using Point = NGraphics.Point;
using Size = NGraphics.Size;

namespace Tiny.Framework.Services.Internal
{
    /* a typical geometry
        M   504     256
        C   504     119 
            393       8 
            256       8
        S     8     119 
              8     256
        s   111     248 
            248     248 
            248    -111 
            248    -248
        z
        m  -448       0
        c     0    -110.5 
             89.5  -200 
            200    -200
        s   200      89.5 
            200     200
           - 89.5   200
           -200     200
        S    56     366.5 
             56     256
        z
        m    72      20
        v  - 40
        c     0    -  6.6
              5.4  - 12 
             12    - 12
        h   116
        v  - 67
        c     0    - 10.7 
             12.9  - 16 
             20.5  -  8.5
        l    99      99
        c     4.7     4.7 
              4.7    12.3 
              0      17
        l  - 99      99
        c  -  7.6     7.6
           - 20.5     2.2
           - 20.5  -  8.5
        v  - 67
        H   140
        c  -  6.6     0
           - 12    -  5.4
           - 12    - 12
        z
    */

    /// <summary>
    /// Geometry parser action provider.
    /// </summary>
    [Shared]
    [Export(typeof(IProvideGeometryParserActions))]
    public sealed class GeometryParserActionProvider :
        MappedContentProvider<GeometryCommand, Func<GeometryState, IAggregateGeometryPathOperations, IWalkGeometryPathData, GeometryState>>,
        IProvideGeometryParserActions
    {
        const bool AllowComma = true;

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="T:Tiny.Framework.Services.GeometryParserActionProvider"/> class.
        /// </summary>
        private GeometryParserActionProvider()
        {
            Add(GeometryCommand.MoveToAbsolute, MoveTo);
            Add(GeometryCommand.MoveToRelative, MoveTo);

            Add(GeometryCommand.LineToAbsolute, LineTo);
            Add(GeometryCommand.LineToRelative, LineTo);
            Add(GeometryCommand.HorzontalLineAbsolute, HorizontalLineToAbsolute);
            Add(GeometryCommand.HorzontalLineRelative, HorizontalLineToRelative);
            Add(GeometryCommand.VerticalLineAbsolute, VerticalLineToAbsolute);
            Add(GeometryCommand.VerticalLineRelative, VerticalLineToRelative);

            Add(GeometryCommand.CurveAbsolute, CurveTo);
            Add(GeometryCommand.CurveRelative, CurveTo);
            Add(GeometryCommand.SmoothCurveAbsolute, SmoothCurveTo);
            Add(GeometryCommand.SmoothCurveRelative, SmoothCurveTo);

            Add(GeometryCommand.QuadraticCurveAbsolute, QuadraticCurveTo);
            Add(GeometryCommand.QuadraticCurveRelative, QuadraticCurveTo);
            Add(GeometryCommand.QuadraticSmoothCurveAbsolute, SmoothQuadraticCurveTo);
            Add(GeometryCommand.QuadraticSmoothCurveRelative, SmoothQuadraticCurveTo);

            Add(GeometryCommand.ArcAbsolute, ArcTo);
            Add(GeometryCommand.ArcRelative, ArcTo);

            Add(GeometryCommand.CloseAbsolute, Close);
            Add(GeometryCommand.CloseRelative, Close);
        }

        /// <summary>
        /// Create this instance.
        /// </summary>
        /// <returns>The geometry parser action provider.</returns>
        public static IProvideGeometryParserActions Create() => new GeometryParserActionProvider();

        /// <summary>
        /// Is absolute curve.
        /// </summary>
        /// <returns><c>true</c>, if absolute curve was ised, <c>false</c> otherwise.</returns>
        /// <param name="theCommand">Cmd.</param>
        private bool IsAbsoluteCurve(GeometryCommand theCommand) => GeometryCommand.CurveAbsolute == theCommand;

        /// <summary>
        /// Is absolute quadratic curve.
        /// </summary>
        /// <returns><c>true</c>, if absolute quadratic curve, <c>false</c> otherwise.</returns>
        /// <param name="theCommand">Cmd.</param>
        private bool IsAbsoluteQuadraticCurve(GeometryCommand theCommand) => GeometryCommand.QuadraticCurveAbsolute == theCommand;

        /// <summary>
        /// Reflect the specified destinationPoint and curveControlPoint.
        /// </summary>
        /// <returns>The reflect.</returns>
        /// <param name="destination">Destination point.</param>
        /// <param name="curveControl">Curve control point.</param>
        private Point Reflect(Point destination, Point curveControl) => new Point(
            2 * destination.X - curveControl.X,
            2 * destination.Y - curveControl.Y);

        /// <summary>
        /// Fetchs the default.
        /// </summary>
        /// <returns>The default.</returns>
        /// <param name="theKey">Key.</param>
        public override Func<GeometryState, IAggregateGeometryPathOperations, IWalkGeometryPathData, GeometryState> FetchDefault(GeometryCommand theKey)
        {
            throw new FormatException($"failed here: {theKey}");
        }

        /// <summary>
        /// Move to.
        /// </summary>
        /// <returns>The state</returns>
        /// <param name="state">State.</param>
        /// <param name="context">Context.</param>
        /// <param name="walker">Walker.</param>
        public GeometryState MoveTo(GeometryState state, IAggregateGeometryPathOperations context, IWalkGeometryPathData walker)
        {
            var destination = walker.ReadPoint(state.Command, state.Destination);

            context.OpenPath(destination);

            var lastCommand = GeometryCommand.MoveToAbsolute;

            while (walker.IsNumber(true))
            {
                destination = walker.ReadPoint(state.Command, context.LastEndPoint());

                context.LineTo(destination);
                lastCommand = GeometryCommand.LineToAbsolute;
            }

            return GeometryState.CreateResponse(lastCommand, destination, state);
        }

        /// <summary>
        /// Line to.
        /// </summary>
        /// <returns>The state</returns>
        /// <param name="state">State.</param>
        /// <param name="context">Context.</param>
        /// <param name="walker">Walker.</param>
        public GeometryState LineTo(GeometryState state, IAggregateGeometryPathOperations context, IWalkGeometryPathData walker)
        {
            Point destination;

            do
            {
                destination = walker.ReadPoint(state.Command, context.LastEndPoint());
                context.LineTo(destination);

            } while (walker.IsNumber(AllowComma));

            return GeometryState.CreateResponse(GeometryCommand.LineToAbsolute, destination, state);
        }

        /// <summary>
        /// Horizontal line to absolute.
        /// </summary>
        /// <returns>The line to absolute.</returns>
        /// <param name="state">State.</param>
        /// <param name="context">Context.</param>
        /// <param name="walker">Walker.</param>
        public GeometryState HorizontalLineToAbsolute(GeometryState state, IAggregateGeometryPathOperations context, IWalkGeometryPathData walker)
        {
            var destination = state.Destination;

            do
            {
                destination.X = walker.ReadNumber();
                context.LineTo(destination);

            } while (walker.IsNumber(AllowComma));

            return GeometryState.CreateResponse(GeometryCommand.LineToAbsolute, destination, state);
        }

        /// <summary>
        /// Horizontal line to relative.
        /// </summary>
        /// <returns>The line to relative.</returns>
        /// <param name="state">State.</param>
        /// <param name="context">Context.</param>
        /// <param name="walker">Walker.</param>
        public GeometryState HorizontalLineToRelative(GeometryState state, IAggregateGeometryPathOperations context, IWalkGeometryPathData walker)
        {
            var destination = state.Destination;

            do
            {
                destination.X += walker.ReadNumber();
                context.LineTo(destination);

            } while (walker.IsNumber(AllowComma));

            return GeometryState.CreateResponse(GeometryCommand.LineToAbsolute, destination, state);
        }

        /// <summary>
        /// Vertical line to absolute.
        /// </summary>
        /// <returns>The line to absolute.</returns>
        /// <param name="state">State.</param>
        /// <param name="context">Context.</param>
        /// <param name="walker">Walker.</param>
        public GeometryState VerticalLineToAbsolute(GeometryState state, IAggregateGeometryPathOperations context, IWalkGeometryPathData walker)
        {
            var destination = state.Destination;

            do
            {
                destination.Y = walker.ReadNumber();
                context.LineTo(destination);

            } while (walker.IsNumber(AllowComma));

            return GeometryState.CreateResponse(GeometryCommand.LineToAbsolute, destination, state);
        }

        /// <summary>
        /// Vertical line to relative.
        /// </summary>
        /// <returns>The line to relative.</returns>
        /// <param name="state">State.</param>
        /// <param name="context">Context.</param>
        /// <param name="walker">Walker.</param>
        public GeometryState VerticalLineToRelative(GeometryState state, IAggregateGeometryPathOperations context, IWalkGeometryPathData walker)
        {
            var destination = state.Destination;

            do
            {
                destination.Y += walker.ReadNumber();
                context.LineTo(destination);

            } while (walker.IsNumber(AllowComma));

            return GeometryState.CreateResponse(GeometryCommand.LineToAbsolute, destination, state);
        }

        /// <summary>
        /// Curve to.
        /// </summary>
        /// <returns>The state</returns>
        /// <param name="state">State.</param>
        /// <param name="context">Context.</param>
        /// <param name="walker">Walker.</param>
        public GeometryState CurveTo(GeometryState state, IAggregateGeometryPathOperations context, IWalkGeometryPathData walker)
        {
            Point destination;
            Point curveControl;

            do
            {
                var control1 = walker.ReadPoint(state.Command, context.LastEndPoint());
                curveControl = walker.ReadPoint(state.Command, context.LastEndPoint(), AllowComma);
                destination = walker.ReadPoint(state.Command, context.LastEndPoint(), AllowComma);

                context.BezierTo(control1, curveControl, destination);

            } while (walker.IsNumber(AllowComma));

            return GeometryState.CreateResponse(GeometryCommand.CurveAbsolute, destination, curveControl);
        }

        /// <summary>
        /// Smooth curve to.
        /// </summary>
        /// <returns>The state</returns>
        /// <param name="state">State.</param>
        /// <param name="context">Context.</param>
        /// <param name="walker">Walker.</param>
        public GeometryState SmoothCurveTo(GeometryState state, IAggregateGeometryPathOperations context, IWalkGeometryPathData walker)
        {
            var destination = state.Destination;
            var curveControl = state.CurveControl;
            var lastCommand = state.LastCommand;

            do
            {
                var control1 = IsAbsoluteCurve(lastCommand)
                    ? Reflect(destination, curveControl)
                    : destination;
                curveControl = walker.ReadPoint(state.Command, context.LastEndPoint());
                destination = walker.ReadPoint(state.Command, context.LastEndPoint(), AllowComma);

                context.BezierTo(control1, curveControl, destination);

                lastCommand = GeometryCommand.CurveAbsolute;

            } while (walker.IsNumber(AllowComma));

            return GeometryState.CreateResponse(lastCommand, destination, curveControl);
        }

        /// <summary>
        /// Quadratic curve to.
        /// </summary>
        /// <returns>The state</returns>
        /// <param name="state">State.</param>
        /// <param name="context">Context.</param>
        /// <param name="walker">Walker.</param>
        public GeometryState QuadraticCurveTo(GeometryState state, IAggregateGeometryPathOperations context, IWalkGeometryPathData walker)
        {
            Point destination;
            Point curveControl;

            do
            {
                curveControl = walker.ReadPoint(state.Command, context.LastEndPoint());
                destination = walker.ReadPoint(state.Command, context.LastEndPoint(), AllowComma);

                context.QuadraticBezierTo(curveControl, destination);

            } while (walker.IsNumber(AllowComma));

            return GeometryState.CreateResponse(GeometryCommand.QuadraticCurveAbsolute, destination, curveControl);
        }

        /// <summary>
        /// Smooth quadratic curve to.
        /// </summary>
        /// <returns>The state</returns>
        /// <param name="state">State.</param>
        /// <param name="context">Context.</param>
        /// <param name="walker">Walker.</param>
        public GeometryState SmoothQuadraticCurveTo(GeometryState state, IAggregateGeometryPathOperations context, IWalkGeometryPathData walker)
        {
            var destination = state.Destination;
            var curveControl = state.CurveControl;
            var lastCommand = state.LastCommand;

            do
            {
                curveControl = IsAbsoluteQuadraticCurve(lastCommand)
                    ? Reflect(destination, curveControl)
                    : destination;
                destination = walker.ReadPoint(state.Command, context.LastEndPoint());

                context.QuadraticBezierTo(curveControl, destination);

                lastCommand = GeometryCommand.QuadraticCurveAbsolute;

            } while (walker.IsNumber(AllowComma));

            return GeometryState.CreateResponse(lastCommand, destination, curveControl);
        }

        /// <summary>
        /// Arc to.
        /// </summary>
        /// <returns>The state</returns>
        /// <param name="state">State.</param>
        /// <param name="context">Context.</param>
        /// <param name="walker">Walker.</param>
        public GeometryState ArcTo(GeometryState state, IAggregateGeometryPathOperations context, IWalkGeometryPathData walker)
        {
            Point destination;

            do
            {
                var width = walker.ReadNumber();
                var height = walker.ReadNumber(AllowComma);
                walker.ReadNumber(AllowComma); // <= we don't use the rotation
                var large = walker.ReadBool();
                var sweep = walker.ReadBool();

                destination = walker.ReadPoint(state.Command, context.LastEndPoint(), AllowComma);

                context.ArcTo(destination, new Size(width, height), large, sweep);

            } while (walker.IsNumber(AllowComma));

            return GeometryState.CreateResponse(GeometryCommand.ArcAbsolute, destination, state);
        }

        /// <summary>
        /// Close.
        /// </summary>
        /// <returns>The state</returns>
        /// <param name="state">State.</param>
        /// <param name="context">Context.</param>
        /// <param name="walker">Walker.</param>
        public GeometryState Close(GeometryState state, IAggregateGeometryPathOperations context, IWalkGeometryPathData walker)
        {
            context.ClosePath();

            return GeometryState.CreateResponse(GeometryCommand.CloseAbsolute, state);
        }
    }
}
