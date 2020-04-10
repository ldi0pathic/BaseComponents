using System;

namespace Extension
{
    public static class ConsoleExtension
    {
        public static void WriteOnLine(this string msg, int postion, ConsoleColor color = ConsoleColor.White )
        {
            var oldPos = Console.CursorTop;

            Console.SetCursorPosition(Console.CursorLeft, postion);
            msg.WriteLine(color);
            Console.SetCursorPosition(Console.CursorLeft, oldPos);
        }

        public static void WriteLine(this string msg, ConsoleColor color = ConsoleColor.White)
        {
            var oldColor = Console.ForegroundColor;

            Console.ForegroundColor = color;
            Console.WriteLine(">> "+msg.TrimStart(' ').PadRight(100, ' '));
            Console.ForegroundColor = oldColor;
        }

        public static void Write(this string msg, ConsoleColor color = ConsoleColor.White)
        {
            var oldColor = Console.ForegroundColor;

            Console.ForegroundColor = color;
            Console.Write(">> " + msg.TrimStart(' ').PadRight(100, ' '));
            Console.ForegroundColor = oldColor;
        }

        public static void DrawProgressBar(this string msg, int progress, int total)
        {
            Console.CursorLeft = 1;
            float onechunk = 30.0f / total;

            int pos = 1;
            for (int i = 0; i < onechunk * progress; i++)
            {
                Console.BackgroundColor = ConsoleColor.Green;
                Console.CursorLeft = pos++;
                Console.Write(" ");
            }

            for (int i = pos; i <= 31; i++)
            {
                Console.BackgroundColor = ConsoleColor.Gray;
                Console.CursorLeft = pos++;
                Console.Write(" ");
            }

            Console.CursorLeft = pos + 3;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Write($"{DateTime.Now.ToString("H:mm")} |{(progress < 10 ? " " : "")}{progress.ToString()} | {total.ToString()} {msg}");
        }
    }
}
