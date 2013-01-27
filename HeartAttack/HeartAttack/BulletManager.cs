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

            m_Bullets = m_Bullets.Where(b => !b.IsDead).ToList();
        }

        //public int CalculateChainLength()
        //{
        //    bool noneAreMissOrDead = true;
        //    int countHits = 0;
        //    int maxChainCount = 0;
        //    foreach (Bullet bullet in m_Bullets)
        //    {
        //        if(bullet.IsMiss)
        //        {
        //            if (maxChainCount < countHits) 
        //            {
        //                maxChainCount = countHits;
        //            }
        //        }
        //        else if (bullet
        //    }
        //    if (
        //    return 0;
        //}

      /*  public void Draw()
        {
            foreach (Bullet bullet in m_Bullets)
            {
                bullet.Draw();
            }
        }*/
    }
}
