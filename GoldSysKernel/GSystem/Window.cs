using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoldSysKernel.GSystem
{
    public abstract class Window
    {
        public string Title = "Window";
        public int SizeX = 640;
        public int SizeY = 420;
        public int PositionX = 0;
        public int PositionY = 60;
        public abstract void Draw();
        public abstract void Open();
        public void DrawTitleBar()
        {
            GSManager.screen.DrawFilledRectangle(Color.Aqua,PositionX,PositionY-30,SizeX,30);
            GSManager.screen.DrawFilledRectangle(Color.White,PositionX,PositionY,SizeX,SizeY);
        }
    }
}
