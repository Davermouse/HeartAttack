﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;

namespace HeartAttack.Scenes
{
    public class TitleScene : Scene
    {
        private Texture2D m_TitleTexture, m_HeartcoreTexture, m_FlowTexture;
        private Rectangle m_HeartcoreRectangle, m_FlowRectangle;
        private bool m_HeartcoreSelected;

        private SoundEffectInstance bgm;

        private double screenDisplayTime;

        public TitleScene()
        {
            m_TitleTexture = HeartAttack.theGameInstance.Content.Load<Texture2D>("title");
            m_HeartcoreTexture = HeartAttack.theGameInstance.Content.Load<Texture2D>("heartcoreMode");
            m_FlowTexture = HeartAttack.theGameInstance.Content.Load<Texture2D>("flowMode");

            bgm = HeartAttack.theGameInstance.Content.Load<SoundEffect>("heartbackground").CreateInstance();
            bgm.Play();

            HeartAttack.theGameInstance.bugsKilled = 0;
            HeartAttack.theGameInstance.shotsFired = 0;
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
            if (screenDisplayTime == 0)
            {
                screenDisplayTime = pGameTime.TotalGameTime.TotalSeconds;
            }

            if (InputManager.DownPressed)
            {
                m_HeartcoreSelected = true;
            }

            if (InputManager.UpPressed)
            {
                m_HeartcoreSelected = false;
            }

            if (pGameTime.TotalGameTime.TotalSeconds - screenDisplayTime > 1 &&
                InputManager.ButtonAPressed)
            {
                bgm.Stop();
                if (HeartAttack.theGameInstance.Oximeter.IsConnected)
                {
                    return new CalmingConfigScene(m_HeartcoreSelected);
                }
                else
                {
                    return new MainGameScene(HeartAttack.theGameInstance.Oximeter.SimulatedHeartRate, m_HeartcoreSelected);
                }
            }
            return this;
        }

        public override void Draw(Microsoft.Xna.Framework.GameTime pGameTime)
        {
            var spriteBatch = HeartAttack.theGameInstance.spriteBatch;
            Color heartcoreColour, flowColour;
            if (m_HeartcoreSelected)
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
            var highscore = "Highscore: " + DirtyGlobalHelpers.highscore;
            var highscoreWidth = font.MeasureString(highscore);
        //    spriteBatch.DrawString(font, highscore, new Vector2(
          //      DirtyGlobalHelpers.CentreOfScreen().X - highscoreWidth.X, DirtyGlobalHelpers.CentreOfScreen().Y - highscoreWidth.Y), Color.White);
            spriteBatch.End();
            
        }
    }
}
