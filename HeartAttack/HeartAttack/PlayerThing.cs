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

        private Texture2D normalHeart;
        private List<Texture2D> lFrames;
        private List<Texture2D> sFrames;

        public PlayerThing(MainGameScene scene) : base(scene)
        {
            var content = HeartAttack.theGameInstance.Content;

            normalHeart = content.Load<Texture2D>("Heart/Heart");

            lFrames = new List<Texture2D>()
            {
                content.Load<Texture2D>("Heart/Heartl1"),
                content.Load<Texture2D>("Heart/Heartl2"),
                content.Load<Texture2D>("Heart/Heartl3"),
                content.Load<Texture2D>("Heart/Heartl4")
            };

            sFrames = new List<Texture2D>()
            {
                content.Load<Texture2D>("Heart/Hearts1"),
                content.Load<Texture2D>("Heart/Hearts2"),
                content.Load<Texture2D>("Heart/Hearts3"),
                content.Load<Texture2D>("Heart/Hearts4")
            };

            m_Sprite = new Sprite(normalHeart,
                DirtyGlobalHelpers.CentreOfScreen());

            this.m_Health = 100;
            this.Radius = 45;
        }

        public int Radius
        {
            get;
            private set;
        }

        public int Health
        {
            get
            {
                return m_Health;
            }
        }

        public double LastBeatTime
        {
            get;
            set;
        }

        public Vector2 Position
        {
            get { return m_Sprite.Position; }
        }

        public override void Update(GameTime pGameTime)
        {
            foreach (var bug in Scene.CollidingEntities.OfType<Bug>())
            {
                if (bug.CollidesWith(this))
                {
                    bug.HitPlayer(this);
                    this.HitByBug(bug);
                }
            }

            if (m_TimeToNextBullet > 0)
            {
                m_TimeToNextBullet -= pGameTime.ElapsedGameTime.Milliseconds;
            }

            float length = GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.Length();
            if (length < 1.1 && length > 0.9)
            {
                m_Sprite.Allign(GamePad.GetState(PlayerIndex.One).ThumbSticks.Left);
            }

            var timeSinceBeat = pGameTime.TotalGameTime.TotalSeconds - LastBeatTime;
            if (timeSinceBeat < 0.1)
            {
                m_Sprite.Texture = lFrames[0];
            }
            else if (timeSinceBeat < 0.2)
            {
                m_Sprite.Texture = lFrames[1];
            }
            else if (timeSinceBeat < 0.3)
            {
                m_Sprite.Texture = lFrames[2];
            }
            else if (timeSinceBeat < 0.4)
            {
                m_Sprite.Texture = lFrames[3];
            }
            else
            {
                m_Sprite.Texture = normalHeart;
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
