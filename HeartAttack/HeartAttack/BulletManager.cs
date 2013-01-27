using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace HeartAttack
{
    public class BulletManager
    {
        private MainGameScene m_Scene;
        private List<Bullet> m_Bullets;

        public BulletManager(MainGameScene scene)
        {
            m_Bullets = new List<Bullet>();
            this.m_Scene = scene;
        }

        public List<Bullet> Bullets
        {
            get { return m_Bullets; }
        }

        public void AddBullet(Bullet pBullet)
        {
            if (pBullet != null)
            {
                m_Bullets.Add(pBullet); 
            }
           
        }

        public void Update(GameTime pGameTime)
        {
            foreach (Bullet bullet in m_Bullets)
            {
                bullet.Update(pGameTime);
            }
            HeartAttack.theGameInstance.maxKillChain = CalculateChainLength();
            m_Bullets = m_Bullets.Where(b => !b.CanDelete).ToList();
        }

        public int CalculateChainLength()
        {
            int countHits = 0;
            int maxChainCount = 0;

            foreach (var pair in m_Scene.Entities.OfType<Bullet>().Select((b,i) => new { b = b, i = i}))
            {
                Bullet bullet = pair.b;
                if(bullet.IsMiss)
                {
                    if (maxChainCount < countHits) 
                    {
                        maxChainCount = countHits;                        
                    }
                   foreach (var deadBullet in m_Scene.Entities.OfType<Bullet>().Take(pair.i))
                        {
                            deadBullet.Kill();
                        }
                    countHits = 0;
                }
                else if (bullet.IsDead)
                {
                    countHits++;
                }
                else
                {
                    // still in transit
                    countHits = 0;
                }
            }
            return maxChainCount;
        }

      /*  public void Draw()
        {
            foreach (Bullet bullet in m_Bullets)
            {
                bullet.Draw();
            }
        }*/
    }
}
