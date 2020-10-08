using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using NGraphics;
using Tiny.Framework.Helpers;
using Tiny.Framework.Services;
using Tiny.Framework.Views;
using Xamarin.Forms;
using XFColor = Xamarin.Forms.Color;
using NGPoint = NGraphics.Point;

namespace Tiny.Framework.Controls
{
    /// <summary>
    /// Round button.
    /// </summary>
    public class RoundButton : CustomControlView
    {
        private const int RoundingOffset = 2;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Tiny.Framework.Controls.RoundButton"/> class.
        /// </summary>
        public RoundButton()
        {
            VerticalOptions = LayoutOptions.Center;
            HorizontalOptions = LayoutOptions.Center;
            SetRequiresMask(true);
        }

        /// <summary>
        /// The command property.
        /// </summary>
        public static readonly BindableProperty CommandProperty =
            BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(RoundButton), null, BindingMode.TwoWay);

        /// <summary>
        /// The command parameter property.
        /// </summary>
        public static readonly BindableProperty CommandParameterProperty =
            BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(RoundButton), null, BindingMode.TwoWay);

        /// <summary>
        /// The border color property.
        /// </summary>
        public static readonly BindableProperty BorderColorProperty =
            BindableProperty.Create(nameof(BorderColor), typeof(XFColor), typeof(RoundButton), XFColor.DarkGray, BindingMode.TwoWay);

        /// <summary>
        /// The border width property.
        /// </summary>
        public static readonly BindableProperty BorderWidthProperty =
            BindableProperty.Create(nameof(BorderWidth), typeof(float), typeof(RoundButton), 0f, BindingMode.TwoWay);

        /// <summary>
        /// The diameter request property.
        /// </summary>
        public static readonly BindableProperty DiameterRequestProperty =
            BindableProperty.Create(nameof(DiameterRequest), typeof(float), typeof(RoundButton), 0f, BindingMode.TwoWay);

        /// <summary>
        /// The fill color property.
        /// </summary>
        public static readonly BindableProperty FillColorProperty =
            BindableProperty.Create(nameof(FillColor), typeof(XFColor), typeof(RoundButton), XFColor.LightGray, BindingMode.TwoWay);

        /// <summary>
        /// Gets or sets the command.
        /// </summary>
        /// <value>The command.</value>
        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        /// <summary>
        /// Gets or sets the command parameter.
        /// </summary>
        /// <value>The command parameter.</value>
        public object CommandParameter
        {
            get => GetValue(CommandParameterProperty);
            set => SetValue(CommandParameterProperty, value);
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
        /// Gets or sets the width of the border.
        /// </summary>
        /// <value>The width of the border.</value>
        public float BorderWidth
        {
            get => (float)GetValue(BorderWidthProperty);
            set => SetValue(BorderWidthProperty, value);
        }

        /// <summary>
        /// Gets or sets the diameter request.
        /// </summary>
        /// <value>The diameter request.</value>
        public float DiameterRequest
        {
            get => (float)GetValue(DiameterRequestProperty);
            set => SetValue(DiameterRequestProperty, value);
        }

        /// <summary>
        /// Gets or sets the color of the fill.
        /// </summary>
        /// <value>The color of the fill.</value>
        public XFColor FillColor
        {
            get => (XFColor)GetValue(FillColorProperty);
            set => SetValue(FillColorProperty, value);
        }

        /// <summary>
        /// Touches began.
        /// </summary>
        /// <returns><c>true</c>, if touches began, <c>false</c> otherwise.</returns>
        /// <param name="points">Points.</param>
        public override bool TouchesBegan(IEnumerable<NGPoint> points)
        {
            base.TouchesBegan(points);
            this.ScaleTo(0.8, 65, Easing.CubicInOut);

            return true;
        }

        /// <summary>
        /// Touches cancelled.
        /// </summary>
        /// <returns><c>true</c>, if touches cancelled, <c>false</c> otherwise.</returns>
        /// <param name="points">Points.</param>
        public override bool TouchesCancelled(IEnumerable<NGPoint> points)
        {
            base.TouchesCancelled(points);
            this.ScaleTo(1.0, 65, Easing.CubicInOut);

            return true;
        }

        /// <summary>
        /// Touches ended.
        /// </summary>
        /// <returns><c>true</c>, if touches ended, <c>false</c> otherwise.</returns>
        /// <param name="points">Points.</param>
        public override bool TouchesEnded(IEnumerable<NGPoint> points)
        {
            base.TouchesEnded(points);
            this.ScaleTo(1.0, 65, Easing.CubicInOut);

            if (Command != null && Command.CanExecute(CommandParameter))
            {
                Command.Execute(CommandParameter);
            }

            return true;
        }

        /// <summary>
        /// Gets the control mask.
        /// </summary>
        /// <param name="usingVisitor">Using visitor.</param>
        /// <param name="withinBounds">Within bounds.</param>
        public override void GetControlMask(IVisitPathGenerators usingVisitor, Rect withinBounds)
        {
            var clone = new Rect(withinBounds.Position, withinBounds.Size);
            clone.Inflate(-RoundingOffset, -RoundingOffset);
            usingVisitor.DrawEllipse(clone);
            usingVisitor.Visit(new ClosePath());
        }

        /// <summary>
        /// Gets the content offset.
        /// </summary>
        /// <value>The content offset.</value>
        public double ContentOffset => BorderWidth + RoundingOffset;

        /// <summary>
        /// Gets the border drawn offset.
        /// </summary>
        /// <value>The border drawn offset.</value>
        public double BorderDrawnOffset => (BorderWidth / 2) + RoundingOffset;

        /// <summary>
        /// Gets the content mask.
        /// </summary>
        /// <param name="usingVisitor">Using visitor.</param>
        /// <param name="withinBounds">Within bounds.</param>
        public override void GetContentMask(IVisitPathGenerators usingVisitor, Rect withinBounds)
        {
            usingVisitor.SetScale(withinBounds, withinBounds);

            var clone = new Rect(withinBounds.Position, withinBounds.Size);
            clone.Inflate(-ContentOffset, -ContentOffset);

            usingVisitor.DrawEllipse(clone);
            usingVisitor.Visit(new ClosePath());
        }

        /// <summary>
        /// Draw on Canvas and within Bounds.
        /// </summary>
        /// <param name="onCanvas">On canvas.</param>
        /// <param name="withinBounds">Within bounds.</param>
        public override void Draw(ICanvas onCanvas, Rect withinBounds)
        {
            var borderColor = BorderColor.AsNGColor();
            var fillColor = FillColor.AsNGColor();

            var clone = new Rect(withinBounds.Position, withinBounds.Size);
            if (BorderWidth > 0)
            {
                clone.Inflate(-BorderDrawnOffset, -BorderDrawnOffset);
            }

            onCanvas.FillEllipse(withinBounds, fillColor);
            onCanvas.DrawEllipse(clone, borderColor, BorderWidth);
        }

        /// <summary>
        /// On property changed.
        /// </summary>
        /// <param name="propertyName">Property name.</param>
        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == DiameterRequestProperty.PropertyName)
            {
                HeightRequest = DiameterRequest;
                WidthRequest = DiameterRequest;
            }
        }
    }
}
