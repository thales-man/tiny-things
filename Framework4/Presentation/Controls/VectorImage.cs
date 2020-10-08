using System.Linq;
using NGraphics;
using Tiny.Framework.Helpers;
using Tiny.Framework.Services;
using Tiny.Framework.Services.Internal;
using Tiny.Framework.Views;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Color = Xamarin.Forms.Color;

namespace Tiny.Framework.Controls
{
    /// <summary>
    /// Vector image.
    /// </summary>
    public class VectorImage : CustomControlView
    {
        /// <summary>
        /// The geometry brush property.
        /// </summary>
        public static readonly BindableProperty GeometryBrushProperty =
            BindableProperty.Create(nameof(GeometryBrush), typeof(GeometryBrush), typeof(VectorImage), default(GeometryBrush));

        /// <summary>
        /// The fill property.
        /// </summary>
        public static readonly BindableProperty FillProperty =
            BindableProperty.Create(nameof(Fill), typeof(Color), typeof(GeometryDrawing), Color.Default);

        /// <summary>
        /// The stroke property.
        /// </summary>
        public static readonly BindableProperty StrokeProperty =
            BindableProperty.Create(nameof(Stroke), typeof(Color), typeof(GeometryDrawing), Color.Default);

        /// <summary>
        /// The stroke width property.
        /// </summary>
        public static readonly BindableProperty StrokeWidthProperty =
            BindableProperty.Create(nameof(StrokeWidth), typeof(int), typeof(GeometryDrawing), -1);

        /// <summary>
        /// Initializes a new instance of the <see cref="VectorImage"/> class.
        /// </summary>
        public VectorImage()
        {
            Padding = 0;
            Margin = 0;
            HeightRequest = 50;
            WidthRequest = 50;
            HorizontalOptions = LayoutOptions.CenterAndExpand;
            VerticalOptions = LayoutOptions.CenterAndExpand;
        }

        /// <summary>
        /// Gets or sets the geometry brush.
        /// </summary>
        /// <value>The geometry brush.</value>
        public GeometryBrush GeometryBrush
        {
            get { return (GeometryBrush)GetValue(GeometryBrushProperty); }
            set { SetValue(GeometryBrushProperty, value); }
        }

        /// <summary>
        /// Gets or sets the fill.
        /// </summary>
        /// <value>The fill.</value>
        public Color Fill
        {
            get { return (Color)GetValue(FillProperty); }
            set { SetValue(FillProperty, value); }
        }

        /// <summary>
        /// Gets or sets the stroke.
        /// </summary>
        /// <value>The stroke.</value>
        public Color Stroke
        {
            get { return (Color)GetValue(StrokeProperty); }
            set { SetValue(StrokeProperty, value); }
        }

        /// <summary>
        /// Gets or sets the width of the stroke.
        /// </summary>
        /// <value>The width of the stroke.</value>
        public int StrokeWidth
        {
            get { return (int)GetValue(StrokeWidthProperty); }
            set { SetValue(StrokeWidthProperty, value); }
        }

        /// <summary>
        /// Gets the color of the fill.
        /// </summary>
        /// <returns>The fill color.</returns>
        /// <param name="candidate">Candidate.</param>
        public NGraphics.Color GetFillColor(GeometryDrawing candidate) =>
            Fill == Color.Default ? candidate.Fill.AsNGColor() : Fill.AsNGColor();

        /// <summary>
        /// Gets the color of the stroke.
        /// </summary>
        /// <returns>The stroke color.</returns>
        /// <param name="candidate">Candidate.</param>
        public NGraphics.Color GetStrokeColor(GeometryDrawing candidate) =>
            Stroke == Color.Default ? candidate.Stroke.AsNGColor() : Stroke.AsNGColor();

        /// <summary>
        /// Gets the width of the stroke.
        /// </summary>
        /// <returns>The stroke width.</returns>
        /// <param name="candidate">Candidate.</param>
        public int GetStrokeWidth(GeometryDrawing candidate) =>
            StrokeWidth == -1 ? candidate.StrokeWidth : StrokeWidth;

        /// <summary>
        /// Draw on the specified canvas within bounds.
        /// </summary>
        /// <param name="onCanvas">On canvas.</param>
        /// <param name="withinBounds">Within bounds.</param>
        public override void Draw(ICanvas onCanvas, Rect withinBounds)
        {
            var viewport = GeometryBrush.Viewport;
            var ratio = viewport.GetScale(withinBounds);

            onCanvas.Translate(withinBounds.GetMidPoint());
            onCanvas.Scale(ratio);
            onCanvas.Translate(viewport.GetNegativeMidPoint());

            // TODO: sort out rotation, how to shift the origin
            // onCanvas.Rotate(GeometryBrush.Rotation)

            GeometryBrush.DrawingGroup
                .ForEach(x =>
                {
                    var pen = new Pen(GetStrokeColor(x), GetStrokeWidth(x));
                    var brush = new SolidBrush(GetFillColor(x));
                    var visitor = new ControlPathOperationsAdapter();

                    ParseGeometry.ForPathOperations(x.Geometry, visitor);

                    onCanvas.DrawPath(visitor.GetPathResult(), pen, brush);
                });
        }

        /// <summary>
        /// Gets the control mask.
        /// </summary>
        /// <param name="usingVisitor">Using visitor.</param>
        /// <param name="withinBounds">Within bounds.</param>
        public override void GetControlMask(IVisitPathGenerators usingVisitor, Rect withinBounds)
        {
            var mask = GeometryBrush.DrawingGroup.FirstOrDefault();
            ParseGeometry.ForPathOperations(mask.Geometry, usingVisitor);
            usingVisitor.SetScale(GeometryBrush.Viewport, withinBounds);
        }

        /// <summary>
        /// Gets the content mask.
        /// </summary>
        /// <param name="usingVisitor">Using visitor.</param>
        /// <param name="withinBounds">Within bounds.</param>
        public override void GetContentMask(IVisitPathGenerators usingVisitor, Rect withinBounds)
        {
            var mask = GeometryBrush.DrawingGroup.ElementAtOrDefault(1)
                ?? GeometryBrush.DrawingGroup.FirstOrDefault();
            ParseGeometry.ForPathOperations(mask.Geometry, usingVisitor);
            usingVisitor.SetScale(GeometryBrush.Viewport, withinBounds);
        }
    }
}
