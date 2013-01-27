using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HeartAttack.Scenes
{
    public class TitleScene : Scene
    {
        public Texture2D m_TitleTexture;

        public TitleScene()
        {
            m_TitleTexture = HeartAttack.theGameInstance.Content.Load<Texture2D>("titlePlaceholder");
        }

        public override Scene Update(Microsoft.Xna.Framework.GameTime pGameTime)
        {
            if (InputManager.ButtonAPressed)
            {
                if (HeartAttack.theGameInstance.Oximeter.IsConnected)
                {
                    return new CalmingConfigScene();
                }
                else
                {
                    return new MainGameScene(HeartAttack.theGameInstance.Oximeter.SimulatedHeartRate);
                }
            }

            return this;
        }

        public override void Draw(Microsoft.Xna.Framework.GameTime pGameTime)
        {
            var spriteBatch = HeartAttack.theGameInstance.spriteBatch;

            spriteBatch.Begin();
            HeartAttack.theGameInstance.spriteBatch.Draw(m_TitleTexture, Vector2.Zero, Color.White);
            spriteBatch.End();
        }
    }
}
