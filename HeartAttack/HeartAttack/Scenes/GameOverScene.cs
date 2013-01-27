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
            if (HeartAttack.theGameInstance.bugsKilled > DirtyGlobalHelpers.highscore)
            {
                DirtyGlobalHelpers.highscore = HeartAttack.theGameInstance.bugsKilled;
            }
            m_GameOverTexture = HeartAttack.theGameInstance.Content.Load<Texture2D>("gameover");
        }

        public override Scene Update(Microsoft.Xna.Framework.GameTime pGameTime)
        {
            if (InputManager.ButtonAPressed)
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
