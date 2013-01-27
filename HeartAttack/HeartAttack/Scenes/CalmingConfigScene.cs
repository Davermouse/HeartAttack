using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace HeartAttack.Scenes
{
    // Get a stable reading from the heart monitor before we start,
    // then give a countdown
    public class CalmingConfigScene : Scene
    {
        public Texture2D m_ConfigTexture;

        public CalmingConfigScene()
        {
            m_ConfigTexture = HeartAttack.theGameInstance.Content.Load<Texture2D>("configPlaceholder");
        }

        public override Scene Update(Microsoft.Xna.Framework.GameTime pGameTime)
        {

            return new MainGameScene();

            return this;
        }

        public override void Draw(Microsoft.Xna.Framework.GameTime pGameTime)
        {
            var spriteBatch = HeartAttack.theGameInstance.spriteBatch;

            spriteBatch.Begin();
            HeartAttack.theGameInstance.spriteBatch.Draw(m_ConfigTexture, Vector2.Zero, Color.White);
            spriteBatch.End();
        }        
    }
}
