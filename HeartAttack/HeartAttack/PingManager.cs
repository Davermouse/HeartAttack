using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using System.Diagnostics;

namespace HeartAttack
{
    public class PingManager
    {
        public SoundEffect m_HeartBeat;
        private List<Ping> m_Pings = new List<Ping>();

        public PingManager()
        {
            m_HeartBeat = HeartAttack.theGameInstance.Content.Load<SoundEffect>("heartBeat1");
        }

        public IEnumerable<Ping> Pings {
            get {
                return m_Pings;    
            }
        }

        public void FirePing()
        {
            Debug.WriteLine("Fire");
            m_HeartBeat.Play();
            m_Pings.Add(new Ping());
        }

        public void Update(GameTime pGameTime)
        {
            if (HeartAttack.theGameInstance.Oximeter.HasBeat)
            {
                FirePing();
            }

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
