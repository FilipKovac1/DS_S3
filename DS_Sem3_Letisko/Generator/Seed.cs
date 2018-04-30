using System;

namespace Generator
{
    public static class Seed
    {
        public static int GetSeed() => Guid.NewGuid().GetHashCode();
    }
}
