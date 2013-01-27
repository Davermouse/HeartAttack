using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using HeartAttack.Timing;

namespace HeartAttack
{
    public class Ping : Entity
    {
        Sprite m_Sprite;

        public Ping(MainGameScene scene) : base(scene)
        {
            Texture2D texture = HeartAttack.theGameInstance.Content.Load<Texture2D>("ping");
            m_Sprite = new Sprite(texture, 
                new Vector2((HeartAttack.theGameInstance.GraphicsDevice.Viewport.Width) / 2,
                (HeartAttack.theGameInstance.GraphicsDevice.Viewport.Height) / 2));

            var lifeTime = 1;
            //m_Sprite.AddUpdater(new ScaleLerpUpdater(new Vector2(0, 0), new Vector2(5, 5), lifeTime * 1000));
            new Timer(scene.ClockManager, lifeTime, true, (s, e) => this.IsDead = true);
        }

        public override void Update(GameTime pGameTime)
        {
            m_Sprite.Update(pGameTime);

            foreach (var bug in Scene.Entities.OfType<Bug>())
            {
                if (bug.CollidesWith(this))
                {
                    bug.ShowBug();
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            m_Sprite.Draw();
        }

        public float GetPingRadius()
        {
            return (float)m_Sprite.Scale.X * m_Sprite.Texture.Width/2;
        }
    }
}
