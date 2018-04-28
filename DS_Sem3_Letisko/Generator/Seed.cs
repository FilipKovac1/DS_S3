using System;

namespace Generator
{
    public static class Seed
    {
        public static int GetSeed()
        {
            return Guid.NewGuid().GetHashCode();
        }
    }
}
