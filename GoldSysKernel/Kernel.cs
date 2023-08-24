using Cosmos.System;
using GoldSysKernel.Core;
using GoldSysKernel.Core.CS;
using GoldSysKernel.GSystem;
using GoldSysKernel.USystem;
using System;
using System.IO;
using Console = System.Console;
using Sys = Cosmos.System;

namespace GoldSysKernel
{
    public class Kernel : Sys.Kernel
    {
        public static int bootstage { get; private set; }
        public static int SystemDrive { get; set; } = -1;
        public static readonly int KernelVersion = 12; // Build/Commit 12
        public static readonly int SystemMilestone = 3; // Milestone 3
        protected override void BeforeRun()
        {
            try
            {
                Console.Clear();
                bootstage = 0;
                Sys.FileSystem.CosmosVFS fs = new Sys.FileSystem.CosmosVFS();
                Sys.FileSystem.VFS.VFSManager.RegisterVFS(fs);
                CSLog.LogTerminal = 0;
                CSLog.Log("COSMOS/Kernel.cs", "File system initialized!", CSLogType.Ok);
                CSLog.Log("COSMOS/Kernel.cs", "Searching for system drive...", CSLogType.Info);
                for (int i = 0; i < 9; i++)
                {
                    if (File.Exists(i+@":\GoldSys\sys.reg"))
                    {
                        SystemDrive = i;
                        CSLog.Log("COSMOS/Kernel.cs", "Found system drive at \""+i+":\\\".", CSLogType.Ok);
                        break;
                    }
                }
                if (SystemDrive == -1)
                {
                    CSLog.Log("COSMOS/Kernel.cs", "Cannot find system drive. You must set up a drive yourself by typing \"setup\" in the drive that you want to be your system drive.", CSLogType.Warning);
                }
                else
                {
                    try
                    {
                        CSRegistry.DefaultPath = SystemDrive + @":\GoldSys\sys.reg";
                        CSRegistry.LoadReg();
                        CSLog.Log("COSMOS/Kernel.cs", "Loaded system registry.", CSLogType.Ok);
                    }
                    catch (Exception e)
                    {
                        CSLog.Log("COSMOS/Kernel.cs", "Failed to load system registry! The system can still boot but many features will be broken."+e.ToString(), CSLogType.Error);
                    }
                }
                GSManager.Initialize();
                MouseManager.ScreenHeight = 480;
                MouseManager.ScreenWidth = 640;
                CSLog.Log("COSMOS/Kernel.cs","Initialized the Graphics Manager.",CSLogType.Ok);
                Console.ForegroundColor = ConsoleColor.Yellow;
                CSTerminal.WriteLine("GoldSys - Milestone 2 (0.2)", 0);
                Console.ForegroundColor = ConsoleColor.White;
                bootstage = -1;
                if (!(SystemDrive == -1))
                {
                    if (USAccountManager.GetAccount("root") == null)
                    {
                        USAccountManager.RegisterAccount("root", "root", USAccountPermLevel.Root);
                    }
                    CSTerminal.Write("login: ", 0);
                    string name = Console.ReadLine();
                    CSTerminal.Write("password: ", 0);
                    string pwd = Console.ReadLine();
                    while (!USAccountManager.Login(name, pwd))
                    {
                        CSTerminal.WriteLine("Invalid username/password", 0);
                        CSTerminal.Write("login: ", 0);
                        name = Console.ReadLine();
                        CSTerminal.Write("password: ", 0);
                        pwd = Console.ReadLine();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            
        }

        protected override void Run()
        {
            try
            {
                if (CSTerminal.DisplayTerminal == 0)
                {
                    CSTerminal.Write(USAccountManager.CurrentUser+"&"+Shell.GetFullPath()+"> ", 0);
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
                else if (CSTerminal.DisplayTerminal == 2)
                {
                    GSManager.Draw();
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
