namespace Utils
{
    public static class Sleeper
    {
        public enum SleepType
        {
            z = 1,
            s = 1000,
            m = s * 60,
            h = m * 60,
            d = h * 24,
        }

        public static void Sleep(int ms = 500, SleepType type = SleepType.z)
        {
            System.Threading.Thread.Sleep(ms * (int)type);
        }

        public static void RandomSleep(int min = 100, int max = 1000, SleepType type = SleepType.z)
        {
            Sleep(Random.GetRandomIntBetween(min, max), type);
        }
    }
}
