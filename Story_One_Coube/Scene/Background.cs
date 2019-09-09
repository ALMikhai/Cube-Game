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
        static Texture background;
        static Sprite BackgroundSprite;
        static double timeToOneFrame;
        static DateTime timeNow;

        public static bool IsLoaded
        {
            get;
            private set;
        }

        public static void Init(RenderWindow window)
        {
            background = new Texture("../../Texturs/Backgrounds/0.png");
            BackgroundSprite = new Sprite(background);
            BackgroundSprite.Scale = new Vector2f((float)Program.WidthWindow / (float)background.Size.X, 0);
            BackgroundSprite.Origin = new Vector2f(BackgroundSprite.Texture.Size.X / 2, BackgroundSprite.Texture.Size.Y / 2);
            BackgroundSprite.Position = new Vector2f(window.Size.X / 2, window.Size.Y / 2);

            timeToOneFrame = 1;
            timeNow = DateTime.Now;

            IsLoaded = false;
        }

        public static void Draw(RenderWindow window)
        {
            if (BackgroundSprite.Scale.Y < (float)Program.HeightWindow / (float)background.Size.Y)
            {
                if ((DateTime.Now - timeNow).TotalSeconds >= timeToOneFrame)
                {
                    BackgroundSprite.Scale = new Vector2f((float)Program.WidthWindow / (float)background.Size.X, (float)(BackgroundSprite.Scale.Y + 0.025));
                }
            }
            else
            {
                IsLoaded = true;
            }

            window.Draw(BackgroundSprite);
        }
    }
}
