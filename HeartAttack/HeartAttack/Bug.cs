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

        public Bug(Vector2 pPosition, int pHealth, float pSpeed)
        {
            m_Health = pHealth;
            m_Sprite = new Sprite(HeartAttack.theGameInstance.Content.Load<Texture2D>("bug"), pPosition);
            Vector2 velocity = (new Vector2(HeartAttack.theGameInstance.GraphicsDevice.Viewport.Width / 2,
                HeartAttack.theGameInstance.GraphicsDevice.Viewport.Height / 2) - pPosition);
            velocity.Normalize();
            velocity *= pSpeed;         

            m_Sprite.AddUpdater(new VelocityUpdater(velocity));
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
            return false;
        }

        public bool CollidesWith(PlayerThing pPlayer)
        {
            return false;
        }

        public void ShowBug()
        {
            Color hidden = Color.CornflowerBlue;
            Color shown = Color.Red;
            m_Sprite.AddUpdater(new BugPingUpdater(hidden, shown, 500));
        }
    }
}
