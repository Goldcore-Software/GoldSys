using GoldSysKernel.GSystem.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoldSysKernel.GSystem
{
    public class GSMessageBox
    {
        public static void ShowMsgBox(string title, string message)
        {
            MsgBoxWindow boxWindow = new MsgBoxWindow();
            boxWindow.Title = title;
            boxWindow.message = message;
            GSManager.OpenWindow(boxWindow);
        }
    }
}
