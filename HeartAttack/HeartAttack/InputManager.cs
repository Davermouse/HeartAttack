using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace HeartAttack
{
    public class InputManager
    {
        private static GamePadState oldGame;
        private static KeyboardState oldKeys;

        private static Vector2 leftThumbStick;
        private static float angle;
        private static float speed = 0.1f;
        private static DateTime lastTime = DateTime.Now;

        static void updateKeybardThumb()
        {
            DateTime now = DateTime.Now;
            TimeSpan interval = now - lastTime;
            lastTime = now;

            if (interval.TotalSeconds > 0.5)
                return;

            KeyboardState k = Keyboard.GetState();

            if (k.IsKeyDown(Keys.Left)) angle += speed;
            if (k.IsKeyDown(Keys.Right)) angle -= speed;
            leftThumbStick = new Vector2((float)Math.Sin(angle), -(float)Math.Cos(angle));
        }

        public static Vector2 LeftThumbStick
        {
            get
            {
                updateKeybardThumb();
                GamePadState g = GamePad.GetState(PlayerIndex.One);
                oldGame = g;
                if (g.IsConnected) 
                    return g.ThumbSticks.Left;
                else

                    return leftThumbStick;
            }
        }

        public static bool ButtonADown
        {
            get
            {
                updateKeybardThumb();
                GamePadState g = GamePad.GetState(PlayerIndex.One);
                oldGame = g;
                if (g.IsConnected)
                    return g.Buttons.A == ButtonState.Pressed;
                else
                {
                    return Keyboard.GetState().IsKeyDown(Keys.A);
                }
            }
        }

        public static bool ButtonAPressed
        {
            get
            {
                updateKeybardThumb();
                GamePadState g = GamePad.GetState(PlayerIndex.One);
                KeyboardState k = Keyboard.GetState();
                bool result = false;
                if (g.IsConnected)
                {
                    result = 
                        (g.Buttons.A == ButtonState.Pressed) != 
                        (oldGame.Buttons.A == ButtonState.Pressed);
                }
                else
                {
                    result = k.IsKeyDown(Keys.A) && oldKeys.IsKeyUp(Keys.A);
                }

                return result;
            }
        }

        public static bool UpPressed
        {
            get
            {
                updateKeybardThumb();
                GamePadState g = GamePad.GetState(PlayerIndex.One);
                KeyboardState k = Keyboard.GetState();
                bool result = false;
                if (g.IsConnected)
                {
                    result = 
                        (g.DPad.Up == ButtonState.Pressed) != 
                        (oldGame.DPad.Up == ButtonState.Pressed);
                }
                else
                {
                    result = k.IsKeyDown(Keys.Up) && oldKeys.IsKeyUp(Keys.Up);
                }

                return result;
            }
        }

        public static bool DownPressed
        {
            get
            {
                updateKeybardThumb();
                GamePadState g = GamePad.GetState(PlayerIndex.One);
                KeyboardState k = Keyboard.GetState();
                bool result = false;
                if (g.IsConnected)
                {
                    result = (g.DPad.Down == ButtonState.Pressed) != (oldGame.DPad.Down == ButtonState.Pressed);
                }
                else
                {
                    result = k.IsKeyDown(Keys.Down) && oldKeys.IsKeyUp(Keys.Down);
                }

                return result;
            }
        }

        public static void Update()
        {
            oldGame = GamePad.GetState(PlayerIndex.One);
            oldKeys = Keyboard.GetState();
        }
    }
}
