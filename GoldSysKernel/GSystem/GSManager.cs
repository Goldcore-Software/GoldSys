﻿using System.Collections.Generic;
using System.Drawing;
using Cosmos.System;
using Cosmos.System.Graphics;
using GoldSysKernel.Core.CSystem;
using IL2CPU.API.Attribs;

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
        public static int ActiveWindow = 0;
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
            CSTerminal.Clear();
            graphicsMode = false;
            CSTerminal.DisplayTerminal = 0;
        }
        public static void Draw()
        {
            DrawDesktop();
            DrawCursor();
            foreach (var wind in Windows)
            {
                wind.Draw();
            }
            if (Windows.Count != 0 && Windows.Count >= ActiveWindow+1)
            {
                // yes I do call Draw() twice
                // this is to make sure that the active window is on top
                // I should probably make it not call Draw() the first time but eh
                Windows[ActiveWindow].Draw();
                Windows[ActiveWindow].Run();
            }
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