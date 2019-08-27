﻿using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Story_One_Coube
{
    class Program
    {
        static RenderWindow mainWindow;

        public static uint height = 720;
        public static uint width = 1280;

        static Character character;

        static Character.Moves moveNow = Character.Moves.STOP;

        static void Main(string[] args)
        {
            mainWindow = new RenderWindow(new SFML.Window.VideoMode(width, height), "Story of one Cube");
            mainWindow.SetVerticalSyncEnabled(true);
            mainWindow.Closed += MainWindow_Closed;
            mainWindow.KeyPressed += MainWindow_KeyPressed;
            mainWindow.KeyReleased += MainWindow_KeyReleased;

            character = new Character();

            while (mainWindow.IsOpen)
            {
                mainWindow.DispatchEvents();

                mainWindow.Clear(Color.Black);

                character.Update(moveNow);

                character.Draw(mainWindow);

                mainWindow.Display();
            }
        }

        private static void MainWindow_KeyReleased(object sender, SFML.Window.KeyEventArgs e)
        {
            switch (e.Code)
            {
                case SFML.Window.Keyboard.Key.Left: { moveNow = Character.Moves.STOP; return; }

                case SFML.Window.Keyboard.Key.Right: { moveNow = Character.Moves.STOP; return; }
            }
        }

        private static void MainWindow_KeyPressed(object sender, SFML.Window.KeyEventArgs e)
        {
            switch (e.Code)
            {
                case SFML.Window.Keyboard.Key.Escape: { mainWindow.Close(); return; }

                case SFML.Window.Keyboard.Key.Space: { character.Jump(); return; }

                case SFML.Window.Keyboard.Key.Left: { moveNow = Character.Moves.LEFT; return; }

                case SFML.Window.Keyboard.Key.Right: { moveNow = Character.Moves.RIGHT; return; }
            }
        }

        private static void MainWindow_Closed(object sender, EventArgs e)
        {
            mainWindow.Close();
        }
    }
}
