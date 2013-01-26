using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace HeartAttack
{
    public class Ping
    {
        Sprite m_Sprite;

        public Ping()
        {
            Texture2D texture = HeartAttack.theGameInstance.Content.Load<Texture2D>("ping");
            m_Sprite = new Sprite(texture, 
                new Vector2((HeartAttack.theGameInstance.GraphicsDevice.Viewport.Width) / 2,
                (HeartAttack.theGameInstance.GraphicsDevice.Viewport.Height) / 2));
            m_Sprite.AddUpdater(new ScaleLerpUpdater(new Vector2(0, 0), new Vector2(5, 5), 1000));
        }

        public void Update(GameTime pGameTime)
        {
            m_Sprite.Update(pGameTime);
        }

        public void Draw()
        {
            m_Sprite.Draw();
        }

        public float GetPingRadius()
        {
            return (float)m_Sprite.Scale.X * m_Sprite.Texture.Width/2;
        }
    }
}
