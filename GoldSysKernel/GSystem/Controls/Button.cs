using Cosmos.System;
using Cosmos.System.Graphics.Fonts;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoldSysKernel.GSystem.Controls
{
    public class Button : Control
    {
        public string Text = "Button";
        public override void Draw()
        {
            GSManager.screen.DrawRectangle(Color.Black,PositionX,PositionY,SizeX,SizeY);
            GSManager.screen.DrawString(Text,PCScreenFont.Default,Color.Black,PositionX+5,(PositionY+(SizeY/2))-8);
        }

        public bool Run()
        {
            if (MouseManager.MouseState == MouseState.Left)
            {
                if ((MouseManager.X >= PositionX && MouseManager.X <= PositionX+SizeX) && (MouseManager.Y >= PositionY && MouseManager.Y <= PositionY + SizeY))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
