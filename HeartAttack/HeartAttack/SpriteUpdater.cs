using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace HeartAttack
{
    public abstract class SpriteUpdater
    {
        public virtual bool IsFinished() { return false; }
        public abstract void Update(GameTime pGameTime, Sprite pSprite);
    }

    public class VelocityUpdater : SpriteUpdater
    {
        public VelocityUpdater(Vector2 pVelocity)
        {
            Velocity = pVelocity;
        }

        public Vector2 Velocity { get; set; }

        public override void Update(GameTime pGameTime, Sprite pSprite)
        {
            pSprite.Position = pSprite.Position + Velocity * (float)pGameTime.ElapsedGameTime.Milliseconds / 1000;
        }
    }

    public class ColourLerpUpdater : SpriteUpdater
    {
        private Color m_StartColour, m_EndColour;
        private int m_LerpTime, m_CurrentTime;

        public ColourLerpUpdater(Color pStartColour, Color pEndColour, int pLerpTime)
        {
            m_StartColour = pStartColour;
            m_EndColour = pEndColour;
            m_CurrentTime = 0;
            m_LerpTime = pLerpTime;
        }

        public override bool IsFinished()
        {
            return m_CurrentTime > m_LerpTime;
        }

        public override void Update(GameTime pGameTime, Sprite pSprite)
        {
            m_CurrentTime += pGameTime.ElapsedGameTime.Milliseconds;
            float lerpFactor = 0;
            if (m_CurrentTime > 0)
            {
                lerpFactor = (float)m_CurrentTime / m_LerpTime;
            }
            pSprite.Colour = Color.Lerp(m_StartColour, m_EndColour, lerpFactor);
        }
    }

    public class BugPingUpdater : SpriteUpdater
    {
        private Color m_StartColour, m_PeakColour;
        private int m_LerpTime, m_CurrentTime;

        public BugPingUpdater(Color pStartColour, Color pPeakColour, int pLerpTime)
        {
            m_StartColour = pStartColour;
            m_PeakColour = pPeakColour;
            m_CurrentTime = 0;
            m_LerpTime = pLerpTime;
        }

        public override bool IsFinished()
        {
            return m_CurrentTime > m_LerpTime;
        }

        public override void Update(GameTime pGameTime, Sprite pSprite)
        {
            m_CurrentTime += pGameTime.ElapsedGameTime.Milliseconds;
            float lerpFactor = 0;
            if (m_CurrentTime > 0)
            {
                lerpFactor = 2 * (float)m_CurrentTime / m_LerpTime;
                if (lerpFactor > 1)
                {
                    lerpFactor = 2 - lerpFactor;
                }
            }
            pSprite.Colour = Color.Lerp(m_StartColour, m_PeakColour, lerpFactor);
        }
    }

    public class ScaleLerpUpdater : SpriteUpdater
    {
        private Vector2 m_StartScale, m_EndScale;
        private int m_LerpTime, m_CurrentTime;

        public ScaleLerpUpdater(Vector2 pStartScale, Vector2 pEndScale, int pLerpTime)
        {
            m_StartScale = pStartScale;
            m_EndScale = pEndScale;
            m_CurrentTime = 0;
            m_LerpTime = pLerpTime;
        }

        public override bool IsFinished()
        {
            return m_CurrentTime > m_LerpTime;
        }

        public override void Update(GameTime pGameTime, Sprite pSprite)
        {
            m_CurrentTime += pGameTime.ElapsedGameTime.Milliseconds;
            float lerpFactor = 0;
            if (m_CurrentTime > 0)
            {
                lerpFactor = (float)m_CurrentTime / m_LerpTime;
            }
            pSprite.Scale = Vector2.Lerp(m_StartScale, m_EndScale, lerpFactor);
        }
    }
}
