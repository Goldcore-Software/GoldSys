using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace GoldSysKernel.Core.CS
{
    internal class CSRegistry
    {
        public static Dictionary<string, string> reg = new Dictionary<string, string>();
        public static StringBuilder sb = new StringBuilder();
        public static void SaveReg(string path)
        {
            sb.Clear();
            foreach (var regkey in reg)
            {
                sb.AppendLine(regkey.Key+";"+regkey.Value);
            }
            File.WriteAllText(path, sb.ToString());
            sb.Clear();
        }
        public static void LoadReg(string path)
        {
            string regserialized = File.ReadAllText(path);
            string[] keyvalue;
            reg.Clear();
            foreach (var item in regserialized.Split("\n"))
            {
                keyvalue = item.Split(";");
                reg.Add(keyvalue[0], keyvalue[1]);
            }
        }
    }
}
