using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;

namespace HeartAttack
{
    public class PlayerThing : Entity
    {
        private Sprite m_Sprite;

        private int m_TimeToNextBullet; // in Milliseconds

        //TODO add ability to upgrade this stuff
        private int m_BulletDelay = 500;
        private int m_Health;
        private int m_BulletPower = 10;
        private int m_BulletSpeed = 100;

        public PlayerThing(MainGameScene scene) : base(scene)
        {
            Texture2D texture = HeartAttack.theGameInstance.Content.Load<Texture2D>("playerThing");
            m_Sprite = new Sprite(texture,
                DirtyGlobalHelpers.CentreOfScreen());

            this.m_Health = 100;
            this.Radius = 15;
        }

        public int Radius
        {
            get;
            private set;
        }

        public Vector2 Position
        {
            get { return m_Sprite.Position; }
        }

        public override void Update(GameTime pGameTime)
        {
            if (m_TimeToNextBullet > 0)
            {
                m_TimeToNextBullet -= pGameTime.ElapsedGameTime.Milliseconds;
            }

            float length = GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.Length();
            if (length < 1.1 && length > 0.9)
            {
                m_Sprite.Allign(GamePad.GetState(PlayerIndex.One).ThumbSticks.Left);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            m_Sprite.Draw();
        }

        public void HitByBug(Bug bug)
        {
            this.m_Health -= 1;
        }

        private Vector2 GetGunPosition()
        {
            return m_Sprite.Position + GetGunDirection() * m_Sprite.Texture.Height/2;
        }

        private Vector2 GetGunDirection()
        {
            return new Vector2((float)Math.Sin(m_Sprite.Rotation), -(float)Math.Cos(m_Sprite.Rotation));
        }

        public Bullet FireBullet()
        {
            if (m_TimeToNextBullet <= 0)
            {
                m_TimeToNextBullet = m_BulletDelay;
                return new Bullet(Scene, GetGunPosition(), GetGunDirection() * m_BulletSpeed, m_BulletPower);
            }
            return null;
        }
    }
}
