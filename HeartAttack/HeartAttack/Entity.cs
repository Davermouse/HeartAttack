using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HeartAttack
{
    public abstract class Entity
    {
        public Entity(MainGameScene scene)
        {
            this.Scene = scene;
        }

        public MainGameScene Scene
        {
            get;
            protected set;
        }

        public bool IsDead
        {
            get;
            protected set;
        }

        public abstract void Update(GameTime gameTime);

        public abstract void Draw(SpriteBatch spriteBatch);
    }
}
