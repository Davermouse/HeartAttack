using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace HeartAttack
{
    public class PowerBar
    {

        Color color;
        Texture2D texture;
        Rectangle position;

        int low;
        int high;
        int range;

        public PowerBar(Color inColor, Texture2D inTexture, Rectangle inRectangle, int inLow, int inHigh)
        {
            color = inColor;
            texture = inTexture;
            position = inRectangle;

            low = inLow;
            high = inHigh;
            range = high - low;
        }

        public void Draw(SpriteBatch spriteBatch, int value)
        {
            int width = (int)(((value - low) / (float)range) * position.Width);
            Rectangle drawRect = position;
            drawRect.Width = width;

            Rectangle sourceRect = new Rectangle(0, 0, width, position.Height);

            spriteBatch.Draw(texture, drawRect, sourceRect, color);
        }
    }
 }
