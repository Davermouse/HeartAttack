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

        #region config variables

        public const float PING_SPEED = 600;


        public const int MAX_TIME_BUGS_SHOWN = 1000;
        public const int MIN_TIME_BUGS_SHOWN = 150;
        public const int MAX_SPAWN_INTERVAL = 5000;
        public const int MIN_SPAWN_INTERVAL = 1000;
        public const int MAX_BULLET_DELAY = 500;
        public const int MIN_BULLET_DELAY = 100;
        public const int MAX_BULLET_SPEED = 800;
        public const int MIN_BULLET_SPEED = 200;
        public const int MAX_BUG_SPEED = 600;
        public const int MIN_BUG_SPEED = 20;

        public class Config
        {
            public int TimeBugsShown = 300;
            public int BugSpeed = 20;
            public int BugSpawnInterval = 5000;
            public int BulletDelay = 400;
            public int BulletSpeed = 200;
        }

        public static Config config = new Config();

        #endregion
    }
}
