using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace HeartAttack
{
    public class MockPulseMonitor
    {
        public int m_TimeToNextPing; // in milliseconds
        public int m_PingInterval; // in milliseconds
        public PingManager m_PingManager;

        public MockPulseMonitor(int pRate, PingManager pPingManager)
        {
            m_PingInterval = 60000 / pRate;
            m_TimeToNextPing = 0;
            m_PingManager = pPingManager;
        }

        public void Update(GameTime pGameTime)
        {
            m_TimeToNextPing -= pGameTime.ElapsedGameTime.Milliseconds;

            if (m_TimeToNextPing <= 0)
            {
                m_TimeToNextPing += m_PingInterval;
                m_PingManager.FirePing();
            }
        }


    }
}
