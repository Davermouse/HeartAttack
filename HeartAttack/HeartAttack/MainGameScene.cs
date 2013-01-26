using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using HeartAttack.Timing;

namespace HeartAttack
{
    public class MainGameScene : Scene
    {
        private PlayerThing m_Player;
        private BulletManager m_BulletManager;
        private BugManager m_BugManager;
        private PingManager m_PingManager;

        public MainGameScene()
        {
            this.Entities = new List<Entity>();
            ClockManager = new Timing.ClockManager();

            m_Player = new PlayerThing(this);
            m_BulletManager = new BulletManager();
            m_BugManager = new BugManager(this);
            m_PingManager = new PingManager(this);

          //  this.Entities.Add(m_Player);
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

        public override Scene Update(GameTime pGameTime)
        {
            ClockManager.Update(pGameTime);

            foreach (var entity in Entities)
            {
                entity.Update(pGameTime);
            }
            m_Player.Update(pGameTime);
            Entities = Entities.Where(e => !e.IsDead).ToList();

         //   m_BulletManager.Update(pGameTime);
            m_BugManager.Update(pGameTime);
            m_PingManager.Update(pGameTime);

            if (GamePad.GetState(PlayerIndex.One).Triggers.Right > 0.9)
            {
                var newBullet = m_Player.FireBullet();

                if (newBullet != null)
                    Entities.Add(newBullet);
            }

            return this;
        }

        public override void Draw(GameTime pGameTime)
        {
            var spriteBatch = HeartAttack.theGameInstance.spriteBatch;

            spriteBatch.Begin();

            foreach (var entity in Entities)
            {
                entity.Draw(spriteBatch);
            }
            m_Player.Draw(spriteBatch);
            var info =
                "Heart rate: " + HeartAttack.theGameInstance.Oximeter.HeartRate.ToString() +
                (HeartAttack.theGameInstance.Oximeter.IsConnected ? "" : " Simulated");

            spriteBatch.DrawString(HeartAttack.theGameInstance.Font, info, new Vector2(20, 20), Color.White);

            var score = "Health: " + Player.Health;
            var scoreWidth = HeartAttack.theGameInstance.Font.MeasureString(score);
            spriteBatch.DrawString(HeartAttack.theGameInstance.Font, score, new Vector2(
                HeartAttack.theGameInstance.GraphicsDevice.Viewport.Width - 20 - scoreWidth.X, 20), Color.White);
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
            spriteBatch.End();
        }
    }
}
