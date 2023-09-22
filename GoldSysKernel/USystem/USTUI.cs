using GoldSysKernel.Core.CS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoldSysKernel.USystem
{
    public class USTUI
    {
        public static int ShowMenu(string[] options,string title)
        {
            Console.CursorVisible = false;
            CSTerminal.BackgroundColor = ConsoleColor.Blue;
            CSTerminal.ForegroundColor = ConsoleColor.White;
            CSTerminal.Clear();
            CSTerminal.WriteLine(title);
            CSTerminal.WriteLine();
            foreach (var choice in options)
            {
                CSTerminal.WriteLine("  " + choice);
            }
            int selection = 0;
            ConsoleKeyInfo key;
            while (true)
            {
                Console.SetCursorPosition(0, selection + 2);
                CSTerminal.Write(">");
                key = Console.ReadKey();
                if (key.Key == ConsoleKey.Enter)
                {
                    CSTerminal.BackgroundColor = ConsoleColor.Black;
                    CSTerminal.Clear();
                    Console.CursorVisible = true;
                    return selection;
                }
                else if (key.Key == ConsoleKey.UpArrow && selection > 0)
                {
                    Console.SetCursorPosition(0, selection + 2);
                    CSTerminal.Write(" ");
                    selection--;
                }
                else if (key.Key == ConsoleKey.DownArrow && selection < options.Length - 1)
                {
                    Console.SetCursorPosition(0, selection + 2);
                    CSTerminal.Write(" ");
                    selection++;
                }
            }
        }
    }
}
