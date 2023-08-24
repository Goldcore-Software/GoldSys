using System.Drawing;
using Cosmos.System;
using Cosmos.System.Graphics;
using GoldSysKernel.Core.CS;
using IL2CPU.API.Attribs;
using Console = System.Console;

namespace GoldSysKernel.GSystem
{
    internal class GSManager
    {
        public static Canvas screen;
        public static Color desktopBackground = Color.DodgerBlue;
        public static Color taskbarColor = Color.Goldenrod;
        public static bool graphicsMode = false;
        [ManifestResourceStream(ResourceName = "GoldSysKernel.Resources.cursor.bmp")]
        public static byte[] cursorbytes;
        public static Image cursor;
        public static List<Window> Windows = new List<Window>();
        public static void Initialize()
        {
            cursor = new Bitmap(cursorbytes);
        }
        public static void ChangeToGraphicsMode()
        {
            screen = FullScreenCanvas.GetFullScreenCanvas(new Mode(640, 480, ColorDepth.ColorDepth32));
            screen.Clear(desktopBackground);
            screen.Display();
            graphicsMode = true;
            CSTerminal.DisplayTerminal = 2;
        }
        public static void ExitGraphicsMode()
        {
            screen.Disable();
            Console.Clear();
            graphicsMode = false;
            CSTerminal.DisplayTerminal = 0;
        }
        public static void Draw()
        {
            DrawDesktop();
            DrawCursor();
            screen.Display();
            if (MouseManager.MouseState == MouseState.Left)
            {
                if ((MouseManager.X >= 610) && (MouseManager.Y <= 30))
                {
                    ExitGraphicsMode();
                }
            }
        }
        public static void DrawDesktop()
        {
            screen.Clear(desktopBackground);
            screen.DrawFilledRectangle(taskbarColor, 0, 0, 640, 30);
            screen.DrawFilledRectangle(Color.Red,610,0,30,30);
        }
        public static void DrawCursor()
        {
            screen.DrawImage(cursor, (int)MouseManager.X, (int)MouseManager.Y);
        }
    }
}
