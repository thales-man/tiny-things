using AppKit;
using Foundation;
using MediaManager;
using MediaManager.Forms.Platforms.Mac;
using Tiny.Framework.Controls;
using Tiny.Framework.Utility;
using Xamarin.Forms;
using Xamarin.Forms.Platform.MacOS;

namespace Hanselman.macOS
{
    [Register("AppDelegate")]
    public class AppDelegate : FormsApplicationDelegate
    {
        private readonly NSWindow _window;

        public AppDelegate()
        {
            var style = NSWindowStyle.Resizable | NSWindowStyle.Titled;
            var rect = new CoreGraphics.CGRect(500, 500, 1000, 800);

            _window = new NSWindow(rect, style, NSBackingStore.Buffered, false)
            {
                Title = "You won't see me!",
                TitleVisibility = NSWindowTitleVisibility.Hidden
            };
        }

        public override NSWindow MainWindow
        {
            get { return _window; }
        }

        public override void DidFinishLaunching(NSNotification notification)
        {
            SmartLinking.IncludeLibraries(
                typeof(GeometryBrush),
                typeof(VideoViewRenderer),
                typeof(CustomControlViewRenderer));

            //NSApplication.SharedApplication.MainMenu = new NSMenu(); // TODO: mmm....
            Forms.Init();
            CrossMediaManager.Current.Init();
            LoadApplication(new App());

            base.DidFinishLaunching(notification);
        }

        public override void WillTerminate(NSNotification notification)
        {
            // Insert code here to tear down your application
        }
    }
}