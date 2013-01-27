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
        private Texture2D m_TitleTexture, m_HeartcoreTexture, m_FlowTexture;
        private Rectangle m_HeartcoreRectangle, m_FlowRectangle;
        private bool m_HeartcoreSelected;

        public TitleScene()
        {
            m_TitleTexture = HeartAttack.theGameInstance.Content.Load<Texture2D>("title");
            m_HeartcoreTexture = HeartAttack.theGameInstance.Content.Load<Texture2D>("heartcoreMode");
            m_FlowTexture = HeartAttack.theGameInstance.Content.Load<Texture2D>("flowMode");

            int space = 20;

            m_FlowRectangle = new Rectangle(
                HeartAttack.theGameInstance.graphics.GraphicsDevice.Viewport.Width -
            m_FlowTexture.Width - space,
            HeartAttack.theGameInstance.graphics.GraphicsDevice.Viewport.Height -
            m_FlowTexture.Height - 2 * space - m_HeartcoreTexture.Height, m_FlowTexture.Width, m_FlowTexture.Height);


            m_HeartcoreRectangle = new Rectangle(
                HeartAttack.theGameInstance.graphics.GraphicsDevice.Viewport.Width -
            m_HeartcoreTexture.Width - space,
            HeartAttack.theGameInstance.graphics.GraphicsDevice.Viewport.Height -
            m_HeartcoreTexture.Height - space, m_HeartcoreTexture.Width, m_HeartcoreTexture.Height);
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
            Color heartcoreColour, flowColour;
            if(m_HeartcoreSelected)
            {
                heartcoreColour = Color.Red;
                flowColour = Color.DimGray;
            }
            else
            {
                flowColour = Color.Red;
                heartcoreColour = Color.DimGray;
            }
            var font = HeartAttack.theGameInstance.Font;
            spriteBatch.Begin();
            HeartAttack.theGameInstance.spriteBatch.Draw(m_TitleTexture, Vector2.Zero, Color.White);
            HeartAttack.theGameInstance.spriteBatch.Draw(m_FlowTexture, m_FlowRectangle, flowColour);
            HeartAttack.theGameInstance.spriteBatch.Draw(m_HeartcoreTexture, m_HeartcoreRectangle, heartcoreColour);
            var highscore = "Highscore: " + HeartAttack.theGameInstance.highscore;
            var highscoreWidth = font.MeasureString(highscore);
            spriteBatch.DrawString(font, highscore, new Vector2(
                DirtyGlobalHelpers.CentreOfScreen().X - highscoreWidth.X, DirtyGlobalHelpers.CentreOfScreen().Y - highscoreWidth.Y), Color.White);
            spriteBatch.End();
            
        }
    }
}
