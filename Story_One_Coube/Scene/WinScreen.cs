using SFML.Graphics;
using SFML.System;
using Story_One_Coube.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Story_One_Coube.Scene
{
    class WinScreen
    {
        static Sprite preWinScreenWords;

        static Texture exitTexture;
        static Sprite exitSprite;

        static Texture mainMenuTexture;
        static Sprite mainMenuSprite;

        static double wordAnimationTime;
        static DateTime timeMow;

        public enum DeadScreenChoose { None, MainMenu, Exit }

        public static DeadScreenChoose DeadScreenChooseNow = DeadScreenChoose.None;

        public static void Init(RenderWindow window)
        {
            wordAnimationTime = 3;
            timeMow = DateTime.Now;

            preWinScreenWords = new Sprite(new Texture("../../Texturs/WinScreen.png"));
            preWinScreenWords.Origin = new Vector2f(preWinScreenWords.Texture.Size.X / 2, preWinScreenWords.Texture.Size.Y / 2);
            preWinScreenWords.Position = new Vector2f(window.Size.X / 2, window.Size.Y / 2);
            preWinScreenWords.Scale = new Vector2f((float)0, (float)0);

            exitTexture = new Texture("../../Texturs/DeadScreenText.png", new IntRect(60, 13, 80, 27));
            exitSprite = new Sprite(exitTexture);
            exitSprite.Origin = new Vector2f(exitTexture.Size.X / 2, exitTexture.Size.Y / 2);
            exitSprite.Position = new Vector2f(window.Size.X / 2, window.Size.Y / 2 + 35);

            mainMenuTexture = new Texture("../../Texturs/DeadScreenText.png", new IntRect(6, 89, 189, 27));
            mainMenuSprite = new Sprite(mainMenuTexture);
            mainMenuSprite.Origin = new Vector2f(mainMenuTexture.Size.X / 2, mainMenuTexture.Size.Y / 2);
            mainMenuSprite.Position = new Vector2f(window.Size.X / 2, window.Size.Y / 2);
        }

        public static void DrawAndUpdate(RenderWindow window)
        {
            if (Program.windowModeNow == Program.WindowMode.Win)
            {
                while (wordAnimationTime > 0)
                {
                    if (preWinScreenWords.Scale.X < 1 || preWinScreenWords.Scale.Y < 1)
                    {
                        preWinScreenWords.Scale = new Vector2f((float)(preWinScreenWords.Scale.X + 0.01), (float)(preWinScreenWords.Scale.Y + 0.01));
                    }

                    window.Draw(preWinScreenWords);
                    wordAnimationTime -= (DateTime.Now - timeMow).TotalSeconds;
                    timeMow = DateTime.Now;
                    return;
                }
            }

            exitSprite.Color = Color.White;
            mainMenuSprite.Color = Color.White;
            DeadScreenChooseNow = DeadScreenChoose.None;

            if (exitSprite.GetGlobalBounds().Contains((int)Program.LastMousePosition.X, (int)Program.LastMousePosition.Y))
            {
                DeadScreenChooseNow = DeadScreenChoose.Exit;
                exitSprite.Color = Color.Red;
            }

            if (mainMenuSprite.GetGlobalBounds().Contains((int)Program.LastMousePosition.X, (int)Program.LastMousePosition.Y))
            {
                DeadScreenChooseNow = DeadScreenChoose.MainMenu;
                mainMenuSprite.Color = Color.Red;
            }

            window.Draw(mainMenuSprite);
            window.Draw(exitSprite);
        }

        public static void Restart()
        {
            wordAnimationTime = 3;
            timeMow = DateTime.Now;
            preWinScreenWords.Scale = new Vector2f(0, 0);
        }

        public static void Click()
        {
            switch (DeadScreenChooseNow)
            {
                case DeadScreenChoose.Exit:
                    {
                        Program.MainWindow_Closed(new object(), new EventArgs());
                        return;
                    }

                case DeadScreenChoose.MainMenu:
                    {
                        Background.Set(0);
                        Program.musicNow.Stop();
                        Program.musicNow = Musics.MainMenu;
                        Program.musicNow.Play();
                        Program.windowModeNow = Program.WindowMode.Menu;
                        DeadScreenChooseNow = DeadScreenChoose.None;
                        return;
                    }
            }
        }

        public static void LoadStuff()
        {
            Program.musicNow.Stop();
            Program.musicNow = Musics.WinScreen;
            Program.musicNow.Play();
        }

        public static void Display()
        {
            Restart();
            LoadStuff();
            Program.windowModeNow = Program.WindowMode.Win;
        }
    }
}
