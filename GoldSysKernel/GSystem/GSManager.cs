using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using Cosmos.Core.Memory;
using Cosmos.System;
using Cosmos.System.Graphics;
using Cosmos.System.Graphics.Fonts;
using GoldSysKernel.Core.CSystem;
using IL2CPU.API.Attribs;

namespace GoldSysKernel.GSystem
{
    internal class GSManager
    {
        public static VBECanvas screen;
        public static Color desktopBackground = Color.DodgerBlue;
        public static Color taskbarColor = Color.DarkViolet;
        public static bool graphicsMode = false;
        [ManifestResourceStream(ResourceName = "GoldSysKernel.Resources.cursor.bmp")]
        public static byte[] cursorbytes;
        public static Image cursor;
        public static List<Window> Windows = new List<Window>();
        public static int ActiveWindow = 0;
        public static int FPS = 0;

        public static int LastS = -1;
        public static int Ticken = 0;

        public static void UpdateFPS()
        {
            if (LastS == -1)
            {
                LastS = DateTime.UtcNow.Second;
            }
            if (DateTime.UtcNow.Second - LastS != 0)
            {
                if (DateTime.UtcNow.Second > LastS)
                {
                    FPS = Ticken / (DateTime.UtcNow.Second - LastS);
                }
                LastS = DateTime.UtcNow.Second;
                Ticken = 0;
            }
            Ticken++;
        }
        public static void Initialize()
        {
            cursor = new Bitmap(cursorbytes);
        }
        public static void ChangeToGraphicsMode()
        {
            screen = (VBECanvas)FullScreenCanvas.GetFullScreenCanvas(new Mode(640, 480, ColorDepth.ColorDepth32));
            screen.Clear(desktopBackground);
            screen.Display();
            graphicsMode = true;
            CSTerminal.DisplayTerminal = 2;
            GSMessageBox.ShowMsgBox("Message box test","Message Box!");
        }
        public static void ExitGraphicsMode()
        {
            Windows.Clear();
            screen.Disable();
            CSTerminal.Clear();
            graphicsMode = false;
            CSTerminal.DisplayTerminal = 0;
        }
        public static void Draw()
        {
            UpdateFPS();
            DrawDesktop();
            
            int i = 0;
            foreach (var wind in Windows)
            {
                if (wind.Closed)
                {
                    // delete the Window that has closed
                    Windows.Remove(wind);
                }
                else
                {
                    // draw the actual window
                    wind.DrawTitleBar();
                    // draw all controls of the window
                    wind.Draw();
                }
                i++;
            }
            if (Windows.Count != 0 && Windows.Count >= ActiveWindow+1)
            {
                // yes I do call Draw() twice
                // this is to make sure that the active window is on top
                // I should probably make it not call Draw() the first time but eh
                Windows[ActiveWindow].DrawTitleBar();
                Windows[ActiveWindow].Draw();
                Windows[ActiveWindow].Run();
            }
            DrawCursor();
            screen.Display();
            if (MouseManager.MouseState == MouseState.Left)
            {
                if ((MouseManager.X >= 610) && (MouseManager.Y <= 30))
                {
                    ExitGraphicsMode();
                }
            }
            Heap.Collect();
        }
        public static void DrawDesktop()
        {
            screen.Clear(desktopBackground);
            screen.DrawFilledRectangle(taskbarColor, 0, 0, 640, 30);
            screen.DrawFilledRectangle(Color.Red,610,0,30,30);
            screen.DrawString("GoldSys | " + "FPS: " + FPS.ToString(), PCScreenFont.Default,Color.White,5,8);
        }
        public static void DrawCursor()
        {
            screen.DrawImageAlpha(cursor, (int)MouseManager.X, (int)MouseManager.Y);
        }
        public static void OpenWindow(Window window)
        {
            window.Open();
            Windows.Add(window);
            ActiveWindow = Windows.Count - 1;
        }
    }
}
