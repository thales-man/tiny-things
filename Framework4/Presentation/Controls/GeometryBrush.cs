using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace Tiny.Framework.Controls
{
    /*
    <GeometryBrush
        x:Key="MySampleBrush"
        Viewport="0,0,512,512">
        <GeometryBrush.DrawingGroup>
            <GeometryDrawing
                Brush="Green"
                Geometry="M256 8C119.033 8 8 119.033 8 256s111.033 248 248 248 248-111.033 248-248S392.967 8 256 8Z" />
            <GeometryDrawing
                Stroke="Yellow"
                StrokeWidth="10"
                Fill="Blue"
                Geometry="M256 8C119.033 8 8 119.033 8 256s111.033 248 248 248 248-111.033 248-248S392.967 8 256 8zm0 48c110.532 0 200 89.451 200 200 0 110.532-89.451 200-200 200-110.532 0-200-89.451-200-200 0-110.532 89.451-200 200-200m140.204 130.267l-22.536-22.718c-4.667-4.705-12.265-4.736-16.97-.068L215.346 303.697l-59.792-60.277c-4.667-4.705-12.265-4.736-16.97-.069l-22.719 22.536c-4.705 4.667-4.736 12.265-.068 16.971l90.781 91.516c4.667 4.705 12.265 4.736 16.97.068l172.589-171.204c4.704-4.668 4.734-12.266.067-16.971z" />
        </GeometryBrush.DrawingGroup>
    </GeometryBrush>
    */

    /// <summary>
    /// Geometry brush.
    /// </summary>
    public class GeometryBrush : 
        BindableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GeometryBrush"/> class.
        /// </summary>
        public GeometryBrush()
        {
            DrawingGroup = new ObservableCollection<GeometryDrawing>();
        }

        /// <summary>
        /// The viewport property.
        /// </summary>
        public static readonly BindableProperty ViewportProperty =
            BindableProperty.Create(nameof(Viewport), typeof(Rectangle), typeof(GeometryBrush), new Rectangle(0, 0, 50, 50));

        /// <summary>
        /// The rotation property.
        /// </summary>
        public static readonly BindableProperty RotationProperty =
            BindableProperty.Create("Rotation", typeof(double), typeof(GeometryBrush), 0.0);

        /// <summary>
        /// The drawing group property.
        /// </summary>
        public static readonly BindableProperty DrawingGroupProperty =
            BindableProperty.Create(nameof(DrawingGroup), typeof(ObservableCollection<GeometryDrawing>), typeof(GeometryBrush));

        /// <summary>
        /// Gets or sets the viewport.
        /// </summary>
        /// <value>The viewport.</value>
        public Rectangle Viewport
        {
            get { return (Rectangle)GetValue(ViewportProperty); }
            set { SetValue(ViewportProperty, value); }
        }

        /// <summary>
        /// Gets or sets the rotation.
        /// </summary>
        /// <value>The rotation.</value>
        public double Rotation
        {
            get { return (double)GetValue(RotationProperty); }
            set { SetValue(RotationProperty, value); }
        }

        /// <summary>
        /// Gets or sets the geometry group.
        /// </summary>
        /// <value>The geometry group.</value>
        public ObservableCollection<GeometryDrawing> DrawingGroup
        {
            get { return (ObservableCollection<GeometryDrawing>)GetValue(DrawingGroupProperty); }
            set { SetValue(DrawingGroupProperty, value); }
        }
    }
}
