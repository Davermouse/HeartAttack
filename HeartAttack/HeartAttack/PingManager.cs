using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using System.Diagnostics;
using HeartAttack.Timing;

namespace HeartAttack
{
    public class PingManager
    {
        private MainGameScene scene;

        public SoundEffect m_HeartBeat;
        private List<Ping> m_Pings = new List<Ping>();

        public PingManager(MainGameScene scene)
        {
            m_HeartBeat = HeartAttack.theGameInstance.Content.Load<SoundEffect>("heartBeat1");

            this.scene = scene;
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
            var newPing = new Ping(scene);
            scene.Entities.Add(newPing);
        }

        public void Update(GameTime pGameTime)
        {
            if (HeartAttack.theGameInstance.Oximeter.HasBeat)
            {
                FirePing();
                scene.Player.LastBeatTime = pGameTime.TotalGameTime.TotalSeconds;
            }

            foreach (Ping ping in m_Pings)
            {
                ping.Update(pGameTime);
            }
        }

       /* public void Draw()
        {
            foreach (Ping ping in m_Pings)
            {
                ping.Draw();
            }
        }
        */
        // TODO remove old pings

    }
}
