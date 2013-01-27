using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using System.IO;

namespace HeartAttack
{
    public static class DirtyGlobalHelpers
    {
        public static int highscore = 0;
        public static Vector2 CentreOfScreen()
        {
            return new Vector2((HeartAttack.theGameInstance.GraphicsDevice.Viewport.Width) / 2,
                (HeartAttack.theGameInstance.GraphicsDevice.Viewport.Height) / 2);
        }

        public static void LoadHighscores()
        {
//            StreamReader tr = new StreamReader("highscore.txt");

            int highscore = 0;
            try
            {
        //        highscore = int.Parse(tr.ReadLine());
            }
            catch { }
            DirtyGlobalHelpers.highscore = highscore;
        }

        public static void SaveHighscores()
        {
            StreamWriter tr = new StreamWriter("highscore.txt");

            try
            {
                tr.WriteLine(DirtyGlobalHelpers.highscore);
            }
            catch { }
        }

        #region config variables

        public const float PING_SPEED = 600;


        public const int MAX_TIME_BUGS_SHOWN = 1000;
        public const int MIN_TIME_BUGS_SHOWN = 150;
        public const int MAX_SPAWN_INTERVAL = 1000;
        public const int MIN_SPAWN_INTERVAL = 100;
        public const int MAX_BULLET_DELAY = 800;
        public const int MIN_BULLET_DELAY = 400;
        public const int MAX_BULLET_SPEED = 500;
        public const int MIN_BULLET_SPEED = 300;
        public const int MAX_BUG_SPEED = 600;
        public const int MIN_BUG_SPEED = 80;
        public const int STRESS_THRESHHOLD = 5;
        public const int BUG_SPEED_STEP = 5;
        public const int BUG_SPAWN_INTERVAL_STEP = 200;
        public const int BULLET_SPEED_STEP = 20;
        public const int BULLET_DELAY_STEP = 20;
        public const int BUG_VISIBLE_STEP = 5;

        public class Config
        {
            public int TimeBugsShown = MAX_TIME_BUGS_SHOWN;
            public int BugSpeed = MIN_BUG_SPEED;
            public int BugSpawnInterval = DirtyGlobalHelpers.MAX_SPAWN_INTERVAL;
            public int BulletDelay = MIN_BULLET_DELAY;
            public int BulletSpeed = MAX_BULLET_SPEED;

            public void IncreaseBugSpeed()
            {
                BugSpeed += BUG_SPEED_STEP;
            }
            public void DecreaseBugSpeed()
            {
                BugSpeed -= BUG_SPEED_STEP;
            }
            public void IncreaseBugSpawnInterval()
            {
                BugSpawnInterval += BUG_SPAWN_INTERVAL_STEP;
            }
            public void DecreaseBugSpawnInterval()
            {
                BugSpawnInterval -= BUG_SPAWN_INTERVAL_STEP;
            }

            public void IncreaseBulletSpeed()
            {
                BugSpeed += BUG_SPEED_STEP;
            }
            public void DecreaseBulletSpeed()
            {
                BugSpeed -= BUG_SPEED_STEP;
            }
            public void IncreaseBulletDelay()
            {
                BulletDelay += BULLET_DELAY_STEP;
            }
            public void DecreaseBulletDelay()
            {
                BulletDelay -= BULLET_DELAY_STEP;
            }
            public void IncreaseTimeBugsVisible()
            {
                TimeBugsShown += BUG_VISIBLE_STEP;
            }
            public void DecreaseTimeBugsVisible()
            {
                TimeBugsShown -= BUG_VISIBLE_STEP;
            }


        }

        public static Config config = new Config();

        #endregion
    }
}
