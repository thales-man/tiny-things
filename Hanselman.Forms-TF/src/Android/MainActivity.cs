using Android.App;
using Android.Content.PM;
using Android.Content.Res;
using Android.OS;
using Android.Runtime;
using MediaManager.Forms;
using Tiny.Framework.Controls;
using Tiny.Framework.Utility;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

namespace Hanselman.Droid
{
    [Activity(Label = "Hanselman",
        MainLauncher = true,
        ScreenOrientation = ScreenOrientation.Portrait,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            ToolbarResource = Resource.Layout.Toolbar;
            TabLayoutResource = Resource.Layout.Tabbar;
            base.OnCreate(savedInstanceState);

            // pick one class from each library that requires linking
            // saves having lists of endless (and useless) 'init' routines...
            SmartLinking.IncludeLibraries(
                typeof(GeometryBrush),
                typeof(VideoView),
                typeof(CustomControlViewRenderer));

            Forms.Init(this, savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            LoadApplication(new App(Resources.Assets.Open));
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}
