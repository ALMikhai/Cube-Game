using SFML.Graphics;
using SFML.System;
using SFML.Window;
using SFML.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Story_One_Coube.Models;

namespace Story_One_Coube
{
    /// <summary>
    /// TODO Delet bullets.
    /// TODO Enemies. +-
    /// TODO Add AI for enemies.
    /// TODO Add opportunity get hit for enemy and main char. 
    /// TODO Platforms.
    /// TODO Textures.
    /// </summary>

    class Program
    {
        static RenderWindow mainWindow;

        public static uint heightWindow = 720;
        public static uint widthWindow = 1280;
        public static Color backgroundColorWindow = new Color(78, 180, 217);

        static Character mainCharacter;
        static double mainCharacterHP = 100;
        static Character.Moves moveNow = Character.Moves.STOP;

        static List<Enemy> Enemies = new List<Enemy>();

        public static Point lastMousePosition = new Point(1280, 720);

        static Random random = new Random();

        static void Main(string[] args)
        {
            mainWindow = new RenderWindow(new SFML.Window.VideoMode(widthWindow, heightWindow), "Story of one Cube");
            mainWindow.SetVerticalSyncEnabled(true);
            mainWindow.Closed += MainWindow_Closed;
            mainWindow.KeyPressed += MainWindow_KeyPressed;
            mainWindow.KeyReleased += MainWindow_KeyReleased;
            mainWindow.MouseMoved += MainWindow_MouseMoved;
            mainWindow.MouseButtonPressed += MainWindow_MouseButtonPressed;

            mainCharacter = new Character(mainCharacterHP, 46, 46, new Point(widthWindow / 2, heightWindow / 2));

            while (mainWindow.IsOpen)
            {
                mainWindow.DispatchEvents();

                mainWindow.Clear(backgroundColorWindow);

                mainCharacter.Update(moveNow);

                mainCharacter.Draw(mainWindow);

                foreach(var enemy in Enemies)
                {
                    enemy.Update();
                    enemy.Draw(mainWindow);
                }

                mainWindow.Display();
            }
        }

        private static void MainWindow_MouseButtonPressed(object sender, SFML.Window.MouseButtonEventArgs e)
        {
            CharacterEvents.Shoot(mainCharacter, new Point(e.X, e.Y));
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
            if (e.Control) switch (e.Code)
                {
                    case Keyboard.Key.H: { CharacterEvents.Hit(mainCharacter, 10); return; }
                    case Keyboard.Key.S: { Enemies.Add(new Enemy(100, 46, 46, new Point(random.Next((int)widthWindow), random.Next((int)heightWindow)))); return; }
                }

            switch (e.Code)
            {
                case SFML.Window.Keyboard.Key.Escape: { mainWindow.Close(); return; }

                case SFML.Window.Keyboard.Key.Space: { CharacterEvents.Jump(mainCharacter); return; }

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
