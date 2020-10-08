// -----------------------------------------------------------------------------
// copyright (c) 2019, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
// -----------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using Android.Content;
using Android.Graphics;
using Android.Runtime;
using Android.Views;
using NGraphics;
using Tiny.Framework.Controls;
using Tiny.Framework.Helpers;
using Tiny.Framework.Views;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomControlView), typeof(CustomControlViewRenderer))]
namespace Tiny.Framework.Controls
{
    /// <summary>
    /// Custom control view renderer.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class CustomControlViewRenderer :
        VisualElementRenderer<CustomControlView>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:Tiny.Framework.Controls.CustomControlViewRenderer"/> class.
        /// </summary>
        /// <param name="context">Context.</param>
        public CustomControlViewRenderer(Context context) :
            base(context)
        {
        }

        /// <summary>
        /// Raises the element changed event.
        /// </summary>
        /// <param name="e">E.</param>
        protected override void OnElementChanged(ElementChangedEventArgs<CustomControlView> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null)
            {
                e.OldElement.OnInvalidate -= HandleInvalidate;
            }

            if (e.NewElement != null)
            {
                e.NewElement.OnInvalidate += HandleInvalidate;
            }

            // apparently, the only way we can get it to draw :(
            SetBackgroundColor(Element.BackgroundColor.ToAndroid());
        }

        /// <summary>
        /// On element property changed.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">E.</param>
        // protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        // [
        //    base.OnElementPropertyChanged(sender, e)

        //    if e.PropertyName == VisualElement.BackgroundColorProperty.PropertyName)
        //        Element.Invalidate()
        //    else if e.PropertyName == VisualElement.IsVisibleProperty.PropertyName)
        //        Element.Invalidate()

        //    Invalidate()
        // ]

        /// <summary>
        /// Dispose.
        /// </summary>
        /// <param name="disposing">If set to <c>true</c> disposing.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && Element != null)
            {
                Element.OnInvalidate -= HandleInvalidate;
            }

            base.Dispose(disposing);
        }

        /// <summary>
        /// Draw the specified canvas.
        /// </summary>
        /// <param name="canvas">Canvas.</param>
        public override void Draw(Canvas canvas)
        {
            if (Element == null)
            {
                base.Draw(canvas);
                return;
            }

            if (Element.IsClippedToBounds)
            {
                canvas.ClipRect(new Android.Graphics.Rect(0, 0, Width, Height));
            }

            // Perform custom drawing from the NGraphics subsystems
            var ncanvas = new CanvasCanvas(canvas);

            var rect = new NGraphics.Rect(0, 0, Width, Height);

            // Fill background 
            ncanvas.FillRectangle(rect, Element.BackgroundColor.AsNGColor());

            // Custom drawing
            Element.Draw(ncanvas, rect);

            base.Draw(canvas);
        }

        /// <Docs>The motion event.</Docs>
        /// <returns>To be added.</returns>
        /// <para tool="javadoc-to-mdoc">Implement this method to handle touch screen motion events.</para>
        /// <format type="text/html">[Android Documentation]</format>
        /// <since version="Added in API level 1"></since>
        /// <summary>
        /// Raises the touch event event.
        /// </summary>
        /// <param name="e">E.</param>
        public override bool OnTouchEvent(MotionEvent e)
        {
            var scale = Element.Width / Width;

            var touchInfo = new List<NGraphics.Point>();
            for (var i = 0; i < e.PointerCount; i++)
            {
                var coord = new MotionEvent.PointerCoords();
                e.GetPointerCoords(i, coord);
                touchInfo.Add(new NGraphics.Point(coord.X * scale, coord.Y * scale));
            }

            var result = false;

            // Handle touch actions
            switch (e.Action)
            {
                case MotionEventActions.Down:
                    if (Element != null)
                        result = Element.TouchesBegan(touchInfo);
                    break;

                case MotionEventActions.Move:
                    if (Element != null)
                        result = Element.TouchesMoved(touchInfo);
                    break;

                case MotionEventActions.Up:
                    if (Element != null)
                        result = Element.TouchesEnded(touchInfo);
                    break;

                case MotionEventActions.Cancel:
                    if (Element != null)
                        result = Element.TouchesCancelled(touchInfo);
                    break;
            }

            return result;
        }

        /// <summary>
        /// Handles the invalidate.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="args">Arguments.</param>
        private void HandleInvalidate(object sender, EventArgs args)
        {
            Invalidate();
        }
    }
}

