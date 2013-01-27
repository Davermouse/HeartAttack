using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace HeartAttack.Scenes
{
    // Display game over message and score, press a to continue to start screen
    public class GameOverScene : Scene
    {
        public Texture2D m_GameOverTexture;

        public GameOverScene()
        {
            m_GameOverTexture = HeartAttack.theGameInstance.Content.Load<Texture2D>("gameoverPlaceholder");
        }

        public override Scene Update(Microsoft.Xna.Framework.GameTime pGameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.A == ButtonState.Pressed)
            {
                return new TitleScene();
            }

            return this;
        }

        public override void Draw(Microsoft.Xna.Framework.GameTime pGameTime)
        {
            var spriteBatch = HeartAttack.theGameInstance.spriteBatch;

            spriteBatch.Begin();
            HeartAttack.theGameInstance.spriteBatch.Draw(m_GameOverTexture, Vector2.Zero, Color.White);
            spriteBatch.End();
        }
    }
}
