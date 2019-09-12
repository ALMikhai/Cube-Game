using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using Story_One_Coube.Models;

namespace Story_One_Coube.Scene
{
    static class LevelChoosePage
    {
        static List<KeyValuePair<int, Sprite>> levelSprites = new List<KeyValuePair<int, Sprite>>();
        static Sprite cancelSprite = new Sprite();

        static Dictionary<int, Level> levels;

        static int chooseLevel = 0;

        enum chooseMode { None, Cancel, Level1, Level2, Level3 }

        static chooseMode chooseModeNow = new chooseMode();

        public static void Init(RenderWindow window)
        {
            int startLevelList = 400;

            levels = new Dictionary<int, Level>
            {
                { 1, new Level1() },
                { 2, new Level1() },
                { 3, new Level1() },
                { 4, new Level1() }
            };

            for(var i = 1; i <= 4; i++)
            {
                Sprite sprite = new Sprite(new Texture("../../Texturs/LevelChoosePage/" + i.ToString() + ".png"));
                sprite.Origin = new Vector2f(sprite.Texture.Size.X / 2, sprite.Texture.Size.Y / 2);
                sprite.Position = new Vector2f(window.Size.X / 2, startLevelList);

                startLevelList += 55;

                levelSprites.Add(new KeyValuePair<int, Sprite>(i, sprite));
            }

            cancelSprite = new Sprite(new Texture("../../Texturs/LevelChoosePage/Cancel.png"));
            cancelSprite.Position = new Vector2f(30, window.Size.Y - 70);
        }

        public static void DrawAndUpdate(RenderWindow window)
        {
            chooseModeNow = chooseMode.None;

            foreach(var level in levelSprites)
            {
                level.Value.Color = Color.White;
                if (level.Value.GetGlobalBounds().Contains((int)Program.LastMousePosition.X, (int)Program.LastMousePosition.Y))
                {
                    level.Value.Color = Color.Red;
                    chooseLevel = level.Key;
                    chooseModeNow = chooseMode.Level1;
                }
            }

            cancelSprite.Color = Color.White;

            if(cancelSprite.GetGlobalBounds().Contains((int)Program.LastMousePosition.X, (int)Program.LastMousePosition.Y))
            {
                cancelSprite.Color = Color.Red;
                chooseLevel = 0;
                chooseModeNow = chooseMode.Cancel;
            }

            foreach(var level in levelSprites)
            {
                window.Draw(level.Value);
            }

            window.Draw(cancelSprite);
        }

        public static void Click()
        {
            switch (chooseModeNow)
            {
                case chooseMode.Cancel:
                    {
                        Program.windowModeNow = Program.WindowMode.Menu;
                        return;
                    }

                case chooseMode.Level1:
                    {
                        Program.levelNow = levels[chooseLevel];
                        Program.levelNow = Program.levelNow.RestartLevel();
                        Program.windowModeNow = Program.WindowMode.Game;
                        Background.Set(1);
                        Program.musicNow.Stop();
                        Program.musicNow = Musics.Level1;
                        Program.musicNow.Play();
                        return;
                    }
            }
        }
    }
}
