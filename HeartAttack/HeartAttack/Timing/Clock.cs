using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace HeartAttack.Timing
{
    public enum ClockState
    {
        Playing,
        Paused,
        Stopped
    }

    class Clock
    {
        double elapsedTime;

        public Clock(ClockManager clockManager)
        {
            Manager = clockManager;
        }

        public ClockManager Manager
        {
            get;
            private set;
        }

        public ClockState State
        {
            get;
            private set;
        }

        public double Duration
        {
            get;
            set;
        }

        public void Start()
        {
            switch (State)
            {
                case ClockState.Paused:
                    Reset();
                    Manager.Clocks.Add(this);
                    break;
                case ClockState.Playing:
                    Reset();
                    break;
                case ClockState.Stopped:
                    Manager.Clocks.Add(this);
                    break;
            }

            State = ClockState.Playing;
        }

        public void Stop()
        {
            switch (State)
            {
                case ClockState.Paused:
                case ClockState.Playing:
                    Manager.Clocks.Remove(this);
                    break;
                case ClockState.Stopped:
                    break;
            }

            State = ClockState.Stopped;
        }

        public void Pause()
        {
            switch (State)
            {
                case ClockState.Paused:
                    break;
                case ClockState.Playing:
                    Manager.Clocks.Remove(this);
                    break;
                case ClockState.Stopped:
                    break;
            }

            State = ClockState.Paused;
        }

        public void Resume()
        {
            switch (State)
            {
                case ClockState.Paused:
                    Manager.Clocks.Add(this);
                    break;
                case ClockState.Playing:
                    // huh? ignore it.
                    break;
                case ClockState.Stopped:
                    Manager.Clocks.Add(this);
                    break;
            }

            State = ClockState.Playing;
        }

        public event EventHandler Begin;
        public event EventHandler Complete;

        public void Reset()
        {
        }

        public void Update(GameTime time)
        {

        }
    }
}
