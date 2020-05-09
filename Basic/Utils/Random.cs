using System;

namespace Utils
{
    public static class Random
    {
        public static bool RandomBool(uint chance)
        {
            if (chance == 0)
                return false;

            if (chance == 100)
                return true;

            var ticks = DateTime.Now.Ticks;

            return ticks % 100 <= chance;      
        }

        public static int GetRandomIntBetween(int min, int max)
        {
            return (int)(DateTime.Now.Ticks % (max - min)) + min;
        }
    }
}
