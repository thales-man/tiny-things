//-----------------------------------------------------------------------------
// copyright (c) 2019, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
//-----------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.ComponentModel;
using CoreAnimation;
using CoreGraphics;
using Foundation;
using NGraphics;
using Tiny.Framework.Controls;
using Tiny.Framework.Services.Internal;
using Tiny.Framework.Views;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

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
        /// The gesture recognizer.
        /// </summary>
        private UITouchesGestureRecognizer _gestureRecognizer;

        /// <summary>
        /// Raises the element changed event.
        /// </summary>
        /// <param name="e">E.</param>
        protected override void OnElementChanged(ElementChangedEventArgs<CustomControlView> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null)
            {
                if (_gestureRecognizer != null)
                {
                    RemoveGestureRecognizer(_gestureRecognizer);
                    _gestureRecognizer = null;
                }

                e.OldElement.OnInvalidate -= HandleInvalidate;
            }

            if (e.NewElement != null)
            {
                e.NewElement.OnInvalidate += HandleInvalidate;

                if ((_gestureRecognizer == null) && (NativeView != null))
                {
                    _gestureRecognizer = new UITouchesGestureRecognizer(e.NewElement, NativeView);
                    NativeView.AddGestureRecognizer(_gestureRecognizer);
                }

                e.NewElement.Invalidate();
            }
        }

        /// <summary>
        /// Raises the element property changed event.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">E.</param>
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == Layout.IsClippedToBoundsProperty.PropertyName)
            {
                Layer.MasksToBounds = Element.IsClippedToBounds;
            }

            if (_itemsOfInterest.Contains(e.PropertyName))
            {
                Element.Invalidate();
            }
        }

        /// <summary>
        /// Draw the specified rect.
        /// </summary>
        /// <param name="rect">Rect.</param>
        public override void Draw(CGRect rect)
        {
            if (Element.RequiresMask)
            {
                SetupMask(rect);
            }

            base.Draw(rect);

            using (CGContext context = UIGraphics.GetCurrentContext())
            {
                context.SetAllowsAntialiasing(true);
                context.SetShouldAntialias(true);
                context.SetShouldSmoothFonts(true);

                var canvas = new CGContextCanvas(context);
                Element.Draw(canvas, rect.GetRect());
            }
        }

        /// <summary>
        /// The items of interest.
        /// </summary>
        private static readonly HashSet<string> _itemsOfInterest = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            ContentView.ContentProperty.PropertyName,
            VisualElement.BackgroundColorProperty.PropertyName,
            VisualElement.IsVisibleProperty.PropertyName,
            Layout.IsClippedToBoundsProperty.PropertyName
        };

        /// <summary>
        /// Sets up the mask.
        /// </summary>
        private void SetupMask(CGRect rect)
        {
            var path = new PlatformPathOperationsAdapter();
            Element.GetControlMask(path, rect.GetRect());
            Layer.Mask = new CAShapeLayer
            {
                //Frame = rect,
                RasterizationScale = (nfloat)path.ContentsScale,
                //Bounds = rect,
                //ContentsRect = rect,
                //Position = rect.GetRect().TopLeft.GetCGPoint(),
                //ContentsScale = (nfloat)path.ContentsScale,
                //ContentsCenter = path.ContentsCenter.GetCGRect(),
                Path = path.GetPathResult(),
                //AllowsEdgeAntialiasing = true,
                ShouldRasterize = true,
                //FillRule = CAShapeLayer.FillRuleEvenOdd,
            };
        }

        /// <summary>
        /// Handles the invalidate.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="args">Arguments.</param>
        private void HandleInvalidate(object sender, EventArgs args)
        {
            SetNeedsDisplay();
        }

        #region redundant routines
        public override void TouchesBegan(NSSet touches, UIEvent evt)
        {
            // ignore Xamarin touch events on iOS, apparently they are buggy
        }

        public override void TouchesEnded(NSSet touches, UIEvent evt)
        {
            // ignore Xamarin touch events on iOS, apparently they are buggy
        }

        public override void TouchesCancelled(NSSet touches, UIEvent evt)
        {
            // ignore Xamarin touch events on iOS, apparently they are buggy
        }

        public override void TouchesMoved(NSSet touches, UIEvent evt)
        {
            // ignore Xamarin touch events on iOS, apparently they are buggy
        }
        #endregion
    }
}
