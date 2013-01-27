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
            this.SimulatedHeartRate = 70;
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

        public bool HasHeartbeat
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
                if (HasHeartbeat)
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

            if (!Oximeter.Setup("com3"))
            {
                IsConnected = false;
                return;
            }
            else
            {
                IsConnected = true;
            }

            Oximeter.OximeterActivated += () =>
                this.HasHeartbeat = true;

            Oximeter.OximeterDeactivated += () =>
                this.HasHeartbeat = false;

            Oximeter.OximeterPulse += (r) =>
                {
                    this.actualHeartRate = r;
                    this.HasBeat = true;
                };
        }

        public void Update(GameTime time)
        {
            if (!this.HasHeartbeat)
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
