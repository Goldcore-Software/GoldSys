using GoldSysKernel.Core.CS;
using GoldSysKernel.USystem;
using IL2CPU.API.Attribs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GoldSysKernel.Core
{
    internal class Shell
    {
        [ManifestResourceStream(ResourceName = "GoldSysKernel.mscorlib.dll")]
        private static byte[] mscorlib;
        [ManifestResourceStream(ResourceName = "GoldSysKernel.TestApp.dll")]
        private static byte[] testapp;
        public static string CurrentDir { get; private set; }
        public static string CurrentVol { get; private set; } = "0";
        public static void Command(string cmd)
        {
            string[] cmdsplit = cmd.Split(' ');
            switch (cmdsplit[0])
            {
                case "cd":
                    if (cmdsplit[1] == "..")
                    {
                        if (CurrentDir == "")
                        {
                            break;
                        }
                        char currletter = CurrentDir[CurrentDir.Length - 1];
                        while (!(currletter == "\\".ToCharArray()[0]))
                        {
                            CurrentDir = CurrentDir.Remove(CurrentDir.Length - 1);
                            if (CurrentDir.Length == 0) { break; }
                            currletter = CurrentDir[CurrentDir.Length - 1];
                        }
                        if (CurrentDir.Length == 0) { break; }
                        CurrentDir = CurrentDir.Remove(CurrentDir.Length - 1);
                        break;
                    }
                    string bdir = CurrentDir;
                    if (CurrentDir == "")
                    {
                        CurrentDir += cmdsplit[1];
                    }
                    else
                    {
                        CurrentDir += "\\" + cmdsplit[1];
                    }
                    if (!Directory.Exists(CurrentVol + ":\\" + CurrentDir))
                    {
                        CurrentDir = bdir;
                        Console.WriteLine("Directory not found!");
                    }
                    break;
                case "term":
                    CSTerminal.DisplayTerminal = int.Parse(cmdsplit[1]);
                    break;
                case "cls":
                    Console.Clear();
                    break;
                case "logtest":
                    CSTerminal.DisplayTerminal = 1;
                    CSLog.Log("Log Test!", "This is a test of the logger 1", CSLogType.Info);
                    CSLog.Log("Log Test!", "This is a test of the logger 2", CSLogType.Fatal);
                    CSLog.Log("Log Test!", "This is a test of the logger 3", CSLogType.Error);
                    CSLog.Log("Log Test!", "This is a test of the logger 4", CSLogType.Warning);
                    CSLog.Log("Log Test!", "This is a test of the logger 5", CSLogType.Ok);
                    break;
                case "crashtest":
                    throw new Exception("you broke it.");
                case "md":
                case "mkdir":
                    Directory.CreateDirectory(GetFullPath(cmdsplit[1]));
                    break;
                case "rd":
                case "rmdir":
                    Directory.Delete(GetFullPath(cmdsplit[1]), true);
                    break;
                case "del":
                    File.Delete(GetFullPath(cmdsplit[1]));
                    break;
                case "dir":
                case "ls":
                    string[] filePaths = Directory.GetFiles(GetFullPath());
                    var drive = new DriveInfo(CurrentVol);
                    Console.WriteLine("Volume in drive 0 is " + $"{drive.VolumeLabel}");
                    Console.WriteLine("Directory of " + GetFullPath());
                    Console.WriteLine("\n");
                    if (filePaths.Length == 0 && Directory.GetDirectories(GetFullPath()).Length == 0)
                    {
                        Console.WriteLine("File Not Found");
                    }
                    else
                    {
                        for (int i = 0; i < filePaths.Length; ++i)
                        {
                            string path = filePaths[i];
                            Console.WriteLine(Path.GetFileName(path));
                        }
                        foreach (var d in Directory.GetDirectories(GetFullPath()))
                        {
                            var dir = new DirectoryInfo(d);
                            var dirName = dir.Name;

                            Console.WriteLine(dirName + " <DIR>");
                        }
                    }
                    Console.WriteLine("\n");
                    Console.WriteLine("        " + $"{drive.TotalSize}" + " bytes");
                    Console.WriteLine("        " + $"{drive.AvailableFreeSpace}" + " bytes free");
                    break;
                case "writefile":
                    string contents = cmd.Substring(cmdsplit[0].Length + cmdsplit[1].Length + 2);
                    File.WriteAllText(GetFullPath(cmdsplit[1]), contents);
                    break;
                case "cat":
                    if (File.Exists(GetFullPath(cmdsplit[1])))
                    {
                        Console.WriteLine("File exists! (File.Exists())");
                    }
                    else
                    {
                        Console.WriteLine("File does not exist! (File.Exists())");
                    }
                    Console.WriteLine(File.ReadAllText(GetFullPath(cmdsplit[1])));
                    break;
                case "debug":
                    switch (cmdsplit[1])
                    {
                        case "currentdir":
                            Console.WriteLine(CurrentDir);
                            break;
                        case "currentvol":
                            Console.WriteLine(CurrentVol);
                            break;
                        default:
                            break;
                    }
                    break;
                case "setup":
                    CSTerminal.WriteLine("Setting up system drive...",0);
                    Directory.CreateDirectory(CurrentVol + @":\GoldSys");
                    Directory.CreateDirectory(CurrentVol + @":\GoldSys\dotnet");
                    Kernel.SystemDrive = int.Parse(CurrentVol);
                    CSRegistry.DefaultPath = Kernel.SystemDrive + @":\GoldSys\sys.reg";
                    USAccountManager.RegisterAccount("root","root",USAccountPermLevel.Root);
                    CSRegistry.SaveReg(CurrentVol + @":\GoldSys\sys.reg");
                    File.WriteAllBytes(Kernel.SystemDrive+@":\GoldSys\dotnet\mscorlib.dll",mscorlib);
                    CSTerminal.WriteLine("You must reboot the system to finish setting up the system drive.",0);
                    break;
                case "createuser":
                    if (USAccountManager.GetAccount(cmdsplit[1]) == null)
                    {
                        CSTerminal.WriteLine("Creating new account...", 0);
                        USAccountManager.RegisterAccount(cmdsplit[1], cmdsplit[2],USAccountPermLevel.Administrator);
                    } else { CSTerminal.WriteLine("This user already exists.",0); }
                    break;
                case "passwd":
                    if (!(USAccountManager.GetAccount(USAccountManager.CurrentUser) == null))
                    {
                        CSRegistry.reg["USAccountManager." + USAccountManager.CurrentUser + ".password"] = cmdsplit[1];
                    }
                    break;
                case "kcp":
                    if (cmdsplit[1].ToLower() == "testapp.dll")
                    {
                        File.WriteAllBytes(GetFullPath("TestApp.dll"),testapp);
                        CSTerminal.WriteLine("Copied TestApp.exe from the kernel into the current directory.",0);
                    }
                    break;
                case "reboot":
                    CSTerminal.WriteLine("Restarting...", 0);
                    if (!(Kernel.SystemDrive == -1))
                    {
                        CSRegistry.SaveReg();
                    }
                    Cosmos.System.Power.Reboot();
                    break;
                default:
                    if (cmdsplit[0].EndsWith(":") && cmdsplit[0].Length == 2)
                    {
                        try
                        {
                            Directory.GetFiles(cmdsplit[0] + "\\");
                            CurrentVol = cmdsplit[0][0].ToString();
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Could not change drive!");
                        }
                        break;
                    } else if (File.Exists(GetFullPath(cmd))) {
                        try
                        {
                            Process process = new Process(File.ReadAllBytes(GetFullPath(cmd)));
                            process.clr.RegisterResolveCallBack(AssemblyCallback);
                            process.clr.Start();
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Command not found!");
                            throw;
                        }
                    } else {
                        Console.WriteLine("Command not found!");
                    }
                    break;
            }
        }
        /// <summary>
        /// Get the full path of a file.
        /// </summary>
        /// <param name="name">The filename.</param>
        /// <returns>The full path.</returns>
        public static string GetFullPath(string name)
        {
            if (CurrentDir == "")
            {
                return CurrentVol + @":\" + name;
            }
            else
            {
                return CurrentVol + @":\" + CurrentDir + "\\" + name;
            }
        }
        public static string GetFullPath()
        {
            if (CurrentDir == "")
            {
                return CurrentVol + @":\";
            }
            else
            {
                return CurrentVol + @":\" + CurrentDir;
            }
        }
        private static byte[] AssemblyCallback(string dll)
        {
            switch (dll)
            {
                case "System.Private.CoreLib":
                    return File.ReadAllBytes(Kernel.SystemDrive + @":\GoldSys\dotnet\mscorlib.dll");
                default:
                    return null;
            }
        }
    }
}
