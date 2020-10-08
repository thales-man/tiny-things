// -----------------------------------------------------------------------------
// copyright (c) 2019, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
// -----------------------------------------------------------------------------
using Tiny.Framework.Helpers;
using Point = NGraphics.Point;

namespace Tiny.Framework.Services.Internal
{
    /// <summary>
    /// Geometry state.
    /// </summary>
    public sealed class GeometryState
    {
        /// <summary>
        /// Gets the command.
        /// </summary>
        /// <value>The command.</value>
        public GeometryCommand Command { get; }

        /// <summary>
        /// Gets the last command.
        /// </summary>
        /// <value>The last command.</value>
        public GeometryCommand LastCommand { get; }

        /// <summary>
        /// Gets the destination.
        /// </summary>
        /// <value>The destination.</value>
        public Point Destination { get; }

        /// <summary>
        /// Gets the curve control point.
        /// </summary>
        /// <value>The curve control point.</value>
        public Point CurveControl { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Tiny.Framework.Services.Internal.GeometryState"/> class.
        /// </summary>
        /// <param name="command">command.</param>
        /// <param name="destination">Destination point.</param>
        private GeometryState(GeometryCommand command, GeometryCommand lastCommand, Point destination, Point curveControl)
        {
            Command = command;
            LastCommand = lastCommand;
            Destination = destination;
            CurveControl = curveControl;
        }

        /// <summary>
        /// Create request.
        /// </summary>
        /// <returns>The request state.</returns>
        public static GeometryState CreateRequest(GeometryCommand command, GeometryState lastState) =>
            new GeometryState(command, lastState.LastCommand, lastState.Destination, lastState.CurveControl);

        /// <summary>
        /// Create the specified lastCommand and destinationPoint.
        /// </summary>
        /// <returns>The create.</returns>
        /// <param name="lastCommand">Last command.</param>
        /// <param name="destination">Destination point.</param>
        public static GeometryState CreateResponse(GeometryCommand lastCommand, Point destination, Point curveControl) =>
            new GeometryState(GeometryCommand.Default, lastCommand, destination, curveControl);

        /// <summary>
        /// Creates the response.
        /// </summary>
        /// <returns>The response.</returns>
        /// <param name="lastCommand">Last command.</param>
        /// <param name="destination">Destination.</param>
        /// <param name="lastState">Last state.</param>
        public static GeometryState CreateResponse(GeometryCommand lastCommand, Point destination, GeometryState lastState) =>
            new GeometryState(GeometryCommand.Default, lastCommand, destination, lastState.CurveControl);

        /// <summary>
        /// Creates the response.
        /// </summary>
        /// <returns>The response.</returns>
        /// <param name="lastCommand">Last command.</param>
        /// <param name="lastState">Last state.</param>
        public static GeometryState CreateResponse(GeometryCommand lastCommand, GeometryState lastState) =>
            new GeometryState(GeometryCommand.Default, lastCommand, lastState.Destination, lastState.CurveControl);

        /// <summary>
        /// Empty instance.
        /// </summary>
        /// <returns>The empty instance.</returns>
        public static GeometryState Empty() =>
            new GeometryState(GeometryCommand.Default, GeometryCommand.Default, default(Point), default(Point));
    }
}
