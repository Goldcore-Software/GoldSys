using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoldSysKernel.Core.GS
{
    internal class GSLog
    {
        public static int LogTerminal = 1;
        public static void Log(string messager, string message, GSLogType type) 
        {
            Console.ForegroundColor = ConsoleColor.White;
            GSTerminal.Write("["+messager+"] ", LogTerminal);
            switch (type)
            {
                case GSLogType.Info:
                    GSTerminal.Write("[INFO] ", LogTerminal);
                    break;
                case GSLogType.Fatal:
                    GSTerminal.Write("[", LogTerminal);
                    Console.ForegroundColor = ConsoleColor.Red;
                    GSTerminal.Write("FATAL", LogTerminal);
                    Console.ForegroundColor = ConsoleColor.White;
                    GSTerminal.Write("] ", LogTerminal);
                    break;
                case GSLogType.Error:
                    GSTerminal.Write("[", LogTerminal);
                    Console.ForegroundColor = ConsoleColor.Red;
                    GSTerminal.Write("ERROR", LogTerminal);
                    Console.ForegroundColor = ConsoleColor.White;
                    GSTerminal.Write("] ", LogTerminal);
                    break;
                case GSLogType.Warning:
                    GSTerminal.Write("[", LogTerminal);
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    GSTerminal.Write("WARN", LogTerminal);
                    Console.ForegroundColor = ConsoleColor.White;
                    GSTerminal.Write("] ", LogTerminal);
                    break;
                case GSLogType.Ok:
                    GSTerminal.Write("[", LogTerminal);
                    Console.ForegroundColor = ConsoleColor.Green;
                    GSTerminal.Write("OK", LogTerminal);
                    Console.ForegroundColor = ConsoleColor.White;
                    GSTerminal.Write("] ", LogTerminal);
                    break;
                default:
                    break;
            }
            GSTerminal.WriteLine(message, LogTerminal);
        }
    }
    enum GSLogType
    {
        Info,
        Fatal,
        Error,
        Warning,
        Ok
    }
}
