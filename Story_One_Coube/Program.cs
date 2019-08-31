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
using Story_One_Coube.Models.Scene;

namespace Story_One_Coube
{
    /// <summary>
    /// TODO Enemies(constructor). +-
    /// TODO Add AI for enemies. +-
    /// TODO Platforms.
    /// TODO Textures.
    /// TODO Main menu.
    /// TODO Death screen.
    /// TODO Score.
    /// TODO Win screen.
    /// TODO Some levels.
    /// TODO Boss
    /// </summary>

    class Program
    {
        static RenderWindow mainWindow;

        public static uint HeightWindow = 720;
        public static uint WidthWindow = 1280;
        public static Color BackgroundColorWindow = new Color(78, 180, 217);

        public static Character MainCharacter;
        static double mainCharacterHP = 100;
        static CharacterEvents.Moves moveNow = CharacterEvents.Moves.STOP;

        public static List<Character> Enemies = new List<Character>();

        public static Point LastMousePosition = new Point(1280, 720);

        static Random random = new Random();

        public static List<RectangleShape> TextureObjects = new List<RectangleShape>();

        static void Main(string[] args)
        {
            mainWindow = new RenderWindow(new VideoMode(WidthWindow, HeightWindow), "Story of one Cube");
            mainWindow.SetVerticalSyncEnabled(true);
            mainWindow.Closed += MainWindow_Closed;
            mainWindow.KeyPressed += MainWindow_KeyPressed;
            mainWindow.KeyReleased += MainWindow_KeyReleased;
            mainWindow.MouseMoved += MainWindow_MouseMoved;
            mainWindow.MouseButtonPressed += MainWindow_MouseButtonPressed;


            MainCharacter = new Character(mainCharacterHP, 46, 46, new Point(WidthWindow / 2, HeightWindow / 2));

            while (mainWindow.IsOpen)
            {
                mainWindow.DispatchEvents();

                mainWindow.Clear(BackgroundColorWindow);

                TextureObjects.Clear();

                Level1.InitialLevel(mainWindow);

                CharacterEvents.UpdateChar(MainCharacter);

                CharacterEvents.UpdateMainChar(moveNow, MainCharacter);

                CharacterEvents.Draw(mainWindow, MainCharacter);

                foreach(var enemy in Enemies.ToArray())
                {
                    CharacterEvents.UpdateChar(enemy);
                    CharacterEvents.UpdateEnemy(enemy);
                    CharacterEvents.Draw(mainWindow, enemy);
                }

                mainWindow.Display();
            }
        }

        private static void MainWindow_MouseButtonPressed(object sender, MouseButtonEventArgs e)
        {
            CharacterEvents.Shoot(MainCharacter, new Point(e.X, e.Y));
        }

        private static void MainWindow_MouseMoved(object sender, MouseMoveEventArgs e)
        {
            LastMousePosition.X = e.X;
            LastMousePosition.Y = e.Y;
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
                    case Keyboard.Key.H: { CharacterEvents.Hit(MainCharacter, 10); return; }
                    case Keyboard.Key.S: { Enemies.Add(new Character(100, 46, 46, new Point(random.Next((int)WidthWindow), random.Next((int)HeightWindow)))); return; }
                }

            switch (e.Code)
            {
                case Keyboard.Key.Escape: { mainWindow.Close(); return; }

                case Keyboard.Key.Space: { CharacterEvents.Jump(MainCharacter); return; }

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
