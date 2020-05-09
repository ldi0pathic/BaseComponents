using Extensions;
using System;
using static Utils.Sleeper;

namespace Basic.Utils
{
    public class Timer
    {
       
        public bool Restart { get; set; }

        public Timer(Action action,  int m = 1, SleepType type = SleepType.m)
        {
            SetTimer(action, m, type);
        }

        private void SetTimer(Action action, int time, SleepType type)
        {
            var realTime = time * (int)type;
            for (int index = 0; index < realTime; index += (int)SleepType.s)
            {
                new DateTime().AddMilliseconds(realTime - index).ToString("HH:mm:ss").WriteOnLine(Console.CursorTop);         
                Sleep(1, SleepType.s);      
            }

            ConsoleExtension.ClearLine();
            action();

            if(Restart)
                SetTimer(action, time, type);
        }
    }
}
