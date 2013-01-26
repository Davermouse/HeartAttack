using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace HeartAttack.Timing
{
    class SpriteAnimation : Clock
    {
        public SpriteAnimation(
            ClockManager manager, 
            List<Texture2D> frames,
            Texture2D defaultFrame,
            Sprite sprite, 
            float frameLength)
            : base(manager)
        {
            Frames = frames;
            Sprite = sprite;
            FrameLength = frameLength;
            DefaultFrame = defaultFrame;

            this.RepeatCount = frames.Count - 1;
        }

        public Texture2D DefaultFrame
        {
            get;
            private set;
        }

        public List<Texture2D> Frames
        {
            get;
            private set;
        }

        public Sprite Sprite
        {
            get;
            private set;
        }

        public double FrameLength
        {
            get { return base.Duration; }
            private set { base.Duration = value; }
        }

        public int CurrentFrame
        {
            get;
            private set;
        }

        protected override void OnBegin()
        {
            this.CurrentFrame = 0;
            this.updateFrame();

            base.OnBegin();
        }

        protected override void OnLooped()
        {
            this.CurrentFrame++;
            this.updateFrame();

            base.OnLooped();
        }

        protected override void OnCompleted()
        {
            this.Sprite.Texture = DefaultFrame;

            base.OnCompleted();
        }

        private void updateFrame()
        {
            this.Sprite.Texture = this.Frames[this.CurrentFrame];
        }
    }
}
