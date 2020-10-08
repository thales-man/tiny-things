using System.Composition;
using System.Threading.Tasks;
using Tiny.Framework.Container;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Color = Xamarin.Forms.Color;

namespace NControl2
{
    /// <summary>
    /// Main page.
    /// </summary>
    [Shared]
    [Export]
    public partial class MainPage :
        ContentPage,
        IShell
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MainPage"/> class.
        /// </summary>
        public MainPage()
        {
            InitializeComponent();

            NavigationPage.SetHasNavigationBar(this, false);
            CreateContents();
        }

        /// <summary>
        /// curtains visible.
        /// </summary>
        private bool _curtainsVisible = true;

        /// <summary>
        /// chrome visible.
        /// </summary>
        private bool _chromeVisible;

        /// <summary>
        /// Creates the contents.
        /// </summary>
        protected void CreateContents()
        {
            RelativeLayout.SetBoundsConstraint(progress, BoundsConstraint.FromExpression(() => layout.Bounds));
            RelativeLayout.SetBoundsConstraint(navigationBar, BoundsConstraint.FromExpression(() => new Rectangle(0, -80, layout.Width, 80)));
            RelativeLayout.SetBoundsConstraint(topCurtain, BoundsConstraint.FromExpression(() => new Rectangle(0, 0, layout.Width, 1 + (layout.Height / 2))));
            RelativeLayout.SetBoundsConstraint(mapContainer, BoundsConstraint.FromExpression(() => layout.Bounds));
            RelativeLayout.SetBoundsConstraint(bottomCurtain, BoundsConstraint.FromExpression(() => new Rectangle(0, layout.Height / 2, layout.Width, layout.Height / 2)));
            RelativeLayout.SetBoundsConstraint(bottomBar, BoundsConstraint.FromExpression(() => new Rectangle(0, layout.Height, layout.Width, 72)));

            firstButton.Command = new Command(async () => await ToggleCurtains());
            secondButton.Command = new Command(() => firstButton.FillColor = Color.Red);
            thirdButton.Command = new Command(() => progress.IsBusy = true);
            fourthButton.Command = new Command(() => progress.IsBusy = false);
        }

        /// <summary>
        /// On appearing.
        /// </summary>
        protected override async void OnAppearing()
        {
            base.OnAppearing();

            // Start the progress control
            progress.Start();

            // Lets pretend we're doing something
            await Task.Delay(1500);

            // Introduce the navigation bar and toolbar
            await ToggleChromeAsync();

            // Hide the background and remove progressbar
            await ToggleCurtains();

            // Add map
            if (Device.RuntimePlatform == Device.iOS)
            {
                var _map = new Map
                {
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    VerticalOptions = LayoutOptions.FillAndExpand
                };

                _map.MoveToRegion(new MapSpan(new Position(0, 0), 360, 360));
                mapContainer.Content = _map;
            }
        }

        /// <summary>
        /// Toggles the curtains.
        /// </summary>
        /// <returns>The curtains.</returns>
        private Task ToggleCurtains()
        {
            if (_curtainsVisible)
            {
                return HideCurtains();
            }

            return ShowCurtains();
        }

        /// <summary>
        /// Shows the curtains.
        /// </summary>
        /// <returns>The curtains.</returns>
        private async Task ShowCurtains()
        {
            _curtainsVisible = true;

            await Task.WhenAll(new[] {
                topCurtain.TranslateTo(0, 0, 965, Easing.BounceOut),
                bottomCurtain.TranslateTo(0, 0, 965, Easing.BounceOut)
            });
        }

        /// <summary>
        /// Hides the curtains.
        /// </summary>
        /// <returns>The curtains.</returns>
        private async Task HideCurtains()
        {
            _curtainsVisible = false;

            await Task.WhenAll(new[] {
                topCurtain.TranslateTo(0, -Height/2, 465, Easing.CubicIn),
                bottomCurtain.TranslateTo(0, Height, 465, Easing.CubicIn)
            });
        }

        /// <summary>
        /// Toggles the chrome async.
        /// </summary>
        /// <returns>The chrome async.</returns>
        private Task ToggleChromeAsync()
        {
            if (_chromeVisible)
            {
                return HideChromeAsync();
            }

            return ShowChromeAsync();
        }

        /// <summary>
        /// Shows the chrome async.
        /// </summary>
        /// <returns>The chrome async.</returns>
        private async Task ShowChromeAsync()
        {
            _chromeVisible = true;

            await Task.WhenAll(new[]{
                bottomBar.TranslateTo(0, -bottomBar.Height, 550, Easing.BounceOut),
                navigationBar.TranslateTo(0, MyDevice.OnPlatform(75, 80, 55), 550, Easing.BounceOut),
            });
        }

        /// <summary>
        /// Hides the chrome async.
        /// </summary>
        /// <returns>The chrome async.</returns>
        private async Task HideChromeAsync()
        {
            _chromeVisible = false;

            await Task.WhenAll(new[]{
                bottomBar.TranslateTo(0, 0, 550, Easing.CubicIn),
                navigationBar.TranslateTo(0, 0, 550, Easing.CubicIn),
            });
        }
    }

    /// <summary>
    /// My device (a platform crutch)
    /// </summary>
    public static class MyDevice
    {
        public static TResult OnPlatform<TResult>(TResult iOS, TResult android, TResult uwp)
        {
            switch (Device.RuntimePlatform)
            {
                case Device.iOS: return iOS;
                case Device.Android: return android;
                case Device.UWP: return uwp;
                default: return default;
            }
        }
    }
}
