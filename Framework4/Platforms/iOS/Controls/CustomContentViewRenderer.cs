//-----------------------------------------------------------------------------
// copyright (c) 2019, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
//-----------------------------------------------------------------------------
using System;
using CoreAnimation;
using CoreGraphics;
using NGraphics;
using Tiny.Framework.Controls;
using Tiny.Framework.Services.Internal;
using Tiny.Framework.Views;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomContentView), typeof(CustomContentViewRenderer))]
namespace Tiny.Framework.Controls
{
    /// <summary>
    /// Custom content view renderer.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class CustomContentViewRenderer : VisualElementRenderer<CustomContentView>
    {
        /// <summary>
        /// Draw the specified rect.
        /// </summary>
        /// <param name="rect">Rect.</param>
        public override void Draw(CGRect rect)
        {
            if (Element.Parent.RequiresMask)
            {
                SetupMask(rect);
            }

            base.Draw(rect);
        }

        /// <summary>
        /// Sets up the mask.
        /// </summary>
        private void SetupMask(CGRect rect)
        {
            var visitor = new PlatformPathOperationsAdapter();
            Element.Parent.GetContentMask(visitor, rect.GetRect());

            Layer.Mask = new CAShapeLayer
            {
                Frame = rect,
                Transform = CATransform3D.MakeScale((nfloat)visitor.ContentsScale, (nfloat)visitor.ContentsScale, (nfloat)visitor.ContentsCenter.Size.Width),
                Path = visitor.GetPathResult(),
                ContentsRect = rect,
                ShadowColor = Xamarin.Forms.Color.AliceBlue.ToCGColor(),
                RasterizationScale = (nfloat)visitor.ContentsScale,
                ContentsScale = (nfloat)visitor.ContentsScale,
                ContentsCenter = visitor.ContentsCenter.GetCGRect(),
                ContentsFormat = CAContentsFormat.Gray8Uint,
                //AllowsEdgeAntialiasing = true,
                ShouldRasterize = true,
                //FillRule = CAShapeLayer.FillRuleEvenOdd,
            };
        }
    }
}
