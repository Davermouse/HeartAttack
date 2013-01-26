using System;

namespace HeartAttack
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (HeartAttack game = new HeartAttack())
            {
                game.Run();
            }
        }
    }
#endif
}

