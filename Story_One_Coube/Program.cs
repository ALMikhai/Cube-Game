using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Story_One_Coube.Models;

namespace Story_One_Coube
{
    class Program
    {
        static RenderWindow mainWindow;

        public static uint height = 720;
        public static uint width = 1280;
        public static Color backgroundColor = new Color(78, 180, 217);

        static Character character;

        static Character.Moves moveNow = Character.Moves.STOP;

        static Point lastMousePosition = new Point(1280, 720);

        static void Main(string[] args)
        {
            mainWindow = new RenderWindow(new SFML.Window.VideoMode(width, height), "Story of one Cube");
            mainWindow.SetVerticalSyncEnabled(true);
            mainWindow.Closed += MainWindow_Closed;
            mainWindow.KeyPressed += MainWindow_KeyPressed;
            mainWindow.KeyReleased += MainWindow_KeyReleased;
            mainWindow.MouseMoved += MainWindow_MouseMoved;
            mainWindow.MouseButtonPressed += MainWindow_MouseButtonPressed;

            character = new Character();

            while (mainWindow.IsOpen)
            {
                mainWindow.DispatchEvents();

                mainWindow.Clear(backgroundColor);

                character.Update(moveNow, lastMousePosition);

                character.Draw(mainWindow);

                mainWindow.Display();
            }
        }

        private static void MainWindow_MouseButtonPressed(object sender, SFML.Window.MouseButtonEventArgs e)
        {
            character.gunNow.Shoot(new Point(e.X, e.Y));
        }

        private static void MainWindow_MouseMoved(object sender, SFML.Window.MouseMoveEventArgs e)
        {
            lastMousePosition.X = e.X;
            lastMousePosition.Y = e.Y;
        }

        private static void MainWindow_KeyReleased(object sender, SFML.Window.KeyEventArgs e)
        {
            switch (e.Code)
            {
                case SFML.Window.Keyboard.Key.A: { if(moveNow == Character.Moves.LEFT) moveNow = Character.Moves.STOP; return; }

                case SFML.Window.Keyboard.Key.D: { if(moveNow == Character.Moves.RIGHT) moveNow = Character.Moves.STOP; return; }
            }
        }

        private static void MainWindow_KeyPressed(object sender, SFML.Window.KeyEventArgs e)
        {
            switch (e.Code)
            {
                case SFML.Window.Keyboard.Key.Escape: { mainWindow.Close(); return; }

                case SFML.Window.Keyboard.Key.Space: { character.Jump(); return; }

                case SFML.Window.Keyboard.Key.A: { moveNow = Character.Moves.LEFT; return; }

                case SFML.Window.Keyboard.Key.D: { moveNow = Character.Moves.RIGHT; return; }
            }
        }

        private static void MainWindow_Closed(object sender, EventArgs e)
        {
            mainWindow.Close();
        }
    }
}
