using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoldSysKernel.Core.CS
{
    internal class CSLog
    {
        public static int LogTerminal = 1;
        public static void Log(string messager, string message, GSLogType type) 
        {
            Console.ForegroundColor = ConsoleColor.White;
            CSTerminal.Write("["+messager+"] ", LogTerminal);
            switch (type)
            {
                case GSLogType.Info:
                    CSTerminal.Write("[INFO] ", LogTerminal);
                    break;
                case GSLogType.Fatal:
                    CSTerminal.Write("[", LogTerminal);
                    Console.ForegroundColor = ConsoleColor.Red;
                    CSTerminal.Write("FATAL", LogTerminal);
                    Console.ForegroundColor = ConsoleColor.White;
                    CSTerminal.Write("] ", LogTerminal);
                    break;
                case GSLogType.Error:
                    CSTerminal.Write("[", LogTerminal);
                    Console.ForegroundColor = ConsoleColor.Red;
                    CSTerminal.Write("ERROR", LogTerminal);
                    Console.ForegroundColor = ConsoleColor.White;
                    CSTerminal.Write("] ", LogTerminal);
                    break;
                case GSLogType.Warning:
                    CSTerminal.Write("[", LogTerminal);
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    CSTerminal.Write("WARN", LogTerminal);
                    Console.ForegroundColor = ConsoleColor.White;
                    CSTerminal.Write("] ", LogTerminal);
                    break;
                case GSLogType.Ok:
                    CSTerminal.Write("[", LogTerminal);
                    Console.ForegroundColor = ConsoleColor.Green;
                    CSTerminal.Write("OK", LogTerminal);
                    Console.ForegroundColor = ConsoleColor.White;
                    CSTerminal.Write("] ", LogTerminal);
                    break;
                default:
                    break;
            }
            CSTerminal.WriteLine(message, LogTerminal);
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
