using NGraphics;
using Tiny.Framework.Helpers;
using Point = NGraphics.Point;
using Rect = NGraphics.Rect;
using Rectangle = Xamarin.Forms.Rectangle;

namespace Tiny.Framework.Services
{
    /// <summary>
    /// Path operations (abstract) adapter.
    /// </summary>
    public abstract class PathOperationsAdapter<TPathContainer> :
        IAdaptPathOperations<TPathContainer>
        where TPathContainer : class, new()
    {
        /// <summary>
        /// Gets the current point.
        /// </summary>
        public abstract Point CurrentPoint { get; }

        /// <summary>
        /// Gets the curve continuation.
        /// </summary>
        /// <value>The curve continuation.</value>
        public abstract Point CurveContinuation { get; }

        // TODO: size and centre => contents and masking, working on it...
        /// <summary>
        /// Gets or sets the contents scale.
        /// </summary>
        /// <value>The contents scale.</value>
        public double ContentsScale { get; set; }

        /// <summary>
        /// Gets or sets the contents center.
        /// </summary>
        /// <value>The contents center.</value>
        public Rect ContentsCenter { get; set; }

        /// <summary>
        /// Gets the path operations.
        /// </summary>
        protected TPathContainer PathOperations { get; } = new TPathContainer();

        /// <summary>
        /// Draws the ellipse.
        /// </summary>
        /// <param name="frame">Frame.</param>
        public abstract void DrawEllipse(Rect frame);

        /// <summary>
        /// Gets the path result.
        /// </summary>
        /// <returns>The path result.</returns>
        public TPathContainer GetPathResult() => PathOperations;

        /// <summary>
        /// Sets the scale.
        /// </summary>
        /// <param name="usingViewport">Using viewport.</param>
        /// <param name="withinBounds">Within bounds.</param>
        public void SetScale(Rect usingViewport, Rect withinBounds)
        {
            ContentsScale = usingViewport.GetScale(withinBounds);
            ContentsCenter = new Rect(usingViewport.GetNegativeMidPoint(), withinBounds.Size);
        }

        /// <summary>
        /// Sets the scale.
        /// </summary>
        /// <param name="usingViewport">Using viewport.</param>
        /// <param name="withinBounds">Within bounds.</param>
        public void SetScale(Rectangle usingViewport, Rect withinBounds)
        {
            ContentsScale = usingViewport.GetScale(withinBounds);
            ContentsCenter = new Rect(usingViewport.GetNegativeMidPoint(), withinBounds.Size);
        }

        /// <summary>
        /// Visit the specified operation.
        /// </summary>
        /// <param name="moveTo">Operation.</param>
        public abstract void Visit(MoveTo moveTo);

        /// <summary>
        /// Visit the specified operation.
        /// </summary>
        /// <param name="lineTo">Operation.</param>
        public abstract void Visit(LineTo lineTo);

        /// <summary>
        /// Visit the specified operation.
        /// </summary>
        /// <param name="curveTo">Operation.</param>
        public abstract void Visit(CurveTo curveTo);

        /// <summary>
        /// Visit the specified operation.
        /// </summary>
        /// <param name="arcTo">Operation.</param>
        public abstract void Visit(ArcTo arcTo);

        /// <summary>
        /// Visit the specified operation.
        /// </summary>
        /// <param name="operation">Operation.</param>
        public abstract void Visit(ClosePath closePath);
    }
}
