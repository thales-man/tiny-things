namespace Tiny.Framework.Helpers
{
    public static class NGraphicsHelper
    {
        public static NGraphics.Color AsNGColor(this Xamarin.Forms.Color source) =>
            new NGraphics.Color(source.R, source.G, source.B, source.A);
    }
}

