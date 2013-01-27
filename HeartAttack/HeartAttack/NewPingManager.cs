using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace HeartAttack
{
    public class NewPingManager
    {
        private Vector4 m_PingDistances;
        private int m_Oldest;
        private SoundEffect m_HeartBeat;
        private MainGameScene scene;

        public NewPingManager(MainGameScene scene)
        {
            m_Oldest = 0;
            m_PingDistances = new Vector4(0, 0, 0, 0);
            m_HeartBeat = HeartAttack.theGameInstance.Content.Load<SoundEffect>("heartBeat1");
            this.scene = scene;
        }

        public Vector4 Pings { get { return m_PingDistances; } }

        public void Update(GameTime pGameTime)
        {
            float dx = DirtyGlobalHelpers.PING_SPEED * (float)pGameTime.ElapsedGameTime.Milliseconds/1000;
            m_PingDistances.X += dx;
            m_PingDistances.Y += dx;
            m_PingDistances.Z += dx;
            m_PingDistances.W += dx;

            if (HeartAttack.theGameInstance.Oximeter.HasBeat)
            {
                FirePing();
                scene.Player.LastBeatTime = pGameTime.TotalGameTime.TotalSeconds;
            }

            foreach (var bug in scene.Entities.OfType<Bug>())
            {
                if (bug.CollidesWith(m_PingDistances))
                {
                    bug.ShowBug();
                }
            }
        }

        public void FirePing()
        {
            switch(m_Oldest)
            {
                case 0:
                    m_PingDistances.X = 0;
                    m_Oldest = 1;
                    break;
                case 1:
                    m_PingDistances.Y = 0;
                    m_Oldest = 2;
                    break;
                case 2:
                    m_PingDistances.Z = 0;
                    m_Oldest = 3;
                    break;
                case 3:
                    m_PingDistances.W = 0;
                    m_Oldest = 0;
                    break;
            }
            m_HeartBeat.Play();
            var newPing = new Ping(scene);
        }
    }
}
