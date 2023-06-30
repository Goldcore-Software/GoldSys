using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoldSysKernel.Core.CS
{
    internal class CSTerminal
    {
        private static int DisplayTerm = 0;
        public static int DisplayTerminal { get { return DisplayTerm; } set { SetTerminal(value); } }
        private static void SetTerminal(int newterm)
        {
            DisplayTerm = newterm;
            Console.Clear();
            if (Kernel.bootstage == -1)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("GSTerminal - Terminal " + newterm);
            }
        }
        public static void WriteLine(object text, int terminal)
        {
            if (terminal == DisplayTerm)
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
    }
}
