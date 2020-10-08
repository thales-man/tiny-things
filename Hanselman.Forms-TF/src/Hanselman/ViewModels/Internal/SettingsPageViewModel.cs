using System.Composition;
using Tiny.Framework.Scaffolding;
using Xamarin.Forms;

namespace Hanselman.ViewModels.Internal
{
    public enum Theme     {         Dark,         Light     } 
    /// <summary>
    /// About page view model.
    /// </summary>
    [Export(typeof(ISettingsPageViewModel))]
    sealed class SettingsPageViewModel :
        ObservableViewModel,
        ISettingsPageViewModel
    {
        private Theme _currentTheme = Theme.Light;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Hanselman.AboutPageViewModel"/> class.
        /// </summary>
        public SettingsPageViewModel()
        {
            Title = "Settings...";
            Subtitle = "this is the settings form";
            Icon = "about.png";
        }

        public void Toggle()
        {
            var origin = Application.Current.Resources;

            switch (_currentTheme)
            {
                case Theme.Dark:
                    //    origin.Add(new LightThemeResources());
                    //    __currentTheme = Theme.Light;
                    break;
                case Theme.Light:
                    //origin.Add(new DarkThemeResources());
                    //__currentTheme = Theme.Dark;
                    break;
            } 
        }
    }
}