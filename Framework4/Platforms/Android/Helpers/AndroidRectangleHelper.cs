// -----------------------------------------------------------------------------
// copyright (c) 2019, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
// -----------------------------------------------------------------------------
using NGraphics;

namespace Tiny.Framework.Helpers
{
    /// <summary>
    /// Rectangle extension.
    /// </summary>
    public static class AndroidRectangleHelper
    {
        public static Rect GetRect(this Android.Graphics.Rect usingSource) =>
            new Rect(new Point(usingSource.Left, usingSource.Top), new Size(usingSource.Width(), usingSource.Height()));
    }
}

