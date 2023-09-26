using Cosmos.System;
using Cosmos.System.Graphics.Fonts;
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
        public bool Closed = false;
        public abstract void Draw();
        public abstract void Open();
        public abstract void Run();
        public void DrawTitleBar()
        {
            GSManager.screen.DrawFilledRectangle(Color.DarkViolet,PositionX,PositionY-30,SizeX,30);
            GSManager.screen.DrawFilledRectangle(Color.Red,PositionX+SizeX-30,PositionY-30,30,30);
            GSManager.screen.DrawFilledRectangle(Color.White,PositionX,PositionY,SizeX,SizeY);
            GSManager.screen.DrawString(Title, PCScreenFont.Default,Color.White,PositionX+3,PositionY-22);
            if (MouseManager.MouseState == MouseState.Left)
            {
                if ((MouseManager.X >= PositionX + SizeX - 30 && MouseManager.X <= PositionX + SizeX) && (MouseManager.Y >= PositionY-30 && MouseManager.Y <= PositionY))
                {
                    Close();
                }
            }
        }
        public void Close()
        {
            Closed = true;
        }
    }
}
