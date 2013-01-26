using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace HeartAttack
{
    public class Sprite
    {
        public Texture2D Texture { get; set; }
        public float Rotation {get; set; }
        public Vector2 Centre { get; set; }
        public Vector2 Position { get; set; }
        public Color Colour { get; set; }
        public Vector2 Scale { get; set; }
        public SpriteEffects Effects { get; set; }
        public float Layer { get; set; }

        private List<SpriteUpdater> m_Updaters = new List<SpriteUpdater>();

        public Sprite(Texture2D pTexture, float pRotation,
            Vector2 pPosition, Vector2 pCentre, Color pColour,
            Vector2 pScale, SpriteEffects pEffects, float pLayer)
        {
            Texture = pTexture;
            Rotation = pRotation;
            Centre = pCentre;
            Position = pPosition;
            Colour = pColour;
            Scale = pScale;
            Effects = pEffects;
            Layer = pLayer;
        }

        public Sprite(Texture2D pTexture)
            :this(pTexture, 0, new Vector2((HeartAttack.theGameInstance.GraphicsDevice.Viewport.Width - pTexture.Width) / 2,
                (HeartAttack.theGameInstance.GraphicsDevice.Viewport.Height - pTexture.Height) / 2),
            new Vector2(pTexture.Width / 2, pTexture.Height / 2),
            Color.White, new Vector2(1,1), SpriteEffects.None, 1)
        {}

        public Sprite(Texture2D pTexture, Vector2 pPosition)
            : this(pTexture, 0, pPosition,
                new Vector2(pTexture.Width / 2, pTexture.Height / 2),
                Color.White, new Vector2(1, 1), SpriteEffects.None, 1)
        {}

        public void Draw()
        {
            HeartAttack.theGameInstance.spriteBatch.Draw(Texture, Position, null, Colour, Rotation, Centre, Scale, Effects, Layer);
        }

        public void Allign(Vector2 pV)
        {
            pV.Normalize();
            Rotation = (float)Math.Atan2(pV.X, pV.Y);
        }

        public void Update(GameTime pGameTime)
        {
            // TODO Is there a bug here as I am iterating through a list and removing at the same time?
            // if so does it matter?
            for(int i = 0; i < m_Updaters.Count; ++i)
            {
                m_Updaters[i].Update(pGameTime, this);
                if(m_Updaters[i].IsFinished())
                {
                    m_Updaters.RemoveAt(i);
                    --i; // this might sort that potential bug
                }
            }
        }

        public void AddUpdater(SpriteUpdater pUpdater)
        {
            m_Updaters.Add(pUpdater);
        }
    }
}
