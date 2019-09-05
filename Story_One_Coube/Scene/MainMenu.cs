using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using SFML.Audio;

namespace Story_One_Coube.Scene
{
    static class MainMenu
    {
        static Texture exitTexture;
        static Sprite exitSprite;

        static Texture storyModeTexture;
        static Sprite storyModeSprite;

        static Texture arenaModeTexture;
        static Sprite arenaModeSprite;

        public static void Init(RenderWindow window)
        {
            exitTexture = new Texture("../../Texturs/MainMenu.png", new IntRect(0, 0, 114, 42));
            exitSprite = new Sprite(exitTexture);
            exitSprite.Origin = new Vector2f(exitTexture.Size.X / 2, exitTexture.Size.Y / 2);
            exitSprite.Position = new Vector2f(window.Size.X / 2, window.Size.Y / 2 + 50);

            storyModeTexture = new Texture("../../Texturs/MainMenu.png", new IntRect(0, 60, 296, 39));
            storyModeSprite = new Sprite(storyModeTexture);
            storyModeSprite.Origin = new Vector2f(storyModeTexture.Size.X / 2, storyModeTexture.Size.Y / 2);
            storyModeSprite.Position = new Vector2f(window.Size.X / 2, window.Size.Y / 2 - 50);

            arenaModeTexture = new Texture("../../Texturs/MainMenu.png", new IntRect(0, 105, 300, 39));
            arenaModeSprite = new Sprite(arenaModeTexture);
            arenaModeSprite.Origin = new Vector2f(arenaModeTexture.Size.X / 2, arenaModeTexture.Size.Y / 2);
            arenaModeSprite.Position = new Vector2f(window.Size.X / 2, window.Size.Y / 2);
        }

        public static void DrawAndUpdate(RenderWindow window)
        {
            exitSprite.Color = Color.White;
            storyModeSprite.Color = Color.White;
            arenaModeSprite.Color = Color.White;
            Program.MainMenuChooseNow = Program.MainMenuChoose.None;

            if (exitSprite.GetGlobalBounds().Contains((int)Program.LastMousePosition.X, (int)Program.LastMousePosition.Y))
            {
                Program.MainMenuChooseNow = Program.MainMenuChoose.Exit;
                exitSprite.Color = Color.Red;
            }

            if (storyModeSprite.GetGlobalBounds().Contains((int)Program.LastMousePosition.X, (int)Program.LastMousePosition.Y))
            {
                Program.MainMenuChooseNow = Program.MainMenuChoose.Story;
                storyModeSprite.Color = Color.Red;
            }

            if (arenaModeSprite.GetGlobalBounds().Contains((int)Program.LastMousePosition.X, (int)Program.LastMousePosition.Y))
            {
                Program.MainMenuChooseNow = Program.MainMenuChoose.Arena;
                arenaModeSprite.Color = Color.Red;
            }

            window.Draw(exitSprite);
            window.Draw(storyModeSprite);
            window.Draw(arenaModeSprite);
        }
    }
}
