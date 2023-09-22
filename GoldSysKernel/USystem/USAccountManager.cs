using Cosmos.HAL.BlockDevice.Registers;
using GoldSysKernel.Core.CSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace GoldSysKernel.USystem
{
    public class USAccountManager
    {
        public static string CurrentUser { get; private set; } = "root";
        public static USAccount CurrentUserAccount { get; private set; } = new USAccount("root","root",USAccountPermLevel.Root);
        public static bool Login(USAccount account,string pwd)
        {
            if (CSRegistry.reg.ContainsKey("USAccountManager."+account.accountname+".password"))
            {
                if (account.CheckPwd(pwd))
                {
                    CurrentUser = account.accountname;
                    return true;
                }
            }
            return false;
        }
        public static bool Login(string name, string pwd)
        {
            USAccount acc = GetAccount(name);
            if (acc == null) { return false; } else if (acc.CheckPwd(pwd))
            {
                CurrentUser = acc.accountname;
                return true;
            }
            return false;
        }
        public static USAccount GetAccount(string name)
        {
            if (CSRegistry.reg.ContainsKey("USAccountManager." + name + ".password"))
            {
                return new USAccount(name, CSRegistry.reg["USAccountManager." + name + ".password"], ((USAccountPermLevel)int.Parse(CSRegistry.reg["USAccountManager." + name + ".perms"])));
            }
            return null;
        }
        public static void RegisterAccount(string name, string pwd, USAccountPermLevel perms)
        {
            CSRegistry.reg["USAccountManager." + name + ".password"] = pwd;
            CSRegistry.reg["USAccountManager." + name + ".perms"] = ((int)perms).ToString();
            CSRegistry.SaveReg();
        }
    }
    public enum USAccountPermLevel
    {
        Guest,
        User,
        Administrator,
        Root
    }
    public class USAccount
    {
        public string accountname { get; private set; }
        private string password;
        public USAccountPermLevel permissions { get; private set; }
        public USAccount(string name,string pwd,USAccountPermLevel perm)
        {
            accountname = name;
            password = pwd;
            permissions = perm;
        }
        public bool CheckPwd(string pwd)
        {
            if ((password == null) || (password == string.Empty)) { return true; } else if (password == pwd) { return true; }
            return false;
        }
    }
}
