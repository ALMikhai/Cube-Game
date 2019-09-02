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
using Story_One_Coube.Scene;

namespace Story_One_Coube
{
    /// <summary>
    /// TODO Fix opportunity shoot to main char if enemy can not hit him. (Доп.)
    /// TODO Main menu.
    /// TODO Death screen. (Add big tablet "You dead!!!").
    /// TODO Win screen.
    /// TODO Some levels.
    /// TODO Boss
    /// TODO Animation for main char.
    /// TODO Add gun sprite and bullet sprite.
    /// </summary>

    class Program
    {
        static RenderWindow mainWindow;

        public static uint HeightWindow = 720;
        public static uint WidthWindow = 1280;
        public static Color BackgroundColorWindow = new Color(78, 180, 217);

        public static Point LastMousePosition = new Point(1280, 720);

        static Random random = new Random();

        public static Level levelNow = new Level1();

        public enum windowMode { Menu, Game, Dead }

        public static windowMode windowModeNow = windowMode.Game;

        static DeadScreen deadScreen;

        public enum deadScreenChoose { None, Restart, MainMenu, Exit }

        public static deadScreenChoose deadScreenChooseNow = deadScreenChoose.None;

        static void Main(string[] args)
        {
            mainWindow = new RenderWindow(new VideoMode(WidthWindow, HeightWindow), "Story of one Cube", Styles.None);
            mainWindow.SetVerticalSyncEnabled(true);
            mainWindow.Closed += MainWindow_Closed;
            mainWindow.KeyPressed += MainWindow_KeyPressed;
            mainWindow.KeyReleased += MainWindow_KeyReleased;
            mainWindow.MouseMoved += MainWindow_MouseMoved;
            mainWindow.MouseButtonPressed += MainWindow_MouseButtonPressed;

            deadScreen = new DeadScreen(mainWindow);

            while (mainWindow.IsOpen)
            {
                mainWindow.DispatchEvents();

                mainWindow.Clear(BackgroundColorWindow);

                if (windowModeNow == windowMode.Game)
                {
                    levelNow.Draw(mainWindow);

                    levelNow.Update(mainWindow);
                }

                if (windowModeNow == windowMode.Dead)
                {
                    deadScreen.DrawAndUpdate(mainWindow);
                }

                mainWindow.Display();
            }
        }

        private static void MainWindow_MouseButtonPressed(object sender, MouseButtonEventArgs e)
        {

            switch (windowModeNow)
            {
                case windowMode.Game:
                    {
                        CharacterEvents.Shoot(Program.levelNow.MainCharacter, new Point(e.X, e.Y));
                        return;
                    }

                case windowMode.Dead:
                    {
                        switch (deadScreenChooseNow)
                        {
                            case deadScreenChoose.Exit:
                                {
                                    MainWindow_Closed(new object(), new EventArgs());
                                    return;
                                }

                            case deadScreenChoose.MainMenu:
                                {
                                    return;
                                }

                            case deadScreenChoose.Restart:
                                {
                                    levelNow = levelNow.RestartLevel();
                                    return;
                                }
                        }
                        return;
                    }

                case windowMode.Menu:
                    {
                        return;
                    }
            }
        }

        private static void MainWindow_MouseMoved(object sender, MouseMoveEventArgs e)
        {
            LastMousePosition.X = e.X;
            LastMousePosition.Y = e.Y;
        }

        private static void MainWindow_KeyReleased(object sender, KeyEventArgs e)
        {
            if (windowModeNow == windowMode.Game)
            {
                switch (e.Code)
                {
                    case Keyboard.Key.A: { if (Program.levelNow.moveNow == CharacterEvents.Moves.LEFT) Program.levelNow.moveNow = CharacterEvents.Moves.STOP; return; }

                    case Keyboard.Key.D: { if (Program.levelNow.moveNow == CharacterEvents.Moves.RIGHT) Program.levelNow.moveNow = CharacterEvents.Moves.STOP; return; }
                }
            }
        }

        private static void MainWindow_KeyPressed(object sender, KeyEventArgs e)
        {
            if (windowModeNow == windowMode.Game)
            {
                if (e.Control) switch (e.Code)
                    {
                        case Keyboard.Key.H: { CharacterEvents.Hit(Program.levelNow.MainCharacter, 10); return; }
                        case Keyboard.Key.S:
                            {
                                Character enemy = Character.SpawnCharacter(100, 46, 46, new Point(random.Next((int)WidthWindow), random.Next((int)HeightWindow)));
                                Program.levelNow.Enemies.Add(enemy);
                                return;
                            }
                        case Keyboard.Key.P: { Program.levelNow.Score += 100; return; }
                    }

                switch (e.Code)
                {
                    case Keyboard.Key.Escape: { mainWindow.Close(); return; }

                    case Keyboard.Key.Space: { CharacterEvents.Jump(Program.levelNow.MainCharacter); return; }

                    case Keyboard.Key.A: { Program.levelNow.moveNow = CharacterEvents.Moves.LEFT; return; }

                    case Keyboard.Key.D: { Program.levelNow.moveNow = CharacterEvents.Moves.RIGHT; return; }
                }
            }
        }

        private static void MainWindow_Closed(object sender, EventArgs e)
        {
            mainWindow.Close();
        }
    }
}
