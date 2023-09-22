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
        public static void Log(string messager, string message, CSLogType type) 
        {
            CSTerminal.ForegroundColor = ConsoleColor.White;
            CSTerminal.Write("["+messager+"] ", LogTerminal);
            switch (type)
            {
                case CSLogType.Info:
                    CSTerminal.Write("[INFO] ", LogTerminal);
                    break;
                case CSLogType.Fatal:
                    CSTerminal.Write("[", LogTerminal);
                    CSTerminal.ForegroundColor = ConsoleColor.Red;
                    CSTerminal.Write("FATAL", LogTerminal);
                    CSTerminal.ForegroundColor = ConsoleColor.White;
                    CSTerminal.Write("] ", LogTerminal);
                    break;
                case CSLogType.Error:
                    CSTerminal.Write("[", LogTerminal);
                    CSTerminal.ForegroundColor = ConsoleColor.Red;
                    CSTerminal.Write("ERROR", LogTerminal);
                    CSTerminal.ForegroundColor = ConsoleColor.White;
                    CSTerminal.Write("] ", LogTerminal);
                    break;
                case CSLogType.Warning:
                    CSTerminal.Write("[", LogTerminal);
                    CSTerminal.ForegroundColor = ConsoleColor.Yellow;
                    CSTerminal.Write("WARN", LogTerminal);
                    CSTerminal.ForegroundColor = ConsoleColor.White;
                    CSTerminal.Write("] ", LogTerminal);
                    break;
                case CSLogType.Ok:
                    CSTerminal.Write("[", LogTerminal);
                    CSTerminal.ForegroundColor = ConsoleColor.Green;
                    CSTerminal.Write("OK", LogTerminal);
                    CSTerminal.ForegroundColor = ConsoleColor.White;
                    CSTerminal.Write("] ", LogTerminal);
                    break;
                default:
                    break;
            }
            CSTerminal.WriteLine(message, LogTerminal);
        }
    }
    enum CSLogType
    {
        Info,
        Fatal,
        Error,
        Warning,
        Ok
    }
}
