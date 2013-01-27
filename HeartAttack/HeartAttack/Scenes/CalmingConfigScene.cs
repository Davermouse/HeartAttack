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

        public CalmingConfigScene()
        {
            m_ConfigTexture = HeartAttack.theGameInstance.Content.Load<Texture2D>("background");
        }

        public override Scene Update(Microsoft.Xna.Framework.GameTime pGameTime)
        {

            if (HeartAttack.theGameInstance.Oximeter.HasHeartbeat)
            {
                timeWithBeat += pGameTime.TotalGameTime.TotalSeconds;
            }
            else
            {
                timeWithBeat = 0;
            }

            return new MainGameScene();

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

            var beatText = oximeter.HasHeartbeat ?
                oximeter.HeartRate.ToString() :
                "--";
            var beatSize = bigFont.MeasureString(beatText);

            spriteBatch.DrawString(bigFont, beatText, new Vector2(screenCenter.X - beatSize.X / 2, screenCenter.Y - beatSize.Y / 2), Color.White);

            spriteBatch.End();
        }        
    }
}
