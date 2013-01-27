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
        
        private List<Bullet> m_Bullets;

        public BulletManager()
        {
            m_Bullets = new List<Bullet>();
            
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
            int startIndex = 0;
            int countHits = 0;
            int maxChainCount = 0;
            for (int i = 0; i < m_Bullets.Count; i++)
            {
                Bullet bullet = m_Bullets[i];
                if(bullet.IsMiss)
                {
                    if (maxChainCount < countHits) 
                    {
                        maxChainCount = countHits;                        
                    }
                    for (int j = startIndex; j <= i; j++)
                    {
                        Bullet bullet2 = m_Bullets[j];
                        bullet2.CanDelete = true;
                    }
                    startIndex = i+1;
                    countHits = 0;
                }
                else if (bullet.IsDead)
                {
                    countHits++;
                }
                else
                {
                    // still in transit
                    startIndex = i+1;
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
