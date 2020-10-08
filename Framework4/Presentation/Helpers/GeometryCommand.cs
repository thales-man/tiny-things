// -----------------------------------------------------------------------------
// copyright (c) 2019, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
// -----------------------------------------------------------------------------
namespace Tiny.Framework.Helpers
{
    /// <summary>
    /// Geometry command.
    /// </summary>
    public enum GeometryCommand
    {
        /// <summary>
        /// not set is the default
        /// </summary>
        Default = ' ',

        /// <summary>
        /// The fill path.
        /// </summary>
        FillPath = 'F',

        /// <summary>
        /// move to absolute.
        /// </summary>
        MoveToAbsolute = 'M',

        /// <summary>
        /// move to relative.
        /// </summary>
        MoveToRelative = 'm',

        /// <summary>
        /// line to absolute.
        /// </summary>
        LineToAbsolute = 'L',

        /// <summary>
        /// line to relative.
        /// </summary>
        LineToRelative = 'l',

        /// <summary>
        /// close absolute.
        /// </summary>
        CloseAbsolute = 'Z',

        /// <summary>
        /// close relative.
        /// </summary>
        CloseRelative = 'z',

        /// <summary>
        /// horzontal line absolute.
        /// </summary>
        HorzontalLineAbsolute = 'H',

        /// <summary>
        /// horzontal line relative.
        /// </summary>
        HorzontalLineRelative = 'h',

        /// <summary>
        /// vertical line absolute.
        /// </summary>
        VerticalLineAbsolute = 'V',

        /// <summary>
        /// vertical line relative.
        /// </summary>
        VerticalLineRelative = 'v',

        /// <summary>
        /// curve absolute.
        /// </summary>
        CurveAbsolute = 'C',

        /// <summary>
        /// curve relative.
        /// </summary>
        CurveRelative = 'c',

        /// <summary>
        /// smooth curve absolute.
        /// </summary>
        SmoothCurveAbsolute = 'S',

        /// <summary>
        /// smooth curve relative.
        /// </summary>
        SmoothCurveRelative = 's',

        /// <summary>
        /// quadratic curve absolute.
        /// </summary>
        QuadraticCurveAbsolute = 'Q',

        /// <summary>
        /// quadratic curve relative.
        /// </summary>
        QuadraticCurveRelative = 'q',

        /// <summary>
        /// quadratic smooth curve absolute.
        /// </summary>
        QuadraticSmoothCurveAbsolute = 'T',

        /// <summary>
        /// quadratic smooth curve relative.
        /// </summary>
        QuadraticSmoothCurveRelative = 't',

        /// <summary>
        /// arc absolute 
        /// </summary>
        ArcAbsolute = 'A',

        /// <summary>
        /// arc relative.
        /// </summary>
        ArcRelative = 'a'
    }
}
