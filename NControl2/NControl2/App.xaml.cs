using System;
using System.IO;
using Tiny.Framework.Container;
using Xamarin.Forms;

namespace NControl2
{
    public partial class App : Application
    {
        public App(Func<string, Stream> openResource = null)
        {
            InitializeComponent();
            BootstrapUI.Start<MainPage>(SetMainPage, openResource);
        }

        public void SetMainPage(MainPage thisPage) =>
            MainPage = thisPage;


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
