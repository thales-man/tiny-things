//-----------------------------------------------------------------------------
// copyright (c) 2019, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
//
// a cleaned up version of code originally provided by Christian Falch 
// which can be found here:
// https://github.com/chrfalch/NControl/blob/develop/NControl/NControl.iOS/UITouchesGestureRecognizer.cs
//-----------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;
using NGraphics;
using Tiny.Framework.Views;
using UIKit;

namespace Tiny.Framework.Controls
{
    /// <summary>
    /// User interface touches gesture recognizer.
    /// </summary>
    public sealed class UITouchesGestureRecognizer :
        UIGestureRecognizer
    {
        /// <summary>
        /// The element.
        /// </summary>
        private readonly CustomControlView _element;

        /// <summary>
        /// The native view.
        /// </summary>
        private readonly UIView _nativeView;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Tiny.Framework.Controls.UITouchesGestureRecognizer"/> class.
        /// </summary>
        /// <param name="element">Element.</param>
        /// <param name="nativeView">Native view.</param>
        public UITouchesGestureRecognizer(CustomControlView element, UIView nativeView)
        {
            _element = element
                ?? throw new ArgumentNullException(nameof(element));
            _nativeView = nativeView
                ?? throw new ArgumentNullException(nameof(nativeView));
        }

        /// <summary>
        /// Gets the touch points.
        /// </summary>
        /// <returns>The touch points.</returns>
        /// <param name="touches">Touches.</param>
        private IEnumerable<Point> GetTouchPoints(NSSet touches)
        {
            foreach (var touch in touches.OfType<UITouch>())
            {
                var touchPoint = touch.LocationInView(_nativeView);
                yield return new Point(touchPoint.X, touchPoint.Y);
            }
        }

        /// <summary>
        /// Touches began.
        /// </summary>
        /// <param name="touches">Touches.</param>
        /// <param name="evt">Evt.</param>
        public override void TouchesBegan(NSSet touches, UIEvent evt)
        {
            base.TouchesBegan(touches, evt);

            State = _element.TouchesBegan(GetTouchPoints(touches))
                ? UIGestureRecognizerState.Began
                : UIGestureRecognizerState.Cancelled;
        }

        /// <summary>
        /// Touches moved.
        /// </summary>
        /// <param name="touches">Touches.</param>
        /// <param name="evt">Evt.</param>
        public override void TouchesMoved(NSSet touches, UIEvent evt)
        {
            base.TouchesMoved(touches, evt);

            if (_element.TouchesMoved(GetTouchPoints(touches)))
            {
                State = UIGestureRecognizerState.Changed;
            }
        }

        /// <summary>
        /// Touches ended.
        /// </summary>
        /// <param name="touches">Touches.</param>
        /// <param name="evt">Evt.</param>
        public override void TouchesEnded(NSSet touches, UIEvent evt)
        {
            base.TouchesEnded(touches, evt);

            if (_element.TouchesEnded(GetTouchPoints(touches)))
            {
                State = UIGestureRecognizerState.Ended;
            }
        }

        /// <summary>
        /// Touches cancelled.
        /// </summary>
        /// <param name="touches">Touches.</param>
        /// <param name="evt">Evt.</param>
        public override void TouchesCancelled(NSSet touches, UIEvent evt)
        {
            base.TouchesCancelled(touches, evt);

            if (_element.TouchesCancelled(GetTouchPoints(touches)))
            {
                State = UIGestureRecognizerState.Cancelled;
            }
        }
    }
}


