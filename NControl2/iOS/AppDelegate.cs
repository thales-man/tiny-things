using Foundation;
using NControl2;
using Tiny.Framework.Controls;
using Tiny.Framework.Utility;
using UIKit;
using Xamarin;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

namespace Shell.Test.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the
    // User Interface of the application, as well as listening (and optionally responding) to application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        public override bool FinishedLaunching(UIApplication uiApplication, NSDictionary launchOptions)
        {
            Forms.Init();
            FormsMaps.Init();

            // pick one class from each library that requires linking
            // saves having lists of endless (and useless) 'init' routines...
            SmartLinking.IncludeLibraries(
                typeof(GeometryBrush),              // <= Tiny.Framework.Presentation
                typeof(CustomControlViewRenderer)); // <= Tiny.Framework.iOS

            LoadApplication(new App());

            return base.FinishedLaunching(uiApplication, launchOptions);
        }
    }
}

