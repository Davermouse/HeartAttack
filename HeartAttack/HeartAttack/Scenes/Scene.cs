using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace HeartAttack
{
    public class Scene
    {
        public virtual Scene Update(GameTime pGameTime) { return this; }
        public virtual void Draw(GameTime pGameTime) { }
    }
}
