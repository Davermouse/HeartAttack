﻿using System;
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
        private int m_Health = 100;
        private int m_BulletPower = 10;
        private int m_BulletSpeed = 150;

        private int m_RestingHeartRate;
        private int m_HeartRateModifier;

        private GameTime startGametime;

        private Texture2D normalHeart;
        private Texture2D cross;
        private List<Texture2D> lFrames;
        private List<Texture2D> sFrames;
        public SoundEffect m_FireSound;

        public PlayerThing(MainGameScene scene, int restingHeartRate) : base(scene)
        {
            m_RestingHeartRate = restingHeartRate;

            var content = HeartAttack.theGameInstance.Content;

            normalHeart = content.Load<Texture2D>("Heart/Heart");
            cross = content.Load<Texture2D>("cross");

            lFrames = new List<Texture2D>()
            {
                content.Load<Texture2D>("Heart/Heartl1"),
                content.Load<Texture2D>("Heart/Heartl2"),
                content.Load<Texture2D>("Heart/Heartl3"),
              //  content.Load<Texture2D>("Heart/Heartl4")
            };

            sFrames = new List<Texture2D>()
            {
                content.Load<Texture2D>("Heart/Hearts1"),
                content.Load<Texture2D>("Heart/Hearts2"),
                content.Load<Texture2D>("Heart/Hearts3"),
            //    content.Load<Texture2D>("Heart/Hearts4")
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
            if (startGametime == null) startGametime = pGameTime;

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

            Vector2 v = InputManager.LeftThumbStick;

            float length = v.Length();
            if (length < 1.1 && length > 0.9)
            {
                m_Sprite.Allign(v);
            }

            this.m_HeartRateModifier = (int)((pGameTime.TotalGameTime.TotalSeconds - startGametime.TotalGameTime.TotalSeconds) / 4);

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
                m_Sprite.Texture = sFrames[0]; // HACK
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
                m_Sprite.Texture = lFrames[0]; // HACK
            }
            else
            {
                m_Sprite.Texture = normalHeart;
            }
        }

        public bool IsStressed()
        {
            int currentHeartRate = 80;
            
            int difference = currentHeartRate + m_HeartRateModifier - m_RestingHeartRate;
            if (difference > 0 && difference > DirtyGlobalHelpers.STRESS_THRESHHOLD)
            {
                return true;
            }
            return false;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            m_Sprite.Draw();
           // spriteBatch.Draw(cross, this.Position - new Vector2(cross.Width/2, cross.Height/2), Color.White);
        }

        public void HitByBug(Bug bug)
        {
            this.m_Health -= 10;
        }

        private Vector2 GetGunPosition()
        {
            Vector2 direction = GetGunDirection();
            direction.Normalize();
            Vector2 dimensions = new Vector2(m_Sprite.Texture.Width, m_Sprite.Texture.Height);
            dimensions *= m_Sprite.Scale;
            float length = dimensions.Length();
            length *= 0.25f;

            direction *= length;
            Vector2 gunPosition = this.Position + direction;
            return gunPosition;
        }

        private Vector2 GetGunDirection()
        {
            var rotation = m_Sprite.Rotation + Math.PI / 16;
            return new Vector2((float)Math.Sin(rotation), -(float)Math.Cos(rotation));
        }

        public Bullet FireBullet()
        {
            if (m_TimeToNextBullet <= 0)
            {
                m_TimeToNextBullet = DirtyGlobalHelpers.config.BulletDelay;
                m_FireSound.Play();
                HeartAttack.theGameInstance.shotsFired++;
                return new Bullet(Scene, GetGunPosition(), GetGunDirection() * DirtyGlobalHelpers.config.BulletSpeed, m_BulletPower);
            }
            return null;
        }
    }
}
