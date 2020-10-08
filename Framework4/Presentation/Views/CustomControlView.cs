//-----------------------------------------------------------------------------
// copyright (c) 2019, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
//-----------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using NGraphics;
using Tiny.Framework.Services;
using Xamarin.Forms;
using Point = NGraphics.Point;

namespace Tiny.Framework.Views
{
    /// <summary>
    /// Custom control view.
    /// </summary>Custom
    [ContentProperty("CustomContent")]
    public abstract class CustomControlView : ContentView
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:Tiny.Framework.Views.CustomControlView"/> class.
        /// </summary>
        protected CustomControlView()
        {
            ContentContainer = new CustomContentView(this);
            Content = ContentContainer;
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="T:Tiny.Framework.Views.CustomControlView"/> requires mask.
        /// </summary>
        /// <value><c>true</c> if requires mask; otherwise, <c>false</c>.</value>
        public bool RequiresMask { get; internal set; }

        /// <summary>
        /// Occurs when on invalidate.
        /// </summary>
        public event EventHandler OnInvalidate;

        /// <summary>
        /// Occurs when on touches began.
        /// </summary>
        public event EventHandler<IEnumerable<Point>> OnTouchesBegan;

        /// <summary>
        /// Occurs when on touches moved.
        /// </summary>
        public event EventHandler<IEnumerable<Point>> OnTouchesMoved;

        /// <summary>
        /// Occurs when on touches ended.
        /// </summary>
        public event EventHandler<IEnumerable<Point>> OnTouchesEnded;

        /// <summary>
        /// Occurs when on touches cancelled.
        /// </summary>
        public event EventHandler<IEnumerable<Point>> OnTouchesCancelled;

        /// <summary>
        /// Gets the content container.
        /// </summary>
        /// <value>The content container.</value>
        internal CustomContentView ContentContainer { get; private set; }

        /// <summary>
        /// Gets or sets the content of the custom.
        /// </summary>
        /// <value>The content of the custom.</value>
        public View CustomContent
        {
            get => ContentContainer.Content;
            set => ContentContainer.Content = value;
        }

        /// <summary>
        /// Sets the requires mask.
        /// </summary>
        /// <param name="newValue">If set to <c>true</c> new value.</param>
        protected void SetRequiresMask(bool newValue) => RequiresMask = newValue;

        /// <summary>
        /// Gets the control mask.
        /// </summary>
        /// <param name="usingVisitor">Using visitor.</param>
        /// <param name="withinBounds">Within bounds.</param>
        public virtual void GetControlMask(IVisitPathGenerators usingVisitor, Rect withinBounds) =>
            new NotSupportedException("GetControlMask => I need to be overridden");

        /// <summary>
        /// Gets the content mask.
        /// </summary>
        /// <param name="usingVisitor">Using visitor.</param>
        /// <param name="withinBounds">Within bounds.</param>
        public virtual void GetContentMask(IVisitPathGenerators usingVisitor, Rect withinBounds) =>
            new NotSupportedException("GetContentMask => I need to be overridden");

        /// <summary>
        /// Draw on the specified canvas within bounds.
        /// </summary>
        /// <param name="onCanvas">On canvas.</param>
        /// <param name="withinBounds">Within bounds.</param>
        public abstract void Draw(ICanvas onCanvas, Rect withinBounds);

        /// <summary>
        /// Invalidate this instance.
        /// </summary>
        public void Invalidate()
        {
            OnInvalidate?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Touches began.
        /// </summary>
        /// <returns><c>true</c>, if began touches, <c>false</c> otherwise.</returns>
        /// <param name="points">Points.</param>
        public virtual bool TouchesBegan(IEnumerable<Point> points)
        {
            OnTouchesBegan?.Invoke(this, points);
            return OnTouchesBegan != null;
        }

        /// <summary>
        /// Touches moved.
        /// </summary>
        /// <returns><c>true</c>, if touches moved, <c>false</c> otherwise.</returns>
        /// <param name="points">Points.</param>
        public virtual bool TouchesMoved(IEnumerable<Point> points)
        {
            OnTouchesMoved?.Invoke(this, points);
            return OnTouchesMoved != null;
        }

        /// <summary>
        /// Touches cancelled.
        /// </summary>
        /// <returns><c>true</c>, if touches cancelled, <c>false</c> otherwise.</returns>
        /// <param name="points">Points.</param>
        public virtual bool TouchesCancelled(IEnumerable<Point> points)
        {
            OnTouchesCancelled?.Invoke(this, points);
            return OnTouchesCancelled != null;
        }

        /// <summary>
        /// Touches ended.
        /// </summary>
        /// <returns><c>true</c>, if touches ended, <c>false</c> otherwise.</returns>
        /// <param name="points">Points.</param>
        public virtual bool TouchesEnded(IEnumerable<Point> points)
        {
            OnTouchesEnded?.Invoke(this, points);
            return OnTouchesEnded != null;
        }

        /// <summary>
        /// On property changed.
        /// </summary>
        /// <param name="propertyName">Property name.</param>
        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            Invalidate();
        }
    }
}
