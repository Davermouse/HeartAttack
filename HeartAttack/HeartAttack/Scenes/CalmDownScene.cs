using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace HeartAttack.Scenes
{
    // challenge to drop your heartbeat by 5 bpm in 50 heartbeats
    // if you can you gain some health
    public class CalmDownScene : Scene
    {
        public Texture2D m_CalmDownTexture;

        public CalmDownScene()
        {
            m_CalmDownTexture = HeartAttack.theGameInstance.Content.Load<Texture2D>("calmdownPlaceholder");
        }

        public override Scene Update(Microsoft.Xna.Framework.GameTime pGameTime)
        {
            if (InputManager.ButtonAPressed)
            {
                return new CalmingConfigScene();
            }

            return this;
        }

        public override void Draw(Microsoft.Xna.Framework.GameTime pGameTime)
        {
            var spriteBatch = HeartAttack.theGameInstance.spriteBatch;
            var font = HeartAttack.theGameInstance.Font;

            spriteBatch.Begin();

            HeartAttack.theGameInstance.spriteBatch.Draw(m_CalmDownTexture, Vector2.Zero, Color.White);
            spriteBatch.End();
        }
    }
}
