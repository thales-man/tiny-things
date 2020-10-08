//-----------------------------------------------------------------------------
// copyright (c) 2016, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
//-----------------------------------------------------------------------------
using System;
using System.Composition;
using System.Threading;
using Tiny.Framework.Registration;

namespace Tiny.Framework.Flow.Internal
{
    /// <summary>
    /// an injectable auto reset event action synchroniser.
    /// </summary>
    /// <seealso cref="IDisposable" />
    /// <seealso cref="ISynchroniseActions" />
    [Export(typeof(ISynchroniseActions))]
    internal sealed class SynchroniseActions :
        IDisposable,
        ISynchroniseActions,
        ISupportServiceRegistration
    {
        /// <summary>
        /// _is waiting.
        /// </summary>
        private int _isWaiting = 0;

        /// <summary>
        /// _query complete.
        /// </summary>
        private AutoResetEvent _queryComplete = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="SynchroniseActions"/> class.
        /// </summary>
        public SynchroniseActions()
        {
            _queryComplete = new AutoResetEvent(true);
        }

        /// <inheritdoc/>
        public bool IsClosed
        {
            get { return _queryComplete == null; }
        }

        /// <inheritdoc/>
        public bool IsWaiting
        {
            get { return _isWaiting > 0; }
        }

        /// <inheritdoc/>
        public bool IsSynchronising { get; set; }

        /// <inheritdoc/>
        public bool WaitTillReady()
        {
            if (!IsClosed)
            {
                Interlocked.Exchange(ref _isWaiting, 1);

                bool value = _queryComplete.WaitOne();

                Interlocked.Exchange(ref _isWaiting, 0);

                return value;
            }

            return false;
        }

        /// <inheritdoc/>
        public bool Finish()
        {
            if (!IsClosed)
            {
                var value = _queryComplete.Set();

                IsSynchronising = false;

                return value;
            }

            return IsSynchronising = false;
        }

        /// <inheritdoc/>
        public bool Start()
        {
            if (!IsClosed && !IsSynchronising && !IsWaiting)
            {
                return IsSynchronising = _queryComplete.Reset();
            }

            return false;
        }

        /// <summary>
        /// Closes this instance.
        /// </summary>
        public void Close()
        {
            _queryComplete.Dispose();
            _queryComplete = null;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Close();
        }

        /// <inheritdoc/>
        public void Update(Action actionDo)
        {
            while (!Start())
            {
                WaitTillReady();
            }

            actionDo();
            Finish();
        }
    }
}
