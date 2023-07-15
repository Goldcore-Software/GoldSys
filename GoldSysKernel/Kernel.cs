using Cosmos.Core.Memory;
using GoldSysKernel.Core;
using GoldSysKernel.Core.CS;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Sys = Cosmos.System;

namespace GoldSysKernel
{
    public class Kernel : Sys.Kernel
    {
        public static int bootstage { get; private set; }
        public static int SystemDrive { get; private set; } = -1;
        protected override void BeforeRun()
        {
            Console.Clear();
            bootstage = 0;
            Sys.FileSystem.CosmosVFS fs = new Sys.FileSystem.CosmosVFS();
            Sys.FileSystem.VFS.VFSManager.RegisterVFS(fs);
            CSLog.LogTerminal = 0;
            CSLog.Log("COSMOS/Kernel.cs", "File system initialised!", GSLogType.Ok);
            CSLog.Log("COSMOS/Kernel.cs", "Searching for system drive...", GSLogType.Info);
            for (int i = 0; i < 9; i++)
            {
                if (File.Exists(i+@":\GoldSys\sys.reg"))
                {
                    SystemDrive = i;
                    CSLog.Log("COSMOS/Kernel.cs", "Found system drive at \""+i+":\\\".", GSLogType.Ok);
                    break;
                }
            }
            if (SystemDrive == -1)
            {
                CSLog.Log("COSMOS/Kernel.cs", "Cannot find system drive. You must set up a drive yourself by typing \"setup\" in the drive that you want to be your system drive.", GSLogType.Warning);
            }
            else
            {
                try
                {
                    CSRegistry.LoadReg(SystemDrive + @":\GoldSys\sys.reg");
                    CSLog.Log("COSMOS/Kernel.cs", "Loaded system registry.", GSLogType.Ok);
                }
                catch (Exception e)
                {
                    CSLog.Log("COSMOS/Kernel.cs", "Failed to load system registry! The system can still boot but many features will be broken."+e.ToString(), GSLogType.Error);
                }
                
            }
            Console.ForegroundColor = ConsoleColor.Yellow;
            CSTerminal.WriteLine("GoldSys - Milestone 2 (0.2)", 0);
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
