using Xamarin.Forms;

namespace Tiny.Framework.Controls
{
    /// <summary>
    /// Geometry drawing.
    /// </summary>
    public class GeometryDrawing : 
        BindableObject
    {
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
            BindableProperty.Create(nameof(StrokeWidth), typeof(int), typeof(GeometryDrawing), default(int));

        /// <summary>
        /// The geometry property.
        /// </summary>
        public static readonly BindableProperty GeometryProperty =
            BindableProperty.Create(nameof(Geometry), typeof(string), typeof(GeometryDrawing));

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
        /// Gets or sets the geometry.
        /// </summary>
        /// <value>The geometry.</value>
        public string Geometry
        {
            get { return (string)GetValue(GeometryProperty); }
            set { SetValue(GeometryProperty, value); }
        }
    }
}
