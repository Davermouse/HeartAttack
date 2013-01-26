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

        private MainGameScene scene;

        // TODO Make this change for difficulty settings
        private int m_TimeTillNextSpawn = 0; // in Milliseconds
        private int m_SpawnInterval = 5000; // in Milliseconds

        public BugManager(MainGameScene scene)
        {
            this.scene = scene;
        }

        // TODO make appropriately random 
        private Bug GetNewBug()
        {
            
            var angle = (float)(m_Random.NextDouble() * MathHelper.TwoPi);

            var minScreenDimension = MathHelper.Min(
                HeartAttack.theGameInstance.graphics.GraphicsDevice.Viewport.Height,
                HeartAttack.theGameInstance.graphics.GraphicsDevice.Viewport.Width);

            //var distance = (float)(m_Random.NextDouble() * (minScreenDimension * 2 / 3)) + (minScreenDimension / 3);
            var distance = (float)(m_Random.NextDouble() * (minScreenDimension )) + (minScreenDimension / 3);

            var center = DirtyGlobalHelpers.CentreOfScreen();

            var position = new Vector2(
                center.X + distance * (float)Math.Sin(angle),
                center.Y + distance * (float)Math.Cos(angle));

            var speed = (float)(5 + random.NextDouble() * 10);
            return new Bug(scene, position, 10, speed);
        }
       
        public void Update(GameTime pGameTime)
        {
            m_TimeTillNextSpawn -= pGameTime.ElapsedGameTime.Milliseconds;

            if (m_TimeTillNextSpawn <= 0)
            {
                scene.Entities.Add(GetNewBug());
                m_TimeTillNextSpawn += m_SpawnInterval;
            }
        }
        /*
        // TODO write this
        public void TestCollisions(BulletManager pBulletManager)
        {
            foreach (var bullet in pBulletManager.Bullets)
            {
                foreach (var bug in m_Bugs.Where(b => !b.IsDead))
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
            foreach (var bug in m_Bugs.Where(b => !b.IsDead))
            {
                if (bug.CollidesWith(pPlayer))
                {
                    bug.HitPlayer(pPlayer);
                }
            }
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
        */
        /*public void Draw()
        {
            foreach (Bug bug in m_Bugs)
            {
                bug.Draw();
            }
        }*/
    }
}
