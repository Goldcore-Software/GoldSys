using Cosmos.Core.Memory;
using GoldSysKernel.Core;
using GoldSysKernel.Core.CS;
using System;
using System.Collections.Generic;
using System.Text;
using Sys = Cosmos.System;

namespace GoldSysKernel
{
    public class Kernel : Sys.Kernel
    {
        public static int bootstage { get; private set; }
        protected override void BeforeRun()
        {
            Console.Clear();
            bootstage = 0;
            Sys.FileSystem.CosmosVFS fs = new Sys.FileSystem.CosmosVFS();
            Sys.FileSystem.VFS.VFSManager.RegisterVFS(fs);
            CSLog.LogTerminal = 0;
            CSLog.Log("Kernel.cs","File system initialised!",GSLogType.Ok);
            Console.ForegroundColor = ConsoleColor.Yellow;
            CSTerminal.WriteLine("GoldSys - Milestone 1 (0.1)", 0);
            Console.ForegroundColor = ConsoleColor.White;
            bootstage = -1;
        }

        protected override void Run()
        {
            try
            {
                if (CSTerminal.DisplayTerminal == 0)
                {
                    CSTerminal.Write(Shell.GetFullPath()+"> ", 0);
                    string cmd = Console.ReadLine();
                    try
                    {
                        Shell.Command(cmd);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.ToString());
                    }
                }
            }
            catch (Exception e)
            {
                Crash(e);
            }
        }
        private static void Crash(Exception e)
        {
            CSTerminal.DisplayTerminal = -1;
            Console.BackgroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Clear();
            CSTerminal.WriteLine("Oops! GoldSys has crashed.", -1);
            CSTerminal.WriteLine("The exception was: "+e.ToString(), -1);
            CSTerminal.WriteLine("Please contact the developer, eli310#9755!", -1);
            CSTerminal.WriteLine("You need to manually restart", -1);
        }
    }
}
