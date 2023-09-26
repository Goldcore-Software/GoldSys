using Cosmos.System.Graphics.Fonts;
using GoldSysKernel.GSystem.Controls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoldSysKernel.GSystem.Windows
{
    public class MsgBoxWindow : Window
    {
        private Button okbutton;
        public string message;
        public override void Draw()
        {
            okbutton.Draw();
            GSManager.screen.DrawString(message,PCScreenFont.Default,Color.Black,PositionX+5, PositionY + ((SizeY / 2) - 20));
        }

        public override void Open()
        {
            PositionX = 100;
            PositionY = 100;
            SizeX = (message.Length * 8) + 10;
            if (SizeX < 125) { SizeX = 125; }
            SizeY = 75;
            okbutton = new Button();
            okbutton.Text = "OK";
            okbutton.PositionX = PositionX + SizeX - 70;
            okbutton.PositionY = PositionY + SizeY - 35;
            okbutton.SizeX = 65;
            okbutton.SizeY = 30;
        }

        public override void Run()
        {
            if (okbutton.Run())
            {
                Close();
            }
        }
    }
}
