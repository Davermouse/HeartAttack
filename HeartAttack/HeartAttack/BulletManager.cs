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
        public SoundEffect m_FireSound;
        private List<Bullet> m_Bullets;

        public BulletManager()
        {
            m_Bullets = new List<Bullet>();
            m_FireSound = HeartAttack.theGameInstance.Content.Load<SoundEffect>("fire");
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
                m_FireSound.Play();
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

      /*  public void Draw()
        {
            foreach (Bullet bullet in m_Bullets)
            {
                bullet.Draw();
            }
        }*/
    }
}
