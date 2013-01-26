using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HeartAttack
{
    public class Bug
    {
        private Sprite m_Sprite;
        private int m_Health;
        private int radius;

        public Bug(Vector2 pPosition, int pHealth, float pSpeed)
        {
            m_Health = pHealth;
            radius = 12;
            m_Sprite = new Sprite(HeartAttack.theGameInstance.Content.Load<Texture2D>("bug"), pPosition);
            m_Sprite.Scale = new Vector2(0.1f,0.1f);
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

        public bool IsDead
        {
            get;
            private set;
        }

        public void Update(GameTime pGameTime)
        {
            m_Sprite.Update(pGameTime);
        }

        public void Draw()
        {
            m_Sprite.Draw();
        }

        public bool CollidesWith(Ping pPing)
        {
            float distanceFromCentre = (m_Sprite.Position + m_Sprite.Centre - DirtyGlobalHelpers.CentreOfScreen()).Length() - pPing.GetPingRadius();
            return (distanceFromCentre > -5 && distanceFromCentre < 5);
        }

        public bool CollidesWith(Bullet pBullet)
        {
            return (m_Sprite.Position - pBullet.Position).Length() <
                pBullet.Radius + radius;
        }

        public bool CollidesWith(PlayerThing pPlayer)
        {
            return (m_Sprite.Position - pPlayer.Position).Length() <
                this.radius + pPlayer.Radius;
        }

        public void HitByBullet(Bullet pBullet)
        {
            pBullet.IsDead = true;

            handleDeath();
        }

        public void HitPlayer(PlayerThing player)
        {
            handleDeath();
        }

        private void handleDeath()
        {
            m_Sprite.AddUpdater(new ScaleLerpUpdater(new Vector2(1), new Vector2(0), 20));


        }

        public void ShowBug()
        {
            Color hidden = Color.Transparent;

            Color shown = Color.White;

            m_Sprite.AddUpdater(new BugPingUpdater(hidden, shown, 500));
        }
    }
}
