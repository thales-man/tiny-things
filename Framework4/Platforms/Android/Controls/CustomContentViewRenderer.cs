// -----------------------------------------------------------------------------
// copyright (c) 2019, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
// -----------------------------------------------------------------------------
using Android.Content;
using Android.Runtime;
using Tiny.Framework.Controls;
using Tiny.Framework.Helpers;
using Tiny.Framework.Services.Internal;
using Tiny.Framework.Views;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomContentView), typeof(CustomContentViewRenderer))]
namespace Tiny.Framework.Controls
{
    /// <summary>
    /// Custom content view renderer.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class CustomContentViewRenderer :
        VisualElementRenderer<CustomContentView>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:Tiny.Framework.Controls.CustomContentViewRenderer"/> class.
        /// </summary>
        /// <param name="context">Context.</param>
        public CustomContentViewRenderer(Context context) :
            base(context)
        {
        }

        /// <summary>
        /// Ons the element changed.
        /// </summary>
        /// <param name="e">E.</param>
        protected override void OnElementChanged(ElementChangedEventArgs<CustomContentView> e)
        {
            base.OnElementChanged(e);

            // apparently, the only way we can get it to draw :(
            SetBackgroundColor(Element.BackgroundColor.ToAndroid());
        }

        /// <summary>
        /// Sets up the mask.
        /// </summary>
        private void SetupMask(Android.Graphics.Canvas canvas, Android.Graphics.Rect rect)
        {
            var path = new PlatformPathOperationsAdapter();
            Element.Parent.GetContentMask(path, rect.GetRect());
            canvas.ClipPath(path.GetPathResult());
        }

        /// <summary>
        /// Draw the specified canvas.
        /// </summary>
        /// <param name="canvas">Canvas.</param>
        public override void Draw(Android.Graphics.Canvas canvas)
        {
            if (Element == null)
            {
                base.Draw(canvas);
                return;
            }

            var clipMask = new Android.Graphics.Rect(0, 0, Width, Height);
            if (Element.IsClippedToBounds)
            {
                canvas.ClipRect(clipMask);
            }

            if (Element.Parent.RequiresMask)
            {
                SetupMask(canvas, clipMask);
            }

            base.Draw(canvas);
        }
    }
}

