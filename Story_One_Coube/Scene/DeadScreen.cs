using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using SFML.Audio;
using Story_One_Coube.Models;

namespace Story_One_Coube.Scene
{
    static class DeadScreen
    {
        static Sprite preDeadScreenWords;

        static Texture exitTexture;
        static Sprite exitSprite;

        static Texture mainMenuTexture;
        static Sprite mainMenuSprite;

        static Texture restartTexture;
        static Sprite restartSprite;

        static Texture continueTexture;
        static Sprite continueSprite;

        static double wordAnimationTime;
        static DateTime timeMow;

        public enum DeadScreenChoose { None, Restart, MainMenu, Exit, Continue }

        public static DeadScreenChoose DeadScreenChooseNow = DeadScreenChoose.None;

        public static void Init(RenderWindow window)
        {
            wordAnimationTime = 3;
            timeMow = DateTime.Now;

            preDeadScreenWords = new Sprite(new Texture("../../Texturs/PreDeadScreen.png", new IntRect(0, 96, 597, 84)));
            preDeadScreenWords.Origin = new Vector2f(preDeadScreenWords.Texture.Size.X / 2, preDeadScreenWords.Texture.Size.Y / 2);
            preDeadScreenWords.Position = new Vector2f(window.Size.X / 2, window.Size.Y / 2);
            preDeadScreenWords.Scale = new Vector2f((float)0, (float)0);

            exitTexture = new Texture("../../Texturs/DeadScreenText.png", new IntRect(60, 13, 80, 27));
            exitSprite = new Sprite(exitTexture);
            exitSprite.Origin = new Vector2f(exitTexture.Size.X / 2, exitTexture.Size.Y / 2);
            exitSprite.Position = new Vector2f(window.Size.X / 2, window.Size.Y / 2 + 35);

            mainMenuTexture = new Texture("../../Texturs/DeadScreenText.png", new IntRect(6, 89, 189, 27));
            mainMenuSprite = new Sprite(mainMenuTexture);
            mainMenuSprite.Origin = new Vector2f(mainMenuTexture.Size.X / 2, mainMenuTexture.Size.Y / 2);
            mainMenuSprite.Position = new Vector2f(window.Size.X / 2, window.Size.Y / 2);

            restartTexture = new Texture("../../Texturs/DeadScreenText.png", new IntRect(25, 178, 149, 27));
            restartSprite = new Sprite(restartTexture);
            restartSprite.Origin = new Vector2f(restartTexture.Size.X / 2, restartTexture.Size.Y / 2);
            restartSprite.Position = new Vector2f(window.Size.X / 2, window.Size.Y / 2 - 35);

            continueTexture = new Texture("../../Texturs/DeadScreenText.png", new IntRect(0, 237, 173, 30));
            continueSprite = new Sprite(continueTexture);
            continueSprite.Origin = new Vector2f(continueTexture.Size.X / 2, continueTexture.Size.Y / 2);
            continueSprite.Position = new Vector2f(window.Size.X / 2, window.Size.Y / 2 - 70);
        }

        public static void DrawAndUpdate(RenderWindow window)
        {
            if (Program.windowModeNow == Program.WindowMode.Dead)
            {
                while (wordAnimationTime > 0)
                {
                    if (preDeadScreenWords.Scale.X < 1 || preDeadScreenWords.Scale.Y < 1)
                    {
                        preDeadScreenWords.Scale = new Vector2f((float)(preDeadScreenWords.Scale.X + 0.01), (float)(preDeadScreenWords.Scale.Y + 0.01));
                    }

                    window.Draw(preDeadScreenWords);
                    wordAnimationTime -= (DateTime.Now - timeMow).TotalSeconds;
                    timeMow = DateTime.Now;
                    return;
                }
            }

            exitSprite.Color = Color.White;
            mainMenuSprite.Color = Color.White;
            restartSprite.Color = Color.White;
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

            if (restartSprite.GetGlobalBounds().Contains((int)Program.LastMousePosition.X, (int)Program.LastMousePosition.Y))
            {
                DeadScreenChooseNow = DeadScreenChoose.Restart;
                restartSprite.Color = Color.Red;
            }

            window.Draw(restartSprite);
            window.Draw(mainMenuSprite);
            window.Draw(exitSprite);

            if(Program.windowModeNow == Program.WindowMode.Pause)
            {
                continueSprite.Color = Color.White;

                if(continueSprite.GetGlobalBounds().Contains((int)Program.LastMousePosition.X, (int)Program.LastMousePosition.Y))
                {
                    DeadScreenChooseNow = DeadScreenChoose.Continue;
                    continueSprite.Color = Color.Red;
                }

                window.Draw(continueSprite);
            }
        }

        public static void Restart()
        {
            wordAnimationTime = 3;
            timeMow = DateTime.Now;
            preDeadScreenWords.Scale = new Vector2f(0, 0);
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

                case DeadScreenChoose.Restart:
                    {
                        Program.levelNow = Program.levelNow.RestartLevel();
                        Program.levelNow.LoadStuff();
                        return;
                    }

                case DeadScreenChoose.Continue:
                    {
                        Program.levelNow.Continue();
                        Program.windowModeNow = Program.WindowMode.Game;
                        DeadScreenChooseNow = DeadScreenChoose.None;
                        return;
                    }
            }
        }

        public static void LoadStuff()
        {
            Program.musicNow.Stop();
            Program.musicNow = Musics.DeadScreen;
            Program.musicNow.Play();
        }
    }
}
