// -----------------------------------------------------------------------------
// copyright (c) 2019, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
// -----------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Globalization;
using Tiny.Framework.Helpers;
using Point = NGraphics.Point;

namespace Tiny.Framework.Services.Internal
{
    /// <summary>
    /// Geometry path data walker.
    /// </summary>
    internal sealed class GeometryPathDataWalker :
       IWalkGeometryPathData
    {
        /// <summary>
        /// allow sign.
        /// </summary>
        const bool AllowSign = true;

        /// <summary>
        /// allow comma.
        /// </summary>
        const bool AllowComma = true;

        /// <summary>
        /// The size of infinity.
        /// </summary>
        const int SizeOfInfinity = 8;

        /// <summary>
        /// The size of not a number.
        /// </summary>
        const int SizeOfNotANumber = 3;

        /// <summary>
        /// The size of a 32 bit integer.
        /// </summary>
        const int SizeOf32BitInteger = 8;

        /// <summary>
        /// The data path.
        /// </summary>
        private readonly string _dataPath;

        /// <summary>
        /// The length of the path.
        /// </summary>
        private readonly int _pathLength;

        /// <summary>
        /// The current position.
        /// </summary>
        private int _currentPosition;

        /// <summary>
        /// The numeric elements include 0 to 9, signs, infinity and NaN expressions.
        /// </summary>
        private static readonly HashSet<char> _numericElements = new HashSet<char> { '.', '-', '+', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'I', 'N' };

        /// <summary>
        /// The number elements are 0 to 9.
        /// </summary>
        private static readonly HashSet<char> _numberElements = new HashSet<char> { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };

        /// <summary>
        /// The sign elements are - and +.
        /// </summary>
        private static readonly HashSet<char> _signElements = new HashSet<char> { '-', '+' };

        /// <summary>
        /// The move to commands are M and m.
        /// </summary>
        private static readonly HashSet<char> _moveToCommands = new HashSet<char>
        {
            (char)GeometryCommand.MoveToAbsolute,
            (char)GeometryCommand.MoveToRelative
        };

        /// <summary>
        /// The (full set of) path commands.
        /// </summary>
        private static readonly HashSet<char> _pathCommands = new HashSet<char>
        {
            (char)GeometryCommand.FillPath,
            (char)GeometryCommand.ArcAbsolute,
            (char)GeometryCommand.ArcRelative,
            (char)GeometryCommand.CloseAbsolute,
            (char)GeometryCommand.CloseRelative,
            (char)GeometryCommand.CurveAbsolute,
            (char)GeometryCommand.CurveRelative,
            (char)GeometryCommand.HorzontalLineAbsolute,
            (char)GeometryCommand.HorzontalLineRelative,
            (char)GeometryCommand.LineToAbsolute,
            (char)GeometryCommand.LineToRelative,
            (char)GeometryCommand.MoveToAbsolute,
            (char)GeometryCommand.MoveToRelative,
            (char)GeometryCommand.QuadraticCurveAbsolute,
            (char)GeometryCommand.QuadraticCurveRelative,
            (char)GeometryCommand.QuadraticSmoothCurveAbsolute,
            (char)GeometryCommand.QuadraticSmoothCurveRelative,
            (char)GeometryCommand.SmoothCurveAbsolute,
            (char)GeometryCommand.SmoothCurveRelative,
            (char)GeometryCommand.VerticalLineAbsolute,
            (char)GeometryCommand.VerticalLineRelative,
        };

        private static readonly HashSet<GeometryCommand> _relativePathCommands = new HashSet<GeometryCommand>
        {
            GeometryCommand.ArcRelative,
            GeometryCommand.CloseRelative,
            GeometryCommand.CurveRelative,
            GeometryCommand.HorzontalLineRelative,
            GeometryCommand.LineToRelative,
            GeometryCommand.MoveToRelative,
            GeometryCommand.QuadraticCurveRelative,
            GeometryCommand.QuadraticSmoothCurveRelative,
            GeometryCommand.SmoothCurveRelative,
            GeometryCommand.VerticalLineRelative,
        };

        /// <summary>
        /// The whitespace elements.
        /// </summary>
        private static readonly HashSet<char> _whitespaceElements = new HashSet<char> { ' ', '\n', '\r', '\t' };

        private GeometryPathDataWalker(string dataPath)
        {
            _dataPath = dataPath;
            _pathLength = _dataPath.Length;
        }

        public static IWalkGeometryPathData Create(string dataPath) => 
            new GeometryPathDataWalker(dataPath);

        /// <summary>
        /// Gets the current token.
        /// </summary>
        /// <value>The current token.</value>
        public char CurrentToken { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this <see cref="T:BLEScanner.Shell.GeometryPathDataWalker"/> has more.
        /// </summary>
        /// <value><c>true</c> if has more; otherwise, <c>false</c>.</value>
        private bool HasMore => _currentPosition < _pathLength;

        private bool IsCurrentlySign => IsSign(_currentPosition);

        /// 'I' => Infinity
        private bool IsCurrentlyInfinity => _dataPath[_currentPosition] == 'I';

        /// 'N' => NaN
        private bool IsCurrentlyNotANumber => _dataPath[_currentPosition] == 'N';

        /// 'E' or 'e' => Exponential
        private bool IsCurrentlyExponential => (_dataPath[_currentPosition] == 'E') || (_dataPath[_currentPosition] == 'e');

        private bool IsCurrentlyANumericElement => IsNumericElement(_currentPosition);

        /// <summary>
        /// Is whitespace.
        /// </summary>
        /// <returns><c>true</c>, if whitespace, <c>false</c> otherwise.</returns>
        /// <param name="candidate">Candidate.</param>
        private bool IsWhitespace(int candidate) => _whitespaceElements.Contains(_dataPath[candidate]);

        /// <summary>
        /// Is sign.
        /// </summary>
        /// <returns><c>true</c>, if is sign, <c>false</c> otherwise.</returns>
        /// <param name="candidate">Candidate.</param>
        private bool IsSign(int candidate) => _signElements.Contains(_dataPath[candidate]);

        /// <summary>
        /// Is geometry command.
        /// </summary>
        /// <returns><c>true</c>, if is geometry command, <c>false</c> otherwise.</returns>
        /// <param name="candidate">Candidate.</param>
        private bool IsGeometryCommand(int candidate) => _pathCommands.Contains(_dataPath[candidate]);

        /// <summary>
        /// Is numeric element.
        /// </summary>
        /// <returns><c>true</c>, if is numeric element, <c>false</c> otherwise.</returns>
        /// <param name="candidate">Candidate.</param>
        private bool IsNumericElement(int candidate) => _numericElements.Contains(_dataPath[candidate]);

        /// <summary>
        /// Position implies negative.
        /// </summary>
        /// <returns><c>true</c>, if implies negative was positioned, <c>false</c> otherwise.</returns>
        /// <param name="candidate">Candidate.</param>
        private bool PositionImpliesNegative(int candidate) => _dataPath[candidate] == '-';

        /// <summary>
        /// Position implies precision.
        /// </summary>
        /// <returns><c>true</c>, if implies precision was positioned, <c>false</c> otherwise.</returns>
        /// <param name="candidate">Candidate.</param>
        private bool PositionImpliesPrecision(int candidate) => _dataPath[candidate] == '.';

        /// <summary>
        /// Position currently holds.
        /// </summary>
        /// <returns><c>true</c>, if position currently holds, <c>false</c> otherwise.</returns>
        /// <param name="candidate">Candidate.</param>
        private bool PositionCurrentlyHolds(char candidate) => _dataPath[_currentPosition] == candidate;

        /// <summary>
        /// Is relative.
        /// </summary>
        /// <returns><c>true</c>, if relative was ised, <c>false</c> otherwise.</returns>
        /// <param name="candidate">Candidate.</param>
        private bool IsRelative(GeometryCommand candidate) => _relativePathCommands.Contains(candidate);

        /// <summary>
        /// Throw unexpected token exception
        /// </summary>
        private void ThrowBadToken()
        {
            throw new FormatException($"failed here: {_dataPath[_currentPosition - 1]}, position:{_currentPosition}");
        }

        /// <summary>
        /// Finds the start.
        /// </summary>
        private void FindStart()
        {
            if (!string.IsNullOrWhiteSpace(_dataPath))
            {
                MoveNext();

                // if the first item encountered is 'F'
                if (PositionCurrentlyHolds((char)GeometryCommand.FillPath))
                {
                    _currentPosition++;

                    while (HasMore && IsWhitespace(_currentPosition))
                    {
                        _currentPosition++;
                    }

                    if (!HasMore)
                    {
                        throw new FormatException("Parser_IllegalToken");
                    }

                    // we read and discard the 'fill' value
                    ReadBool();
                    _currentPosition++;
                }
            }
        }

        /// <summary>
        /// Move next.
        /// </summary>
        /// <returns><c>true</c>, if next was moved, <c>false</c> otherwise.</returns>
        /// <param name="commaAllowed">If set to <c>true</c> allow comma.</param>
        private bool MoveNext(bool commaAllowed = false)
        {
            bool foundComma = false;

            while (HasMore)
            {
                if (!IsWhitespace(_currentPosition))
                {
                    if (PositionCurrentlyHolds(','))
                    {
                        if (!commaAllowed)
                        {
                            ThrowBadToken();
                        }

                        foundComma = true;
                        commaAllowed = false; // allow only one comma
                    }

                    else if (IsGeometryCommand(_currentPosition) || IsNumericElement(_currentPosition))
                    {
                        return foundComma;
                    }
                }

                _currentPosition++;
            }

            return foundComma;
        }

        /// <summary>
        /// Moves to end of number.
        /// </summary>
        /// <param name="signAllowed">If set to <c>true</c> sign allowed.</param>
        private void MoveToEndOfNumber(bool signAllowed = false)
        {
            // cater for a sign
            if (signAllowed && HasMore && IsCurrentlySign)
            {
                _currentPosition++;
            }

            while (HasMore && _numberElements.Contains(_dataPath[_currentPosition]))
            {
                _currentPosition++;
            }
        }

        /// <summary>
        /// Moves to start.
        /// </summary>
        public void MoveToStart()
        {
            _currentPosition = 0;
            FindStart();
            ReadToken();

            // the opening token has to be 'M' or 'm'
            if (!_moveToCommands.Contains(CurrentToken))
            {
                ThrowBadToken();
            }
        }

        /// <summary>
        /// Is number.
        /// </summary>
        /// <returns><c>true</c>, if is number, <c>false</c> otherwise.</returns>
        /// <param name="commaAllowed">If set to <c>true</c> allow comma.</param>
        public bool IsNumber(bool commaAllowed = false)
        {
            var foundComma = MoveNext(commaAllowed);

            if (HasMore)
            {
                CurrentToken = _dataPath[_currentPosition];

                if (IsCurrentlyANumericElement)
                {
                    return true;
                }
            }

            // commas only allowed between numbers
            if (foundComma)
            {
                ThrowBadToken();
            }

            return false;
        }

        /// <summary>
        /// Read the next non whitespace character
        /// </summary>
        /// <returns>True if not end of string</returns>
        public bool ReadToken()
        {
            MoveNext();

            if (HasMore)
            {
                CurrentToken = _dataPath[_currentPosition++];

                return true;
            }

            return false;
        }

        /// <summary>
        /// Read a bool: 1 or 0
        /// </summary>
        /// <returns>the result of the read</returns>
        public bool ReadBool()
        {
            MoveNext(AllowComma);

            if (HasMore)
            {
                CurrentToken = _dataPath[_currentPosition++];

                if (CurrentToken == '0')
                {
                    return false;
                }

                if (CurrentToken == '1')
                {
                    return true;
                }
            }

            ThrowBadToken();

            return false;
        }

        /// <summary>
        /// Read a floating point number
        /// </summary>
        /// <returns>the number read</returns>
        public double ReadNumber(bool commaAllowed = false)
        {
            if (!IsNumber(commaAllowed))
            {
                ThrowBadToken();
            }

            var simple = true;
            var start = _currentPosition;

            // check for a sign and increment
            if (HasMore && IsCurrentlySign)
            {
                _currentPosition++;
            }

            // check for Infinity
            if (HasMore && IsCurrentlyInfinity)
            {
                // set the read to the size of "Infinity" (8 characters)
                _currentPosition = Math.Min(_currentPosition + SizeOfInfinity, _pathLength);
                simple = false;
            }

            // Check for NaN
            else if (HasMore && IsCurrentlyNotANumber)
            {
                // set the read to the size of "NaN" (3 characters)
                _currentPosition = Math.Min(_currentPosition + SizeOfNotANumber, _pathLength);
                simple = false;
            }
            else
            {
                MoveToEndOfNumber();

                // check for precision
                if (HasMore && PositionImpliesPrecision(_currentPosition))
                {
                    simple = false;
                    _currentPosition++;
                    MoveToEndOfNumber();
                }

                // check for exponent
                if (HasMore && IsCurrentlyExponential)
                {
                    simple = false;
                    _currentPosition++;
                    MoveToEndOfNumber(AllowSign);
                }
            }

            if (simple && (_currentPosition <= (start + SizeOf32BitInteger)))
            {
                var sign = PositionImpliesNegative(start) ? -1 : 1;
                if (IsSign(start))
                {
                    start++;
                }

                int value = 0;

                while (start < _currentPosition)
                {
                    value = value * 10 + (_dataPath[start] - '0');
                    start++;
                }

                return value * sign;
            }

            var candidate = _dataPath.Substring(start, _currentPosition - start);

            try
            {
                return Convert.ToDouble(candidate, CultureInfo.InvariantCulture);
            }
            catch (FormatException except)
            {
                Console.WriteLine(except.Message);
                throw new FormatException();
            }
        }

        /// <summary>
        /// Read a relative point
        /// </summary>
        /// <returns>The point.</returns>
        /// <param name="theCommand">the command.</param>
        /// <param name="theLastPoint">the last point.</param>
        /// <param name="commaAllowed">If set to <c>true</c> comma allowed.</param>
        public Point ReadPoint(GeometryCommand theCommand, Point theLastPoint = default, bool commaAllowed = false)
        {
            var x = ReadNumber(commaAllowed);
            var y = ReadNumber(AllowComma);

            if (IsRelative(theCommand))
            {
                x += theLastPoint.X;
                y += theLastPoint.Y;
            }

            return new Point(x, y);
        }
    }
}
