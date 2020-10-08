// -----------------------------------------------------------------------------
// copyright (c) 2019, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
// -----------------------------------------------------------------------------
using AppKit;
using CoreGraphics;
using Foundation;
using MediaManager.Platforms.Mac.Video;
using Tiny.Framework.Controls;
using Tiny.Framework.Utility;
using Xamarin.Forms;
using Xamarin.Forms.Platform.MacOS;

namespace MessageRelay.macOS
{
    /// <summary>
    /// App delegate.
    /// </summary>
    [Register("AppDelegate")]     public class AppDelegate : FormsApplicationDelegate     {
        /// <summary>
        /// The main window.
        /// </summary>
        private readonly NSWindow _mainWindow; 
        /// <summary>
        /// Initializes a new instance of the <see cref="AppDelegate"/> class.
        /// </summary>         public AppDelegate()         {
            var style = NSWindowStyle.Resizable | NSWindowStyle.Titled;
            var rect = new CGRect(500, 500, 800, 300); 
            _mainWindow = new NSWindow(rect, style, NSBackingStore.Buffered, false)
            {
                Title = "Message Relay",
                TitleVisibility = NSWindowTitleVisibility.Hidden
            };
        } 
        /// <summary>
        /// Gets the main window.
        /// </summary>
        /// <value>The main window.</value>         public override NSWindow MainWindow         {             get { return _mainWindow; }         } 
        /// <summary>
        /// Did finish launching.
        /// </summary>
        /// <param name="notification">Notification.</param>         public override void DidFinishLaunching(NSNotification notification)         {
            SmartLinking.IncludeLibraries(
                typeof(VideoView),
                typeof(GeometryBrush),                 typeof(CustomControlViewRenderer));              Forms.Init();             LoadApplication(new App());             base.DidFinishLaunching(notification);
        } 
        /// <summary>
        /// Will terminate.
        /// </summary>
        /// <param name="notification">Notification.</param>         public override void WillTerminate(NSNotification notification)         {             // Insert code here to tear down your application         }     } }