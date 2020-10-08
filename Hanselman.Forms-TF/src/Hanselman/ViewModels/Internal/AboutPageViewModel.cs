using System.Composition;
using Hanselman.Services;
using Tiny.Framework.Scaffolding;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Hanselman.ViewModels.Internal
{
    /// <summary>
    /// About page view model.
    /// </summary>
    [Export(typeof(IAboutPageViewModel))]
    sealed class AboutPageViewModel :
        ObservableViewModel,
        IAboutPageViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:Hanselman.AboutPageViewModel"/> class.
        /// </summary>
        public AboutPageViewModel()
        {
            Title = "Scott Hanselman";
            Subtitle = "Hanselman.Forms";
            Icon = "about.png";
        }

        [Import]
        public ILaunchTwitter Launch { get; set; }

        [OnImportsSatisfied]
        public void Compose()
        {
            TwitterCommand = new Command(async () =>
            {
                if (!Launch.OpenAccountFor("shanselman"))
                {
                    await Browser.OpenAsync("http://m.twitter.com/shanselman");
                }
            });

            FacebookCommand = new Command(async () => await Browser.OpenAsync("https://m.facebook.com/shanselman"));
            InstagramCommand = new Command(async () => await Browser.OpenAsync("https://www.instagram.com/shanselman"));
        }

        public Command TwitterCommand { get; set; }

        public Command FacebookCommand { get; set; }

        public Command InstagramCommand { get; set; }

        public string Description { get; set; } = "My name is Scott Hanselman. I'm a programmer, " +
            "teacher, and speaker. I work out of my home office in Portland, Oregon for the Web Platform " +
            "Team at Microsoft, but this blog, its content and opinions are my own. I blog about technology, " +
            "culture, gadgets, diversity, code, the web, where we're going and where we've been. I'm excited " +
            "about community, social equity, media, entrepreneurship and above all, the open web.";
    }
}