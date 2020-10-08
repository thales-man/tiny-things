using System;

namespace Tiny.Framework.Utility
{
    /// <summary>
    /// a color conosle 'helper'.
    /// </summary>
    public static class ColorConsole
    {
        /// <summary>
        /// write.
        /// </summary>
        /// <param name="inColor">in color.</param>
        /// <param name="withMessage">with message.</param>
        public static void Write(ConsoleColor inColor, string withMessage)
        {
            Console.ForegroundColor = inColor;
            Console.Write(withMessage);
            Console.ResetColor();
        }

        /// <summary>
        /// write line.
        /// </summary>
        /// <param name="inColor">in color.</param>
        /// <param name="withMessage">with message.</param>
        public static void WriteLine(ConsoleColor inColor, string withMessage)
        {
            Console.ForegroundColor = inColor;
            Console.WriteLine(withMessage);
            Console.ResetColor();
        }

        /// <summary>
        /// write line.
        /// </summary>
        public static void WriteLine()
        {
            Console.WriteLine();
            Console.ResetColor();
        }

        /// <summary>
        /// consistent startup message format and colors.
        /// </summary>
        /// <param name="message">the message.</param>
        public static void Startup(string message)
        {
            Write(ConsoleColor.Magenta, "startup message:");
            WriteLine(ConsoleColor.Cyan, message);
        }

        /// <summary>
        /// consistent startup message format and colors.
        /// </summary>
        /// <param name="message">the message.</param>
        public static void Warning(string message)
        {
            Write(ConsoleColor.DarkYellow, "warning:");
            WriteLine(ConsoleColor.Yellow, message);
        }

        /// <summary>
        /// consistent startup message format and colors.
        /// </summary>
        /// <param name="message">the message.</param>
        public static void Error(string message)
        {
            Write(ConsoleColor.DarkRed, "warning:");
            WriteLine(ConsoleColor.Red, message);
        }
    }
}
