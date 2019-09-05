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
    /// TOFO Use minor scale for mirror gun sprite.
    /// TODO Main menu.
    /// TODO Win screen.
    /// TODO Some levels.
    /// TODO Boss
    /// TODO Animation for main char.
    /// TODO Add gun sprite and bullet sprite.
    /// </summary>

    class Program
    {
        public static RenderWindow MainWindow;

        public static uint HeightWindow = 1080;
        public static uint WidthWindow = 1920;
        public static Color BackgroundColorWindow = new Color(78, 180, 217);

        public static Point LastMousePosition = new Point(1280, 720);

        static Random random = new Random();

        public static Level levelNow = new Level1();

        public static Texture Background = new Texture("../../Texturs/Background.png");
        public static Sprite BackgroundSprite = new Sprite(Background);

        public enum WindowMode { Menu, Game, Dead }

        public static WindowMode windowModeNow = WindowMode.Menu;

        public enum DeadScreenChoose { None, Restart, MainMenu, Exit }

        public static DeadScreenChoose DeadScreenChooseNow = DeadScreenChoose.None;

        public enum MainMenuChoose { None, Story, Arena, Exit }

        public static MainMenuChoose MainMenuChooseNow = MainMenuChoose.None;

        static void Main(string[] args)
        {
            CharacterMovesAnimation.Init();

            MainWindow = new RenderWindow(new VideoMode(WidthWindow, HeightWindow), "Story of one Cube", Styles.None);
            MainWindow.SetVerticalSyncEnabled(true);
            MainWindow.Closed += MainWindow_Closed;
            MainWindow.KeyPressed += MainWindow_KeyPressed;
            MainWindow.KeyReleased += MainWindow_KeyReleased;
            MainWindow.MouseMoved += MainWindow_MouseMoved;
            MainWindow.MouseButtonPressed += MainWindow_MouseButtonPressed;

            DeadScreen.Init(MainWindow);
            MainMenu.Init(MainWindow);

            BackgroundSprite.Scale = new Vector2f((float)WidthWindow / (float)Background.Size.X, (float)HeightWindow / (float)Background.Size.Y);

            while (MainWindow.IsOpen)
            {
                MainWindow.DispatchEvents();

                MainWindow.Clear();

                MainWindow.Draw(BackgroundSprite);

                if(windowModeNow == WindowMode.Menu)
                {
                    MainMenu.DrawAndUpdate(MainWindow);
                }

                if (windowModeNow == WindowMode.Game)
                {
                    levelNow.Update(MainWindow);
                    levelNow.Draw(MainWindow);
                }

                if (windowModeNow == WindowMode.Dead)
                {
                    levelNow.Draw(MainWindow);
                    DeadScreen.DrawAndUpdate(MainWindow);
                }

                MainWindow.Display();
            }
        }

        private static void MainWindow_MouseButtonPressed(object sender, MouseButtonEventArgs e)
        {

            switch (windowModeNow)
            {
                case WindowMode.Game:
                    {
                        CharacterEvents.Shoot(Program.levelNow.MainCharacter, new Point(e.X, e.Y));
                        return;
                    }

                case WindowMode.Dead:
                    {
                        switch (DeadScreenChooseNow)
                        {
                            case DeadScreenChoose.Exit:
                                {
                                    MainWindow_Closed(new object(), new EventArgs());
                                    return;
                                }

                            case DeadScreenChoose.MainMenu:
                                {
                                    return;
                                }

                            case DeadScreenChoose.Restart:
                                {
                                    levelNow = levelNow.RestartLevel();
                                    return;
                                }
                        }
                        return;
                    }

                case WindowMode.Menu:
                    {
                        switch (MainMenuChooseNow)
                        {
                            case MainMenuChoose.Exit:
                                {
                                    MainWindow_Closed(new object(), new EventArgs());
                                    return;
                                }
                        }
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
            if (windowModeNow == WindowMode.Game)
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
            if (windowModeNow == WindowMode.Game)
            {
                if (e.Control) switch (e.Code)
                    {
                        case Keyboard.Key.H: { CharacterEvents.Hit(Program.levelNow.MainCharacter, 10); return; }
                        case Keyboard.Key.S:
                            {
                                Character enemy = Character.SpawnCharacter(100, new Point(random.Next((int)WidthWindow), random.Next((int)HeightWindow)), CharacterMovesAnimation.StandEnemyTexture);
                                Program.levelNow.Enemies.Add(enemy);
                                return;
                            }
                        case Keyboard.Key.P: { Program.levelNow.Score += 100; return; }
                    }

                switch (e.Code)
                {
                    case Keyboard.Key.Escape: { MainWindow.Close(); return; }

                    case Keyboard.Key.Space: { CharacterMovesAnimation.JumpFinished = false; CharacterEvents.Jump(Program.levelNow.MainCharacter); return; }

                    case Keyboard.Key.A: { Program.levelNow.moveNow = CharacterEvents.Moves.LEFT; return; }

                    case Keyboard.Key.D: { Program.levelNow.moveNow = CharacterEvents.Moves.RIGHT; return; }
                }
            }
        }

        private static void MainWindow_Closed(object sender, EventArgs e)
        {
            MainWindow.Close();
        }
    }
}
