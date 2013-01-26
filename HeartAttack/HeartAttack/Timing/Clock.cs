using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace HeartAttack.Timing
{
    public enum ClockState
    {
        Playing,
        Paused,
        Stopped,
    }

    public class Clock
    {
        protected Clock(ClockManager manager)
        {
            Duration = 1.0; // 1 sec
            State = ClockState.Stopped;
            ClockManager = manager;
        }

        public void Start()
        {
            switch (State)
            {
                case ClockState.Paused:
                    Reset();
                    ClockManager.Clocks.Add(this);
                    break;
                case ClockState.Playing:
                    // we dont need to add because these states should already be added.
                    Reset();
                    break;
                case ClockState.Stopped:
                    // we dont need to reset, because stopped clocks already are
                    ClockManager.Clocks.Add(this);
                    break;
            }

            State = ClockState.Playing;

            if (BeginOffset == 0.0)
            {
                // clocks without BeginOffset update syncronously
                Update(0.0);
            }
        }

        public void Stop()
        {
            switch (State)
            {
                case ClockState.Paused:
                case ClockState.Playing:
                    ClockManager.Clocks.Remove(this);
                    break;
                case ClockState.Stopped:
                    // nothing to do, we're already stopped
                    break;
            }

            State = ClockState.Stopped;
        }

        public void Pause()
        {
            switch (State)
            {
                case ClockState.Paused:
                    // success!
                    break;
                case ClockState.Playing:
                    ClockManager.Clocks.Remove(this);
                    break;
                case ClockState.Stopped:
                    // huh?  ignore it.
                    break;
            }

            State = ClockState.Paused;
        }

        public void Resume()
        {
            switch (State)
            {
                case ClockState.Paused:
                    ClockManager.Clocks.Add(this);
                    break;
                case ClockState.Playing:
                    // huh? ignore it.
                    break;
                case ClockState.Stopped:
                    ClockManager.Clocks.Add(this);
                    break;
            }

            State = ClockState.Playing;
        }

        public void SkipToBegin()
        {
            if (_totalElapsedTime < BeginOffset)
            {
                _totalElapsedTime = BeginOffset;
            }
        }

        public void Reset()
        {
            _totalElapsedTime = 0.0;
            Progress = 0.0f;
        }

        public void Update(double elapsedTime)
        {
            Debug.Assert(State == ClockState.Playing);

            var beginOffset = BeginOffset;
            var oldTotalElapsedTime = _totalElapsedTime;
            _totalElapsedTime += elapsedTime;

            if (_totalElapsedTime >= beginOffset)
            {
                if (oldTotalElapsedTime <= beginOffset)
                {
                    OnBegin();
                }

                var effectiveElapsedTime = _totalElapsedTime - beginOffset;
                var oldEffectiveElapsedTime = oldTotalElapsedTime - beginOffset;
                var duration = Duration;
                var segment = (int)(effectiveElapsedTime / duration);
                var oldSegment = (int)(oldEffectiveElapsedTime / duration);
                var autoReverse = AutoReverse;
                var totalSegments = (long)RepeatCount * (autoReverse ? 2L : 1L) + (autoReverse ? 1L : 0L);

                if (segment > totalSegments)
                {
                    // completed
                    Progress = (autoReverse ? 0.0f : 1.0f);
                    Stop();
                    OnTicked(Progress);
                    OnCompleted();
                }
                else
                {
                    Progress = (float)((effectiveElapsedTime % duration) / duration);

                    // odd loops are flipped if we're auto-reversing
                    if (autoReverse && segment % 2 == 1)
                    {
                        Progress = 1.0f - Progress;
                    }

                    for (int i = oldSegment; i < segment; i++)
                    {
                        // in autoReverse mode, 'loop' only once we've gone there & back
                        // i.e. 2 'loops'
                        if (!autoReverse || i % 2 == 1)
                        {
                            OnLooped();
                        }
                    }

                    OnTicked(Progress);
                }
            }
        }

        protected virtual void OnTicked(float progress) { }

        protected virtual void OnBegin()
        {
            if (Begin != null)
            {
                Begin(this, EventArgs.Empty);
            }
        }

        protected virtual void OnCompleted()
        {
            if (Complete != null)
            {
                Complete(this, EventArgs.Empty);
            }
        }

        protected virtual void OnLooped()
        {
            if (Looped != null)
            {
                Looped(this, EventArgs.Empty);
            }
        }

        public bool AutoReverse { get; set; }
        public int RepeatCount { get; set; }
        public double BeginOffset { get; set; }
        public double Duration { get; set; }

        public float Progress { get; private set; }
        public ClockState State { get; private set; }
        public ClockManager ClockManager { get; private set; }

        public event EventHandler Begin;
        public event EventHandler Complete;
        public event EventHandler Looped;

        double _totalElapsedTime;
    }
}
