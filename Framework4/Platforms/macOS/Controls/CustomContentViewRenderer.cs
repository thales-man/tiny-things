using CoreAnimation;
using CoreGraphics;
using Foundation;
using NGraphics;
using Tiny.Framework.Controls;
using Tiny.Framework.Services.Internal;
using Tiny.Framework.Views;
using Xamarin.Forms;
using Xamarin.Forms.Platform.MacOS;

// TODO: working on it...
[assembly: ExportRenderer(typeof(CustomContentView), typeof(CustomContentViewRenderer))]
namespace Tiny.Framework.Controls
{
    /// <summary>
    /// Custom content view renderer.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class CustomContentViewRenderer : VisualElementRenderer<CustomContentView>
    {
        public override void DisplayRect(CGRect rect)
        {
            if (Element.Parent.RequiresMask)
            {
                SetupMask(rect);
            }

            base.DisplayRect(rect);
        }

        /// <summary>
        /// Sets up the mask.
        /// </summary>
        private void SetupMask(CGRect rect)
        {
            var path = new PlatformPathOperationsAdapter();
            Element.Parent.GetContentMask(path, rect.GetRect());
            Layer.Mask = new CAShapeLayer
            {
                Path = path.GetPathResult(),
                //AllowsEdgeAntialiasing = true,
                //ShouldRasterize = true,
                //FillRule = CAShapeLayer.FillRuleEvenOdd,
            };
        }
    }
}
