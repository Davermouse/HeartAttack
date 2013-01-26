using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace HeartAttack
{
    public class PingManager
    {
        private List<Ping> m_Pings = new List<Ping>();

        public PingManager()
        {}

        public IEnumerable<Ping> Pings {
            get {
                return m_Pings;    
            }
        }

        public void FirePing()
        {
            Debug.WriteLine("Fire");
            m_Pings.Add(new Ping());
        }

        public void Update(GameTime pGameTime)
        {
            foreach (Ping ping in m_Pings)
            {
                ping.Update(pGameTime);
            }
        }

        public void Draw()
        {
            foreach (Ping ping in m_Pings)
            {
                ping.Draw();
            }
        }

        // TODO remove old pings

    }
}
