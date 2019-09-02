﻿using SFML.Graphics;
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

        public static int Score = 0;

        public static Level levelNow = new Level1();

        static bool mainCharIsDead = false;

        enum windowMode { Menu, Game, Dead }

        static windowMode windowModeNow = windowMode.Dead;

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

            MainCharacter = Character.SpawnCharacter(mainCharacterHP, 46, 46, new Point(WidthWindow / 2, HeightWindow / 2));

            while (mainWindow.IsOpen)
            {
                mainWindow.DispatchEvents();

                mainWindow.Clear(BackgroundColorWindow);

                if (windowModeNow == windowMode.Game)
                {
                    TextureObjects.Clear();

                    levelNow.Draw(mainWindow);

                    CharacterEvents.UpdateChar(MainCharacter);

                    CharacterEvents.UpdateMainChar(moveNow, MainCharacter);

                    CharacterEvents.Draw(mainWindow, MainCharacter);

                    foreach (var enemy in Enemies.ToArray())
                    {
                        CharacterEvents.UpdateChar(enemy);
                        CharacterEvents.UpdateEnemy(enemy);
                        CharacterEvents.Draw(mainWindow, enemy);
                    }
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
            if (windowModeNow == windowMode.Game)
            {
                CharacterEvents.Shoot(MainCharacter, new Point(e.X, e.Y));
            }

            if(windowModeNow == windowMode.Dead)
            {
                if(deadScreenChooseNow == deadScreenChoose.Exit)
                {
                    MainWindow_Closed(new object(), new EventArgs());
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
                    case Keyboard.Key.A: { if (moveNow == CharacterEvents.Moves.LEFT) moveNow = CharacterEvents.Moves.STOP; return; }

                    case Keyboard.Key.D: { if (moveNow == CharacterEvents.Moves.RIGHT) moveNow = CharacterEvents.Moves.STOP; return; }
                }
            }
        }

        private static void MainWindow_KeyPressed(object sender, KeyEventArgs e)
        {
            if (windowModeNow == windowMode.Game)
            {
                if (e.Control) switch (e.Code)
                    {
                        case Keyboard.Key.H: { CharacterEvents.Hit(MainCharacter, 10); return; }
                        case Keyboard.Key.S:
                            {
                                Character enemy = Character.SpawnCharacter(100, 46, 46, new Point(random.Next((int)WidthWindow), random.Next((int)HeightWindow)));
                                Enemies.Add(enemy);
                                return;
                            }
                        case Keyboard.Key.P: { Score += 100; return; }
                    }

                switch (e.Code)
                {
                    case Keyboard.Key.Escape: { mainWindow.Close(); return; }

                    case Keyboard.Key.Space: { CharacterEvents.Jump(MainCharacter); return; }

                    case Keyboard.Key.A: { moveNow = CharacterEvents.Moves.LEFT; return; }

                    case Keyboard.Key.D: { moveNow = CharacterEvents.Moves.RIGHT; return; }
                }
            }
        }

        private static void MainWindow_Closed(object sender, EventArgs e)
        {
            mainWindow.Close();
        }
    }
}
