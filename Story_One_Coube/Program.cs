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
    /// TODO Enemies. +-
    /// TODO Add AI for enemies.
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
        static CharacterEvents.Moves moveNow = CharacterEvents.Moves.STOP;

        public static List<Character> Enemies = new List<Character>();

        public static Point lastMousePosition = new Point(1280, 720);

        static Random random = new Random();

        static void Main(string[] args)
        {
            mainWindow = new RenderWindow(new VideoMode(widthWindow, heightWindow), "Story of one Cube");
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

                CharacterEvents.UpdateMainChar(moveNow, mainCharacter);

                CharacterEvents.Draw(mainWindow, mainCharacter);

                foreach(var enemy in Enemies.ToArray())
                {
                    CharacterEvents.UpdateEnemy(enemy);
                    CharacterEvents.Draw(mainWindow, enemy);
                }

                mainWindow.Display();
            }
        }

        private static void MainWindow_MouseButtonPressed(object sender, MouseButtonEventArgs e)
        {
            CharacterEvents.Shoot(mainCharacter, new Point(e.X, e.Y));
        }

        private static void MainWindow_MouseMoved(object sender, MouseMoveEventArgs e)
        {
            lastMousePosition.X = e.X;
            lastMousePosition.Y = e.Y;
        }

        private static void MainWindow_KeyReleased(object sender, KeyEventArgs e)
        {
            switch (e.Code)
            {
                case Keyboard.Key.A: { if(moveNow == CharacterEvents.Moves.LEFT) moveNow = CharacterEvents.Moves.STOP; return; }

                case Keyboard.Key.D: { if(moveNow == CharacterEvents.Moves.RIGHT) moveNow = CharacterEvents.Moves.STOP; return; }
            }
        }

        private static void MainWindow_KeyPressed(object sender, KeyEventArgs e)
        {
            if (e.Control) switch (e.Code)
                {
                    case Keyboard.Key.H: { CharacterEvents.Hit(mainCharacter, 10); return; }
                    case Keyboard.Key.S: { Enemies.Add(new Character(100, 46, 46, new Point(random.Next((int)widthWindow), random.Next((int)heightWindow)))); return; }
                }

            switch (e.Code)
            {
                case Keyboard.Key.Escape: { mainWindow.Close(); return; }

                case Keyboard.Key.Space: { CharacterEvents.Jump(mainCharacter); return; }

                case Keyboard.Key.A: { moveNow = CharacterEvents.Moves.LEFT; return; }

                case Keyboard.Key.D: { moveNow = CharacterEvents.Moves.RIGHT; return; }
            }
        }

        private static void MainWindow_Closed(object sender, EventArgs e)
        {
            mainWindow.Close();
        }
    }
}
