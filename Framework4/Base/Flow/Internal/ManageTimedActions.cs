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
    /// a managed polling timer class.
    /// </summary>
    /// <seealso cref="IManageTimedActions" />
    [Export(typeof(IManageTimedActions))]
    internal sealed class ManageTimedActions :
        IManageTimedActions,
        ISupportServiceRegistration
    {
        private int _interval;
        private WeakAction _managedAction = null;
        private Timer _timer = null;
        private bool _reload;
        private int _passThroughLock = 0;

        /// <summary>
        /// Sets the invocation interval.
        /// </summary>
        /// <param name="seconds">the seconds.</param>
        public void SetInvocationInterval(int seconds)
        {
            _interval = seconds;
            _reload = true;
        }

        /// <summary>
        /// Sets the managed action.
        /// </summary>
        /// <param name="managedAction">the managed action.</param>
        public void SetManagedAction(Action managedAction)
        {
            _managedAction = new WeakAction(managedAction);
            _reload = true;
        }

        /// <inheritdoc/>
        public void Start()
        {
            if (_reload)
            {
                Finished();
            }

            Load();

            _timer.Change(0, _interval);
        }

        /// <inheritdoc/>
        public void Start(int seconds, Action managedAction)
        {
            _interval = seconds;
            _managedAction = new WeakAction(managedAction);
            _reload = true;

            Start();
        }

        /// <inheritdoc/>
        public void Stop()
        {
            if (_timer != null)
            {
                _timer.Change(-1, 0);
            }
        }

        /// <inheritdoc/>
        public void Finished()
        {
            if (_timer != null)
            {
                Stop();
                _timer.Dispose();
                _timer = null;
            }
        }

        /// <summary>
        /// Loads this instance.
        /// </summary>
        public void Load()
        {
            if (_timer == null)
            {
                var interval = _interval * 1000;

                if (interval < int.MaxValue)
                {
                    _timer = new Timer(TimerLapsed, null, -1, 0);
                }
            }
        }

        /// <summary>
        /// Handles the Elapsed event of the timer control.
        /// </summary>
        /// <param name="state">the state.</param>
        private void TimerLapsed(object state)
        {
            if (_managedAction.IsAlive
                && Interlocked.Increment(ref _passThroughLock) == 1)
            {
                _managedAction.Do();
                Interlocked.Exchange(ref _passThroughLock, 0);
            }
        }
    }
}
