using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using HeartAttack.Timing;

namespace HeartAttack
{
    public class Bug : Entity
    {
        private Sprite m_Sprite;
        private int m_Health;
        private int radius;

        public SoundEffect m_bugDeathSound;

        private bool runningAnimation = false;

        private MainGameScene scene;

        private List<Texture2D> frames;
        private Texture2D cross;

        public Bug(MainGameScene scene, Vector2 pPosition, int pHealth, float pSpeed) : base(scene)
        {
            this.scene = scene;
            m_Health = pHealth;
            radius = 15;

            frames = new List<Texture2D>();
            var content = HeartAttack.theGameInstance.Content;
            cross = content.Load<Texture2D>("cross");
            for (int i = 1 ; i <= 6 ; i++)
            {
                frames.Add(content.Load<Texture2D>("Bug/bug" + i));
            }
            m_bugDeathSound = HeartAttack.theGameInstance.Content.Load<SoundEffect>("bugDeath1");
            m_Sprite = new Sprite(frames[0], pPosition);
            m_Sprite.Scale = new Vector2(0.05f, 0.05f);
           // m_Sprite.Centre *= m_Sprite.Scale;
            m_Sprite.Colour = Color.Transparent;

            Vector2 velocity = (new Vector2(HeartAttack.theGameInstance.GraphicsDevice.Viewport.Width / 2,
                HeartAttack.theGameInstance.GraphicsDevice.Viewport.Height / 2) - pPosition);
            velocity.Normalize();
            velocity *= pSpeed;         

            m_Sprite.AddUpdater(new VelocityUpdater(velocity));
        }

        public Color Colour
        {
            get;
            private set;
        }

        public override void Update(GameTime pGameTime)
        {
            if (!runningAnimation)
            {
                if (new Random().NextDouble() > 0.5)
                {
                    runningAnimation = true;

                    var animation = new SpriteAnimation(Scene.ClockManager,
                        this.frames,
                        this.frames[0],
                        this.m_Sprite,
                        0.05f);

                    animation.Complete += (s, e) => this.runningAnimation = false;

                    animation.Start();
                }
            }

            m_Sprite.Update(pGameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            m_Sprite.Draw();
          //  spriteBatch.Draw(cross, m_Sprite.Position - new Vector2(cross.Width / 2, cross.Height / 2), Color.White);
        }

        public bool CollidesWith(Ping pPing)
        {
            float distanceFromCentre = (m_Sprite.Position - DirtyGlobalHelpers.CentreOfScreen()).Length() - pPing.GetPingRadius();
            return (distanceFromCentre > -5 && distanceFromCentre < 5);
        }

        public bool CollidesWith(Bullet pBullet)
        {
            if ((m_Sprite.Position - pBullet.Position).Length() <
                pBullet.Radius + radius)
            {
                m_bugDeathSound.Play();
                HeartAttack.theGameInstance.bugsKilled++;
                return true;
            }
            return false;
        }

        public bool CollidesWith(PlayerThing pPlayer)
        {
            return (m_Sprite.Position - pPlayer.Position).Length() <
                this.radius + pPlayer.Radius;
        }

        public void HitByBullet(Bullet pBullet)
        {
            handleDeath();
        }

        public void HitPlayer(PlayerThing player)
        {
            handleDeath();
        }

        private void handleDeath()
        {
            this.IgnoresCollisions = true;

            var deathLength = 0.2f;
            m_Sprite.Colour = Color.White;
            m_Sprite.AddUpdater(new ScaleLerpUpdater(new Vector2(this.m_Sprite.Scale.X), new Vector2(0), (int)(deathLength * 100)));
            
            new Timing.Timer(scene.ClockManager, deathLength, true, (e, s) => {
                this.IsDead = true;
            });
        }

        public void ShowBug()
        {
            Color hidden = Color.Transparent;

            Color shown = Color.White;

            m_Sprite.AddUpdater(new BugPingUpdater(hidden, shown, 500));
        }
    }
}
