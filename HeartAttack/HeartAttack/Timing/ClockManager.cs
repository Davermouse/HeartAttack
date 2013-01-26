using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace HeartAttack.Timing
{
    class ClockManager
    {
        public List<Clock> Clocks
        {
            get;
            private set;
        }

        public void Update(GameTime time)
        {
            double elapsedTime = time.TotalGameTime.TotalSeconds;

            // This means that clocks can remove themselves etc.
            var tempClocks = Clocks.ToList();

            foreach (var clock in tempClocks)
            {
                clock.Update(time);
            }
        }
    }
}
