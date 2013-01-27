using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using HeartAttack.Timing;
using Microsoft.Xna.Framework.Graphics;
using HeartAttack.Scenes;

namespace HeartAttack
{
    public class MainGameScene : Scene
    {
        private PlayerThing m_Player;
        private BulletManager m_BulletManager;
        private BugManager m_BugManager;
        private NewPingManager m_PingManager;
        private Texture2D m_BackgroundTexture;
        private RenderTarget2D m_ShaderRenderTarget;
        private Texture2D m_ShaderTexture;
        private Effect m_Ripple;
        private EffectParameter m_PingLengthsParameter;

        public MainGameScene(int restingHeartRate)
        {
            this.Entities = new List<Entity>();
            ClockManager = new Timing.ClockManager();

            m_Player = new PlayerThing(this, restingHeartRate);
            m_BulletManager = new BulletManager();
            m_BugManager = new BugManager(this);
            // m_PingManager = new PingManager(this);
            m_PingManager = new NewPingManager(this);
            m_BackgroundTexture = HeartAttack.theGameInstance.Content.Load<Texture2D>("background");
            m_Ripple = HeartAttack.theGameInstance.Content.Load<Effect>("RippleShader");

            m_PingLengthsParameter = m_Ripple.Parameters["pingLengths"];
            m_Ripple.Parameters["Viewport"].SetValue(
                new Vector2(HeartAttack.theGameInstance.graphics.GraphicsDevice.Viewport.Width, 
                    HeartAttack.theGameInstance.graphics.GraphicsDevice.Viewport.Height));

            m_ShaderRenderTarget = new RenderTarget2D
                (HeartAttack.theGameInstance.graphics.GraphicsDevice,
                HeartAttack.theGameInstance.graphics.GraphicsDevice.PresentationParameters.BackBufferWidth,
                HeartAttack.theGameInstance.graphics.GraphicsDevice.PresentationParameters.BackBufferHeight);
            m_ShaderTexture = new Texture2D(
                HeartAttack.theGameInstance.graphics.GraphicsDevice,
                HeartAttack.theGameInstance.graphics.GraphicsDevice.PresentationParameters.BackBufferWidth,
                HeartAttack.theGameInstance.graphics.GraphicsDevice.PresentationParameters.BackBufferHeight, true,
                m_ShaderRenderTarget.Format
                );
        }

        public ClockManager ClockManager
        {
            get;
            private set;
        }

        public PlayerThing Player
        {
            get
            {
                return m_Player;
            }
        }

        public List<Entity> Entities
        {
            get;
            private set;
        }

        public IEnumerable<Entity> CollidingEntities
        {
            get
            {
                return Entities.Where(e => !e.IgnoresCollisions);
            }
        }

        public int Score
        {
            get;
            set;
        }

        public override Scene Update(GameTime pGameTime)
        {
            ClockManager.Update(pGameTime);

            foreach (var entity in Entities)
            {
                entity.Update(pGameTime);
            }
            m_Player.Update(pGameTime);
            Entities = Entities.Where(e => !e.IsDead).ToList();

            m_BugManager.Update(pGameTime);
            m_PingManager.Update(pGameTime);

            if (InputManager.ButtonAPressed)
            {
                var newBullet = m_Player.FireBullet();

                if (newBullet != null)
                    Entities.Add(newBullet);
            }

            m_PingLengthsParameter.SetValue(m_PingManager.Pings);

            if (m_Player.Health <= 0)
            {
                return new GameOverScene();
            }
            return this;
        }

        public override void Draw(GameTime pGameTime)
        {
            HeartAttack.theGameInstance.graphics.GraphicsDevice.SetRenderTarget(m_ShaderRenderTarget);

            var spriteBatch = HeartAttack.theGameInstance.spriteBatch;

            spriteBatch.Begin();

            spriteBatch.Draw(m_BackgroundTexture,
                new Rectangle(0, 0, HeartAttack.theGameInstance.GraphicsDevice.Viewport.Width,
                    HeartAttack.theGameInstance.GraphicsDevice.Viewport.Height), Color.White);

            foreach (var entity in Entities)
            {
                entity.Draw(spriteBatch);
            }
            m_Player.Draw(spriteBatch);

            spriteBatch.End();

            m_ShaderTexture = (Texture2D)m_ShaderRenderTarget;

            HeartAttack.theGameInstance.graphics.GraphicsDevice.SetRenderTarget(null);

            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Opaque, null, null, null, m_Ripple);
            spriteBatch.Draw(m_ShaderTexture,
                new Rectangle(0, 0,
                    HeartAttack.theGameInstance.graphics.GraphicsDevice.Viewport.Width,
                    HeartAttack.theGameInstance.graphics.GraphicsDevice.Viewport.Height), Color.White);
            spriteBatch.End();

            spriteBatch.Begin();
            DrawHUDStuff(spriteBatch);
            spriteBatch.End();
        }

        private void DrawHUDStuff(SpriteBatch spriteBatch)
        {
            var font = HeartAttack.theGameInstance.Font;
            var screenWidth = HeartAttack.theGameInstance.GraphicsDevice.Viewport.Width;

            var info =
                "Heart rate: " + HeartAttack.theGameInstance.Oximeter.HeartRate.ToString() +
                (HeartAttack.theGameInstance.Oximeter.HasHeartbeat ? "" : " Simulated");

            spriteBatch.DrawString(font, info, new Vector2(20, 20), Color.White);

            var health = "Health: " + Player.Health;
            var healthWidth = font.MeasureString(health);
            spriteBatch.DrawString(font, health, new Vector2(
                screenWidth - 20 - healthWidth.X, 20), Color.White);
            var bugsKilled = "Burgers binned: " + HeartAttack.theGameInstance.bugsKilled;
            var bugsKilledWidth = HeartAttack.theGameInstance.Font.MeasureString(bugsKilled);
            spriteBatch.DrawString(HeartAttack.theGameInstance.Font, bugsKilled, new Vector2(
            HeartAttack.theGameInstance.GraphicsDevice.Viewport.Width - 20 - bugsKilledWidth.X, 20 + 20), Color.White);
            int accuracyValue = 0;
            if (HeartAttack.theGameInstance.shotsFired > 0)
            {
                accuracyValue = (int)((float)HeartAttack.theGameInstance.bugsKilled / (float)HeartAttack.theGameInstance.shotsFired * 100);
            }
            var accuracy = "Accuracy: " + accuracyValue;
            var accuracyWidth = HeartAttack.theGameInstance.Font.MeasureString(accuracy);
            spriteBatch.DrawString(HeartAttack.theGameInstance.Font, accuracy, new Vector2(
                HeartAttack.theGameInstance.GraphicsDevice.Viewport.Width - 20 - accuracyWidth.X, 20 + 40), Color.White);
        }
    }
}
