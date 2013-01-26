using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace HeartAttack.Oximeter
{
    public class OximeterManager
    {
        private int actualHeartRate;

        public OximeterManager()
        {
            this.SimulatedHeartRate = 20;
        }

        private OximeterComms Oximeter
        {
            get;
            set;
        }

        public bool IsConnected
        {
            get;
            private set;
        }

        public bool HasBeat
        {
            get;
            private set;
        }

        public int HeartRate
        {
            get
            {
                if (IsConnected)
                {
                    return actualHeartRate;
                }
                else
                {
                    return SimulatedHeartRate;
                }
            }
        }

        public double LastBeat
        {
            get;
            private set;
        }

        public int SimulatedHeartRate
        {
            get;
            set;
        }

        public void Start()
        {
            Oximeter = new OximeterComms();
            Oximeter.Setup("");

            Oximeter.OximeterActivated += () =>
                this.IsConnected = true;

            Oximeter.OximeterDeactivated += () =>
                this.IsConnected = false;

            Oximeter.OximeterPulse += (r) =>
                {
                    this.actualHeartRate = r;
                    this.HasBeat = true;
                };
        }

        public void Update(GameTime time)
        {
            if (!this.IsConnected)
            {
                double millisBetweenBeats = 60000 / SimulatedHeartRate;

                if (time.TotalGameTime.TotalMilliseconds - 
                    LastBeat >
                    millisBetweenBeats)
                {
                    HasBeat = true;
                    LastBeat = time.TotalGameTime.TotalMilliseconds;
                }
            }
        }

        public void ResetBeat()
        {
            this.HasBeat = false;
        }
    }
}
