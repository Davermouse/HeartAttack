using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace HeartAttack
{
    public class BugManager
    {
        private Random m_Random = new Random();
        private List<Bug> m_Bugs = new List<Bug>();


        // TODO Make this change for difficulty settings
        private int m_TimeTillNextSpawn = 0; // in Milliseconds
        private int m_SpawnInterval = 20000; // in Milliseconds

        public BugManager()
        {}

        // TODO make appropriately random 
        private Bug GetNewBug()
        {
            return new Bug(new Vector2(100, 100), 10, 5);
        }
       
        public void Update(GameTime pGameTime)
        {
            m_TimeTillNextSpawn -= pGameTime.ElapsedGameTime.Milliseconds;

            if (m_TimeTillNextSpawn <= 0)
            {
                m_Bugs.Add(GetNewBug());
                m_TimeTillNextSpawn += m_SpawnInterval;
            }

            foreach (Bug bug in m_Bugs)
            {
                bug.Update(pGameTime);
            }
        }

        // TODO write this
        public void TestCollisions(BulletManager pBulletManager)
        {
            foreach (var bullet in pBulletManager.Bullets)
            {
                foreach (var bug in m_Bugs)
                {
                    if (bug.CollidesWith(bullet))
                    {
                        bug.HitByBullet(bullet);
                    }
                }
            }
        }

        // TODO write this
        public void TestCollisions(PlayerThing pPlayer)
        {
        }

        // TODO write this
        public void TestCollisions(PingManager pPingManager)
        {
            foreach (Ping ping in pPingManager.Pings)
            {
                foreach (Bug bug in m_Bugs)
                {
                    if (bug.CollidesWith(ping))
                    {
                        bug.ShowBug();
                    }
                }
            }
        }

        public void Draw()
        {
            foreach (Bug bug in m_Bugs)
            {
                bug.Draw();
            }
        }
    }
}
