using Xamarin.Forms;
using Color = Xamarin.Forms.Color;
using Point = Xamarin.Forms.Point;

namespace Tiny.Framework.Controls
{
    /// <summary>
    /// Busy box.
    /// i'm a bit problematic and need looking at...
    /// </summary>
    public class BusyBox :
        ContentView
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BusyBox"/> class.
        /// </summary>
        public BusyBox()
        {
            OperatingOpacity = 0.6;
            HorizontalOptions = LayoutOptions.CenterAndExpand;
            VerticalOptions = LayoutOptions.CenterAndExpand;
            HeightRequest = 85;
            WidthRequest = 85;
        }

        /// <summary>
        /// Creates the contents.
        /// </summary>
        public void CreateContents()
        {
            var largeCog = new VectorImage
            {
                GeometryBrush = CogBrush,
                HeightRequest = 38,
                Fill = Color.WhiteSmoke
            };

            var smallCog = new VectorImage
            {
                GeometryBrush = CogBrush,
                HeightRequest = 19,
                Fill = Color.WhiteSmoke
            };

            var _animation = new Animation(
                (val) => { largeCog.Rotation = val; smallCog.Rotation = -val; },
                0,
                360,
                Easing.Linear);

            _animation.Commit(this, "Rotation", length: 2000, repeat: () => true);

            var layout = new AbsoluteLayout
            {
                Margin = 0,
                Padding = 0,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand,
            };

            layout.Children.Add(smallCog, new Point(25, 0));
            layout.Children.Add(largeCog, new Point(0, 8));

            Content = new Border
            {
                BackgroundColor = Color.DarkSlateGray,
                BorderColor = Color.HotPink,
                BorderThickness = new Thickness(3),
                CornerRadius = new Thickness(10, 0, 10, 10),
                Padding = 10,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Content = layout
            };
        }

        /// <summary>
        /// The cog brush property.
        /// </summary>
        public static readonly BindableProperty CogBrushProperty =
            BindableProperty.Create(nameof(CogBrush), typeof(GeometryBrush), typeof(BusyBox));

        /// <summary>
        /// The is busy property.
        /// </summary>
        public static readonly BindableProperty IsBusyProperty =
            BindableProperty.Create(nameof(IsBusy), typeof(bool), typeof(BusyBox), false, BindingMode.TwoWay);

        /// <summary>
        /// The operating opacity property.
        /// </summary>
        public static readonly BindableProperty OperatingOpacityProperty =
            BindableProperty.Create(nameof(OperatingOpacity), typeof(double), typeof(BusyBox), 1.0, BindingMode.TwoWay,
                propertyChanged: (bindable, oldValue, newValue) =>
                {
                    var ctrl = (BusyBox)bindable;
                    ctrl.Opacity = (double)newValue;
                });

        /// <summary>
        /// Gets or sets the cog brush.
        /// </summary>
        public GeometryBrush CogBrush
        {
            get => (GeometryBrush)GetValue(CogBrushProperty);
            set => SetValue(CogBrushProperty, value);
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="BusyBox"/> is busy.
        /// </summary>
        public bool IsBusy
        {
            get => (bool)GetValue(IsBusyProperty);
            set
            {
                SetValue(IsBusyProperty, value);
                if (value) { Start(); }
                else { Stop(); }
            }
        }

        /// <summary>
        /// Gets or sets the operating opacity.
        /// </summary>
        public double OperatingOpacity
        {
            get => (double)GetValue(OperatingOpacityProperty);
            set => SetValue(OperatingOpacityProperty, value);
        }

        /// <summary>
        /// Start this instance.
        /// </summary>
        public void Start()
        {
            if (Content == null)
            {
                CreateContents();
            }

            var _opacity = OperatingOpacity;
            Device.BeginInvokeOnMainThread(() => this.FadeTo(_opacity, 365, Easing.CubicOut));
        }

        /// <summary>
        /// Stop this instance.
        /// </summary>
        public void Stop()
        {
            Device.BeginInvokeOnMainThread(() => this.FadeTo(0, 365, Easing.CubicInOut));
        }
    }
}