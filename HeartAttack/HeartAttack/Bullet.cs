using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using HeartAttack.Timing;

namespace HeartAttack
{
    public class Bullet : Entity
    {
        //TODO change sprite to some sort of particle shiz
        private Sprite m_Sprite;
        private int m_Power;
        private Texture2D cross;
        private List<Texture2D> frames = new List<Texture2D>();

        public Bullet(MainGameScene scene, Vector2 pPosition, Vector2 pVelocity, int pPower)
            : base(scene)
        {
            m_Power = pPower;
            IsMiss = false;
            CanDelete = false;
            frames = new List<Texture2D>();
            var content = HeartAttack.theGameInstance.Content;
            cross = content.Load<Texture2D>("cross");
            for (int i = 1; i <= 4; i++)
            {
                frames.Add(content.Load<Texture2D>("Bullet/bullet" + i));
            }

            m_Sprite = new Sprite(frames[0], pPosition);
            m_Sprite.AddUpdater(new VelocityUpdater(pVelocity));
            m_Sprite.Scale = new Vector2(0.15f);
            //m_Sprite.Centre *= m_Sprite.Scale;
            var anim = new SpriteAnimation(Scene.ClockManager,
                    frames,
                    frames[0],
                    this.m_Sprite,
                    0.05f);

            anim.Start();

            anim.Complete += (s, e) =>
                {
                    anim.Reset();
                    anim.Start();
                };
        }

        public Vector2 Position
        {
            get { return m_Sprite.Position; }
        }

        public bool IsMiss
        {
            get;
            set;
        }
        public bool CanDelete
        {
            get;
            set;
        }

        public int Radius
        {
            get
            {
                // TODO: This shouldn't be hardcoded
                return 15;
            }
        }

        public void Kill()
        {
            this.IsDead = true;
        }

        public override void Update(GameTime pGameTime)
        {
            m_Sprite.Update(pGameTime);

            foreach (var bug in Scene.Entities.OfType<Bug>())
            {
                if (bug.CollidesWith(this))
                {
                    bug.HitByBullet(this);
                    this.IsDead = true;
                    break;
                }
            }
            LeftScreen();
        }

        public void LeftScreen()
        {
            Vector2 screenCentre = DirtyGlobalHelpers.CentreOfScreen();
            float screenRadius = screenCentre.Length();
            Vector2 relativePosition = Position - screenCentre;
            float distance = relativePosition.Length();


            if (distance > screenRadius)
            {
                IsMiss = true;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            m_Sprite.Draw();
          //  spriteBatch.Draw(cross, m_Sprite.Position - new Vector2(cross.Width / 2, cross.Height / 2), Color.White);
        }
    }
}
