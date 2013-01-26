using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HeartAttack.Timing
{
    public class Timer : Clock
    {
        public Timer(ClockManager manager) :
            base(manager)
        {
        }

        public Timer(ClockManager manager, float duration, bool autoStart, EventHandler complete)
            : base(manager)
        {
            Duration = duration;
            Complete += complete;
            if (autoStart)
                Start();
        }
    }
}
