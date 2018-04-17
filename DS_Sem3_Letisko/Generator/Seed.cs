using System;

namespace Generator
{
    class Seed
    {
        private static Random random = new Random();

        public static int GetSeed()
        {
            return random.Next();
        }
    }
}
