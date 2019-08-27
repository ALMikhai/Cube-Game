using SFML.Graphics;
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

        public static uint height = 800;
        public static uint width = 800;

        static Character character;

        static void Main(string[] args)
        {
            mainWindow = new RenderWindow(new SFML.Window.VideoMode(width, height), "Story of one Cube");
            mainWindow.SetVerticalSyncEnabled(true);
            mainWindow.Closed += MainWindow_Closed;
            mainWindow.KeyPressed += MainWindow_KeyPressed;
            character = new Character();

            while (mainWindow.IsOpen)
            {
                mainWindow.DispatchEvents();

                mainWindow.Clear(Color.Black);

                character.Update();

                character.Draw(mainWindow);

                mainWindow.Display();
            }
        }

        private static void MainWindow_KeyPressed(object sender, SFML.Window.KeyEventArgs e)
        {
            if(e.Code == SFML.Window.Keyboard.Key.Space)
            {
                character.Jump();
            }
        }

        private static void MainWindow_Closed(object sender, EventArgs e)
        {
            mainWindow.Close();
        }
    }
}
