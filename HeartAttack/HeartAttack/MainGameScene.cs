using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

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
            m_Player = new PlayerThing();
            m_BulletManager = new BulletManager();
            m_BugManager = new BugManager();
            m_PingManager = new PingManager();
        }

        public override Scene Update(GameTime pGameTime)
        {
            m_Player.Update(pGameTime);
            m_BulletManager.Update(pGameTime);
            m_BugManager.Update(pGameTime);
            m_PingManager.Update(pGameTime);

            //TODO test collisions - bugs and player - bugs and bullets
            m_BugManager.TestCollisions(m_Player);
            m_BugManager.TestCollisions(m_BulletManager);
            m_BugManager.TestCollisions(m_PingManager);

            if (GamePad.GetState(PlayerIndex.One).Triggers.Right > 0.9)
            {
                m_BulletManager.AddBullet(m_Player.FireBullet());
            }

            return this;
        }

        public override void Draw(GameTime pGameTime)
        {
            HeartAttack.theGameInstance.spriteBatch.Begin();
            m_PingManager.Draw();
            m_BugManager.Draw();
            m_Player.Draw();
            m_BulletManager.Draw();

            var info =
                "Heart rate: " + HeartAttack.theGameInstance.Oximeter.HeartRate.ToString() +
                (HeartAttack.theGameInstance.Oximeter.IsConnected ? "" : " Simulated");

            HeartAttack.theGameInstance.spriteBatch.DrawString(HeartAttack.theGameInstance.Font, info, new Vector2(20, 20), Color.White);

            HeartAttack.theGameInstance.spriteBatch.End();
        }
    }
}
