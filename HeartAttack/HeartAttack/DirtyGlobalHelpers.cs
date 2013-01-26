using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace HeartAttack
{
    public static class DirtyGlobalHelpers
    {
        public static Vector2 CentreOfScreen()
        {
            return new Vector2((HeartAttack.theGameInstance.GraphicsDevice.Viewport.Width) / 2,
                (HeartAttack.theGameInstance.GraphicsDevice.Viewport.Height) / 2);
        }
    }
}
