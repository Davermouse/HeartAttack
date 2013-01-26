using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
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
        private int m_BulletSpeed = 150;

        private Texture2D normalHeart;
        private Texture2D cross;
        private List<Texture2D> lFrames;
        private List<Texture2D> sFrames;
        public SoundEffect m_FireSound;

        public PlayerThing(MainGameScene scene) : base(scene)
        {
            var content = HeartAttack.theGameInstance.Content;

            normalHeart = content.Load<Texture2D>("Heart/Heart");
            cross = content.Load<Texture2D>("cross");

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
            m_Sprite.Scale = new Vector2(0.5f, 0.5f);
            m_FireSound = HeartAttack.theGameInstance.Content.Load<SoundEffect>("fire");
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

            // TODO: Tidy this up.
            if (timeSinceBeat < 0.075)
            {
                m_Sprite.Texture = sFrames[0];
            }
            else if (timeSinceBeat < 0.15)
            {
                m_Sprite.Texture = sFrames[1];
            }
            else if (timeSinceBeat < 0.225)
            {
                m_Sprite.Texture = sFrames[2];
            }
            else if (timeSinceBeat < 0.3)
            {
                m_Sprite.Texture = sFrames[3];
            } 
            else if (timeSinceBeat < 0.375)
            {
                m_Sprite.Texture = lFrames[0];
            }
            else if (timeSinceBeat < 0.45)
            {
                m_Sprite.Texture = lFrames[1];
            }
            else if (timeSinceBeat < 0.525)
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
           // spriteBatch.Draw(cross, this.Position - new Vector2(cross.Width/2, cross.Height/2), Color.White);
        }

        public void HitByBug(Bug bug)
        {
            this.m_Health -= 1;
        }

        private Vector2 GetGunPosition()
        {
            return this.Position;
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
                m_FireSound.Play();
                HeartAttack.theGameInstance.shotsFired++;
                return new Bullet(Scene, GetGunPosition(), GetGunDirection() * m_BulletSpeed, m_BulletPower);

               
            }
            return null;
        }
    }
}
