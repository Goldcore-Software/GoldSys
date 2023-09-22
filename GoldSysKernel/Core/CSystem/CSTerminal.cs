using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace GoldSysKernel.Core.CS
{
    public class CSTerminal
    {
        private static int DisplayTerm = 0;
        public static int DisplayTerminal { get { return DisplayTerm; } set { SetTerminal(value); } }

        public static ConsoleColor ForegroundColor { get { return Console.ForegroundColor; } set { Console.ForegroundColor = value; } }

        public static ConsoleColor BackgroundColor { get { return Console.BackgroundColor; } set { Console.BackgroundColor = value; } }

        private static void SetTerminal(int newterm)
        {
            DisplayTerm = newterm;
            Console.Clear();
            if (Kernel.bootstage == -1)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("CSTerminal - Terminal " + newterm);
            }
        }
        public static void WriteLine(object text, int terminal)
        {
            if (terminal == DisplayTerm)
            {
                Console.WriteLine(text.ToString());
            }
        }
        public static void WriteLine(object text)
        {
            if (0 == DisplayTerm)
            {
                Console.WriteLine(text.ToString());
            }
        }
        public static void Write(object text, int terminal)
        {
            if (terminal == DisplayTerm)
            {
                Console.Write(text.ToString());
            }
        }
        public static void Write(object text)
        {
            if (0 == DisplayTerm)
            {
                Console.Write(text.ToString());
            }
        }
        public static void Clear()
        {
            Console.Clear();
        }

        public static string ReadLine()
        {
            return Console.ReadLine();
        }

        public static void WriteLine()
        {
            if (0 == DisplayTerm)
            {
                Console.WriteLine();
            }
        }
    }
}
