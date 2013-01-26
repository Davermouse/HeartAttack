using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HeartAttack
{
    public class Bullet : Entity
    {
        //TODO change sprite to some sort of particle shiz
        private Sprite m_Sprite;
        private int m_Power;

        public Bullet(MainGameScene scene, Vector2 pPosition, Vector2 pVelocity, int pPower) : base(scene)
        {
            m_Power = pPower;
            m_Sprite = new Sprite(HeartAttack.theGameInstance.Content.Load<Texture2D>("bullet1"), pPosition);
            m_Sprite.AddUpdater(new VelocityUpdater(pVelocity));
            m_Sprite.Scale = new Vector2(0.05f);
            m_Sprite.Centre *= m_Sprite.Scale;
        }

        public Vector2 Position
        {
            get { return m_Sprite.Position; }
        }

        public int Radius
        {
            get
            {
                // TODO: This shouldn't be hardcoded
                return 5;
            }
        }

        public override void Update(GameTime pGameTime)
        {
            m_Sprite.Update(pGameTime);

            foreach (var bug in Scene.Entities.OfType<Bug>())
            {
                if (bug.CollidesWith(this))
                {
                    bug.HitByBullet(this);
                    this.IsDead = true;
                    break;
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            m_Sprite.Draw();
        }
    }
}
