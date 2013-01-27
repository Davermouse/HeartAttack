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
        private double timeWithBeat;
        private States state;

        private bool heartCoreMode;

        private readonly int averagingTime = 10;
        private readonly int countDownLength = 3;

        enum States
        {
            WaitingForBeat,
            WaitingForBeatToAverage,
            Countdown
        }

        public CalmingConfigScene(bool heartCore)
        {
            this.heartCoreMode = heartCore;
            m_ConfigTexture = HeartAttack.theGameInstance.Content.Load<Texture2D>("background");
            state = States.WaitingForBeat;
        }

        public override Scene Update(Microsoft.Xna.Framework.GameTime pGameTime)
        {
            if (HeartAttack.theGameInstance.Oximeter.HasHeartbeat)
            {
                timeWithBeat += pGameTime.ElapsedGameTime.TotalSeconds;

                state = States.WaitingForBeatToAverage;
            }
            else
            {
                timeWithBeat = 0;

                state = States.WaitingForBeat;
            }

            if (timeWithBeat > averagingTime)
            {
                state = States.Countdown;
            }

            if (timeWithBeat > averagingTime + countDownLength)
            {
                return new MainGameScene(HeartAttack.theGameInstance.Oximeter.HeartRate, heartCoreMode);
            }

         //   return new MainGameScene();

            return this;
        }

        public override void Draw(Microsoft.Xna.Framework.GameTime pGameTime)
        {
            var spriteBatch = HeartAttack.theGameInstance.spriteBatch;
            var bigFont = HeartAttack.theGameInstance.BigFont;
            var font = HeartAttack.theGameInstance.Font;
            var oximeter = HeartAttack.theGameInstance.Oximeter;

            spriteBatch.Begin();
            HeartAttack.theGameInstance.spriteBatch.Draw(m_ConfigTexture, Vector2.Zero, Color.White);
            
            var screenCenter = DirtyGlobalHelpers.CentreOfScreen();

            var numberText = "";

            if (state == States.WaitingForBeat)
            {
                numberText = "--";
            }
            else if (state == States.WaitingForBeatToAverage)
            {
                numberText = oximeter.HeartRate.ToString();
            }
            else
            {
                numberText = (countDownLength + averagingTime - (timeWithBeat)).ToString("f0");
            }

            var beatSize = bigFont.MeasureString(numberText);

            spriteBatch.DrawString(bigFont, numberText, new Vector2(screenCenter.X - beatSize.X / 2, screenCenter.Y - beatSize.Y / 2), Color.White);

            var message = "";
            if (state == States.WaitingForBeat)
            {
                message = "Waiting for a heartbeat";
            }
            else if (state == States.WaitingForBeatToAverage)
            {
                message = "Averaging heartbeat";
            }
            else if (state == States.Countdown)
            {
                message = "Starting the game in";
            }

            var messageSize = font.MeasureString(message);

            spriteBatch.DrawString(font, message, new Vector2(screenCenter.X - messageSize.X / 2, 40), Color.White);

            spriteBatch.End();
        }        
    }
}
