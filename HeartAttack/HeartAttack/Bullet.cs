using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HeartAttack
{
    public class Bullet
    {
        //TODO change sprite to some sort of particle shiz
        private Sprite m_Sprite;
        private int m_Power;

        public Bullet(Vector2 pPosition, Vector2 pVelocity, int pPower)
        {
            m_Power = pPower;
            m_Sprite = new Sprite(HeartAttack.theGameInstance.Content.Load<Texture2D>("bullet"), pPosition);
            m_Sprite.AddUpdater(new VelocityUpdater(pVelocity));
        }

        public Vector2 Position
        {
            get { return m_Sprite.Position; }
        }

        public bool IsDead
        {
            get;
            set;
        }

        public int Radius
        {
            get
            {
                // TODO: This shouldn't be hardcoded
                return 5;
            }
        }

        public void Update(GameTime pGameTime)
        {
            m_Sprite.Update(pGameTime);
        }

        public void Draw()
        {
            m_Sprite.Draw();
        }
    }
}
