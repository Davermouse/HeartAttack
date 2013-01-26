﻿using System;
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

        public void Draw()
        {
            foreach (Bullet bullet in m_Bullets)
            {
                bullet.Draw();
            }
        }
    }
}
