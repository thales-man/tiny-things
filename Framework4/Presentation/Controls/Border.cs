using System;
using NGraphics;
using Tiny.Framework.Helpers;
using Tiny.Framework.Services;
using Tiny.Framework.Views;
using Xamarin.Forms;
using XFColor = Xamarin.Forms.Color;
using NGColor = NGraphics.Color;
using NGPoint = NGraphics.Point;

namespace Tiny.Framework.Controls
{
    /// <summary>
    /// Border control.
    /// </summary>
    public class Border : 
        CustomControlView
    {
        SolidBrush _backgroundBrush;
        NGColor _borderColour;
        // the size
        private double width = 300;
        private double height = 300;
        // the sides
        private double _thicknessLeft = 15;
        private double _thicknessTop = 15;
        private double _thicknessRight = 15;
        private double _thicknessBottom = 15;
        // the corners
        private double _radiusLeft = 40;
        private double _radiusTop = 70;
        private double _radiusRight = 30;
        private double _radiusBottom = 60;
        // used for the corner and sides calculations
        private double _halfThicknessLeft;
        private double _halfThicknessTop;
        private double _halfThicknessRight;
        private double _halfThicknessBottom;

        /// <summary>
        /// The corner radius property.
        /// </summary>
        public static readonly BindableProperty CornerRadiusProperty =
            BindableProperty.Create(nameof(CornerRadius), typeof(Thickness), typeof(Border), default(Thickness));

        /// <summary>
        /// The border color property.
        /// </summary>
        public static readonly BindableProperty BorderColorProperty =
            BindableProperty.Create(nameof(BorderColor), typeof(XFColor), typeof(Border), XFColor.Transparent);

        /// <summary>
        /// The border thickness property.
        /// </summary>
        public static readonly BindableProperty BorderThicknessProperty =
            BindableProperty.Create(nameof(BorderThickness), typeof(Thickness), typeof(Border), default(Thickness));

        /// <summary>
        /// The background color property.
        /// </summary>
        public static readonly new BindableProperty BackgroundColorProperty =
            BindableProperty.Create(nameof(BackgroundColor), typeof(XFColor), typeof(Border), XFColor.Transparent);

        /// <summary>
        /// Initializes a new instance of the <see cref="Border"/> class.
        /// </summary>
        public Border() => SetRequiresMask(true);

        /// <summary>
        /// Gets or sets the corner radius.
        /// </summary>
        /// <value>The corner radius.</value>
        public Thickness CornerRadius
        {
            get => (Thickness)GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }

        /// <summary>
        /// Gets or sets the color of the border.
        /// </summary>
        /// <value>The color of the border.</value>
        public XFColor BorderColor
        {
            get => (XFColor)GetValue(BorderColorProperty);
            set => SetValue(BorderColorProperty, value);
        }

        /// <summary>
        /// Gets or sets the border thickness.
        /// </summary>
        /// <value>The border thickness.</value>
        public Thickness BorderThickness
        {
            get => (Thickness)GetValue(BorderThicknessProperty);
            set => SetValue(BorderThicknessProperty, value);
        }

        /// <summary>
        /// Gets or sets the color of the background.
        /// </summary>
        /// <value>The color of the background.</value>
        public new XFColor BackgroundColor
        {
            get => (XFColor)GetValue(BackgroundColorProperty);
            set => SetValue(BackgroundColorProperty, value);
        }

        /// <summary>
        /// Type of line offset.
        /// </summary>
        public enum TypeOfLineOffset { Midway, ControlMask, ContentMask };

        /// <summary>
        /// Gets the line offset.
        /// </summary>
        /// <returns>The line offset.</returns>
        /// <param name="thickness">Thickness.</param>
        /// <param name="lineOffset">Line offset.</param>
        public double GetLineOffset(double thickness, TypeOfLineOffset lineOffset)
        {
            switch (lineOffset)
            {
                case TypeOfLineOffset.ControlMask: 
                    return 0;
                case TypeOfLineOffset.ContentMask: 
                    return thickness + 1;
                default:
                    return thickness > 0 ? thickness / 2 : 0;
            }
        }

        /// <summary>
        /// Draw on Canvas and within Bounds.
        /// </summary>
        /// <param name="onCanvas">On canvas.</param>
        /// <param name="withinBounds">Within bounds.</param>
        public override void Draw(ICanvas onCanvas, Rect withinBounds)
        {
            // draw and fill the box
            var pathOps = new ControlPathOperationsAdapter();
            DoFullPath(pathOps, withinBounds, TypeOfLineOffset.Midway);
            onCanvas.DrawPath(pathOps.GetPathResult(), null, _backgroundBrush);

            // draw the top border and top right curve
            pathOps.ClearPath();
            DoRightCornerPath(pathOps, _radiusRight > 0);
            onCanvas.DrawPath(pathOps.GetPathResult(), new Pen(_borderColour, _thicknessTop), null);

            // draw the right border and bottom right curve
            pathOps.ClearPath();
            DoBottomCornerPath(pathOps, _radiusBottom > 0);
            onCanvas.DrawPath(pathOps.GetPathResult(), new Pen(_borderColour, _thicknessRight), null);

            // draw the bottom border and bottom left curve
            pathOps.ClearPath();
            DoLeftCornerPath(pathOps, _radiusLeft > 0);
            onCanvas.DrawPath(pathOps.GetPathResult(), new Pen(_borderColour, _thicknessBottom), null);

            // draw the left border and top left curve
            pathOps.ClearPath();
            DoTopCornerPath(pathOps, _radiusTop > 0);
            onCanvas.DrawPath(pathOps.GetPathResult(), new Pen(_borderColour, _thicknessLeft), null);
        }

        /// <summary>
        /// Prepares the locals.
        /// </summary>
        /// <param name="withinBounds">Within bounds.</param>
        /// <param name="offsetType">Offset type.</param>
        public void PrepareLocals(Rect withinBounds, TypeOfLineOffset offsetType)
        {
            _backgroundBrush = new SolidBrush(BackgroundColor.AsNGColor());

            width = withinBounds.Width;
            height = withinBounds.Height;

            _borderColour = BorderColor.AsNGColor();

            _radiusLeft = CornerRadius.Left;
            _radiusRight = CornerRadius.Right;
            _radiusBottom = CornerRadius.Bottom;
            _radiusTop = CornerRadius.Top;

            _thicknessLeft = BorderThickness.Left;
            _thicknessTop = BorderThickness.Top;
            _thicknessRight = BorderThickness.Right;
            _thicknessBottom = BorderThickness.Bottom;

            _halfThicknessLeft = GetLineOffset(_thicknessLeft, offsetType);
            _halfThicknessTop = GetLineOffset(_thicknessTop, offsetType);
            _halfThicknessRight = GetLineOffset(_thicknessRight, offsetType);
            _halfThicknessBottom = GetLineOffset(_thicknessBottom, offsetType);

            // go round the corners and decide if there are any we can't do
            if (Math.Abs(_thicknessLeft - _thicknessTop) > 0)
            {
                _radiusTop = 0;
            }

            if (Math.Abs(_thicknessTop - _thicknessRight) > 0)
            {
                _radiusRight = 0;
            }

            if (Math.Abs(_thicknessRight - _thicknessBottom) > 0)
            {
                _radiusBottom = 0;
            }

            if (Math.Abs(_thicknessBottom - _thicknessLeft) > 0)
            {
                _radiusLeft = 0;
            }
        }

        /// <summary>
        /// Gets the control mask.
        /// </summary>
        /// <param name="usingVisitor">Using visitor.</param>
        /// <param name="withinBounds">Within bounds.</param>
        public override void GetControlMask(IVisitPathGenerators usingVisitor, Rect withinBounds)
        {
            DoFullPath(usingVisitor, withinBounds, TypeOfLineOffset.ControlMask);
        }

        /// <summary>
        /// Gets the content mask.
        /// </summary>
        /// <param name="usingVisitor">Using visitor.</param>
        /// <param name="withinBounds">Within bounds.</param>
        public override void GetContentMask(IVisitPathGenerators usingVisitor, Rect withinBounds)
        {
            DoFullPath(usingVisitor, withinBounds, TypeOfLineOffset.ContentMask);
        }

        /// <summary>
        /// Does the full path.
        /// </summary>
        /// <param name="usingVisitor">Using visitor.</param>
        /// <param name="withinBounds">Within bounds.</param>
        /// <param name="offset">Offset.</param>
        public void DoFullPath(IVisitPathGenerators usingVisitor, Rect withinBounds, TypeOfLineOffset offset)
        {
            PrepareLocals(withinBounds, offset);

            usingVisitor.SetScale(Bounds, withinBounds);

            DoRightCornerPath(usingVisitor, _radiusRight > 0);
            DoBottomCornerPath(usingVisitor, _radiusBottom > 0, false);
            DoLeftCornerPath(usingVisitor, _radiusLeft > 0, false);
            DoTopCornerPath(usingVisitor, _radiusTop > 0, false);

            usingVisitor.Visit(new ClosePath());
        }

        /// <summary>
        /// Does the top corner path.
        /// </summary>
        /// <param name="usingVisitor">Using visitor.</param>
        /// <param name="hasCurve">If set to <c>true</c> has curve.</param>
        /// <param name="applyMove">If set to <c>true</c> apply move.</param>
        public void DoTopCornerPath(IPathOpVisitor usingVisitor, bool hasCurve, bool applyMove = true)
        {
            if (applyMove)
            {
                usingVisitor.Visit(new MoveTo(_halfThicknessLeft, height - _radiusLeft));
            }

            if (hasCurve)
            {
                DoTopCurvedCornerPath(usingVisitor);
                return;
            }

            DoTopSquareCornerPath(usingVisitor);
        }

        /// <summary>
        /// Does the top square corner path.
        /// </summary>
        /// <param name="usingVisitor">Using visitor.</param>
        public void DoTopSquareCornerPath(IPathOpVisitor usingVisitor)
        {
            // left line
            usingVisitor.Visit(new LineTo(_halfThicknessLeft, _radiusTop));
        }

        /// <summary>
        /// Does the top curved corner path.
        /// </summary>
        /// <param name="usingVisitor">Using visitor.</param>
        public void DoTopCurvedCornerPath(IPathOpVisitor usingVisitor)
        {
            // left line
            usingVisitor.Visit(new LineTo(_halfThicknessLeft, _radiusTop + _thicknessLeft));

            // top left corner
            usingVisitor.Visit(new CurveTo(
                new NGPoint(_halfThicknessLeft, _radiusTop - _halfThicknessTop),
                new NGPoint(_halfThicknessLeft, _halfThicknessTop),
                new NGPoint(_radiusTop + _thicknessLeft, _halfThicknessTop)));
        }

        /// <summary>
        /// Does the left corner path.
        /// </summary>
        /// <param name="usingVisitor">Using visitor.</param>
        /// <param name="hasCurve">If set to <c>true</c> has curve.</param>
        /// <param name="applyMove">If set to <c>true</c> apply move.</param>
        public void DoLeftCornerPath(IPathOpVisitor usingVisitor, bool hasCurve, bool applyMove = true)
        {
            if (applyMove)
            {
                usingVisitor.Visit(new MoveTo(width - _radiusBottom, height - _halfThicknessBottom));
            }

            if (hasCurve)
            {
                DoLeftCurvedCornerPath(usingVisitor);
                return;
            }

            DoLeftSquareCornerPath(usingVisitor);
        }

        /// <summary>
        /// Does the left square corner path.
        /// </summary>
        /// <param name="usingVisitor">Using visitor.</param>
        public void DoLeftSquareCornerPath(IPathOpVisitor usingVisitor)
        {
            // bottom line
            usingVisitor.Visit(new LineTo(_thicknessLeft, height - _halfThicknessBottom));
        }

        /// <summary>
        /// Does the left curved corner path.
        /// </summary>
        /// <param name="usingVisitor">Using visitor.</param>
        public void DoLeftCurvedCornerPath(IPathOpVisitor usingVisitor)
        {
            // bottom line
            usingVisitor.Visit(new LineTo(_radiusLeft + _halfThicknessLeft, height - _halfThicknessBottom));

            // bottom left corner
            usingVisitor.Visit(new CurveTo(
                new NGPoint(_radiusLeft - _halfThicknessLeft, height - _halfThicknessBottom),
                new NGPoint(_halfThicknessLeft, height - _halfThicknessBottom),
                new NGPoint(_halfThicknessLeft, height - _radiusLeft)));
        }

        /// <summary>
        /// Does the bottom corner path.
        /// </summary>
        /// <param name="usingVisitor">Using visitor.</param>
        /// <param name="hasCurve">If set to <c>true</c> has curve.</param>
        /// <param name="applyMove">If set to <c>true</c> apply move.</param>
        public void DoBottomCornerPath(IPathOpVisitor usingVisitor, bool hasCurve, bool applyMove = true)
        {
            if (applyMove)
            {
                usingVisitor.Visit(new MoveTo(width - _halfThicknessRight, _radiusRight + _thicknessTop));
            }

            if (hasCurve)
            {
                DoBottomCurvedCornerPath(usingVisitor);
                return;
            }

            DoBottomSquareCornerPath(usingVisitor);
        }

        /// <summary>
        /// Does the bottom square corner path.
        /// </summary>
        /// <param name="usingVisitor">Using visitor.</param>
        public void DoBottomSquareCornerPath(IPathOpVisitor usingVisitor)
        {
            // right line
            usingVisitor.Visit(new LineTo(width - _halfThicknessRight, height));

        }

        /// <summary>
        /// Does the bottom curved corner path.
        /// </summary>
        /// <param name="usingVisitor">Using visitor.</param>
        public void DoBottomCurvedCornerPath(IPathOpVisitor usingVisitor)
        {
            // right line
            usingVisitor.Visit(new LineTo(width - _halfThicknessRight, height - _radiusBottom - _halfThicknessRight));

            // bottom right corner
            usingVisitor.Visit(new CurveTo(
                new NGPoint(width - _halfThicknessRight, height - _radiusBottom),
                new NGPoint(width - _halfThicknessRight, height - _halfThicknessBottom),
                new NGPoint(width - _radiusBottom, height - _halfThicknessBottom)));
        }

        /// <summary>
        /// Does the right corner path.
        /// </summary>
        /// <param name="usingVisitor">Using visitor.</param>
        /// <param name="hasCurve">If set to <c>true</c> has curve.</param>
        /// <param name="applyMove">If set to <c>true</c> apply move.</param>
        public void DoRightCornerPath(IPathOpVisitor usingVisitor, bool hasCurve, bool applyMove = true)
        {
            if (applyMove)
            {
                usingVisitor.Visit(new MoveTo(_radiusTop + _thicknessLeft, _halfThicknessTop));
            }

            if (hasCurve)
            {
                DoRightCurvedCornerPath(usingVisitor);
                return;
            }

            DoRightSquareCornerPath(usingVisitor);
        }

        /// <summary>
        /// Does the right square corner path.
        /// </summary>
        /// <param name="usingVisitor">Using visitor.</param>
        public void DoRightSquareCornerPath(IPathOpVisitor usingVisitor)
        {
            // top line
            usingVisitor.Visit(new LineTo(width - _radiusRight, _halfThicknessTop));
        }

        /// <summary>
        /// Does the right curved corner path.
        /// </summary>
        /// <param name="usingVisitor">Using visitor.</param>
        public void DoRightCurvedCornerPath(IPathOpVisitor usingVisitor)
        {
            // top line
            usingVisitor.Visit(new LineTo(width - (_radiusRight + _thicknessRight), _halfThicknessTop));

            // top right corner
            usingVisitor.Visit(new CurveTo(
                new NGPoint(width - (_radiusRight + _halfThicknessRight), _halfThicknessRight),
                new NGPoint(width - _halfThicknessRight, _halfThicknessRight),
                new NGPoint(width - _halfThicknessRight, _radiusRight + _thicknessRight)));
        }
    }
}
