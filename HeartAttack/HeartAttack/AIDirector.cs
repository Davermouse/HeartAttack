using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HeartAttack
{
    // AI Director modifies
    public class AIDirector
    {
        private bool m_Heartcore;
        private PlayerThing m_Player;
        private Random m_RNG;

        public AIDirector(PlayerThing pPlayer, bool pHeartcore)
        {
            m_Player = pPlayer;
            m_Heartcore = pHeartcore;
        }

        public void ModifyDifficulty()
        {
            if (m_Heartcore)
            {
                if (m_Player.IsStressed())
                {
                    MakeHarder();
                }
                else
                {
                    MakeEasier();
                }
            }
            else
            {
                if (m_Player.IsStressed())
                {
                    MakeEasier();
                }
                else
                {
                    MakeHarder();
                }
            }
        }
        public void MakeHarder()
        {
            DirtyGlobalHelpers.config.IncreaseBugSpeed();
            DirtyGlobalHelpers.config.IncreaseBugSpawnInterval();
            DirtyGlobalHelpers.config.IncreaseBulletDelay();
            DirtyGlobalHelpers.config.DecreaseBulletSpeed();
            DirtyGlobalHelpers.config.DecreaseTimeBugsVisible();
        }

        public void MakeEasier()
        {
            DirtyGlobalHelpers.config.DecreaseBugSpeed();
            DirtyGlobalHelpers.config.DecreaseBugSpawnInterval();
            DirtyGlobalHelpers.config.DecreaseBulletDelay();
            DirtyGlobalHelpers.config.IncreaseBulletSpeed();
            DirtyGlobalHelpers.config.IncreaseTimeBugsVisible();
        }
    }
}
