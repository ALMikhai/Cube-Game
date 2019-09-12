using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Story_One_Coube.Scene
{
    static class Background
    {
        static Sprite BackgroundMainMenu;
        static Sprite BackgroundLevel1;
        static double timeToOneFrame;
        static DateTime timeNow;

        static Sprite backgroundNow;

        public static bool IsLoaded
        {
            get;
            private set;
        }

        public static void Init(RenderWindow window)
        {
            BackgroundMainMenu = new Sprite(new Texture("../../Texturs/Backgrounds/0.png"));
            BackgroundMainMenu.Scale = new Vector2f((float)Program.WidthWindow / (float)BackgroundMainMenu.Texture.Size.X, (float)Program.HeightWindow / (float)BackgroundMainMenu.Texture.Size.Y);
            BackgroundMainMenu.Origin = new Vector2f(BackgroundMainMenu.Texture.Size.X / 2, BackgroundMainMenu.Texture.Size.Y / 2);
            BackgroundMainMenu.Position = new Vector2f(window.Size.X / 2, window.Size.Y / 2);

            BackgroundLevel1 = new Sprite(new Texture("../../Texturs/Backgrounds/3.png"));
            BackgroundLevel1.Scale = new Vector2f((float)Program.WidthWindow / (float)BackgroundLevel1.Texture.Size.X, (float)Program.HeightWindow / (float)BackgroundLevel1.Texture.Size.Y);
            BackgroundLevel1.Origin = new Vector2f(BackgroundLevel1.Texture.Size.X / 2, BackgroundLevel1.Texture.Size.Y / 2);
            BackgroundLevel1.Position = new Vector2f(window.Size.X / 2, window.Size.Y / 2);

            timeToOneFrame = 1;
            timeNow = DateTime.Now;

            IsLoaded = false;
        }

        public static void Draw(RenderWindow window)
        {
            if (backgroundNow.Scale.Y < (float)Program.HeightWindow / (float)backgroundNow.Texture.Size.Y)
            {
                if ((DateTime.Now - timeNow).TotalSeconds >= timeToOneFrame)
                {
                    backgroundNow.Scale = new Vector2f((float)Program.WidthWindow / (float)backgroundNow.Texture.Size.X, (float)(backgroundNow.Scale.Y + 0.025));
                }
            }
            else
            {
                IsLoaded = true;
            }

            window.Draw(backgroundNow);
        }

        public static void Set(int numLevel)
        {
            switch (numLevel)
            {
                case 0:
                    {
                        backgroundNow = BackgroundMainMenu;
                        break;
                    }
                case 1:
                    {
                        backgroundNow = BackgroundLevel1;
                        break;
                    }
            }

            backgroundNow.Scale = new Vector2f((float)Program.WidthWindow / (float)BackgroundMainMenu.Texture.Size.X, 0);
            timeNow = DateTime.Now;

            IsLoaded = false;
        }
    }
}
