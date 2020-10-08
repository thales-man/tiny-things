// -----------------------------------------------------------------------------
// copyright (c) 2019, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
// -----------------------------------------------------------------------------
using Tiny.Framework.Helpers;
using Point = NGraphics.Point;

namespace Tiny.Framework.Services
{
    /// <summary>
    /// I Walk geometry path data.
    /// </summary>
    public interface IWalkGeometryPathData
    {
        /// <summary>
        /// Gets the current token.
        /// </summary>
        /// <value>The current token.</value>
        char CurrentToken { get; }

        /// <summary>
        /// Moves to start.
        /// </summary>
        void MoveToStart();

        /// <summary>
        /// Reads the token.
        /// </summary>
        /// <returns><c>true</c>, if token was  read, <c>false</c> otherwise.</returns>
        bool ReadToken();

        /// <summary>
        /// Ises the number.
        /// </summary>
        /// <returns><c>true</c>, if number was ised, <c>false</c> otherwise.</returns>
        /// <param name="commaAllowed">If set to <c>true</c> comma allowed.</param>
        bool IsNumber(bool commaAllowed = false);

        /// <summary>
        /// Reads the number.
        /// </summary>
        /// <returns>The number.</returns>
        /// <param name="commaAllowed">If set to <c>true</c> comma allowed.</param>
        double ReadNumber(bool commaAllowed = false);

        /// <summary>
        /// Reads the point.
        /// </summary>
        /// <returns>The point.</returns>
        /// <param name="theCommand">the command</param>
        /// <param name="theLastPoint">the last point</param>
        /// <param name="commaAllowed">If set to <c>true</c> comma allowed.</param>
        Point ReadPoint(GeometryCommand theCommand, Point theLastPoint = default, bool commaAllowed = false);

        /// <summary>
        /// Reads the bool.
        /// </summary>
        /// <returns><c>true</c>, if bool was  read, <c>false</c> otherwise.</returns>
        bool ReadBool();
    }
}
