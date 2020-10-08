using MessageRelay.Views;
using Tiny.Framework.Container;
using Xamarin.Forms;

namespace MessageRelay
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            BootstrapUI.Start<MainPage>(SetMainPage);
            BootstrapServer.Start(null);
        }

        public void SetMainPage(MainPage thisPage)
        {
            MainPage = thisPage;
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
