using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace HeartAttack
{
    public class BulletManager
    {
        private List<Bullet> m_Bullets;

        public BulletManager()
        {
            m_Bullets = new List<Bullet>();
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
        }

        public void Draw()
        {
            foreach (Bullet bullet in m_Bullets)
            {
                bullet.Draw();
            }
        }
    }
}
