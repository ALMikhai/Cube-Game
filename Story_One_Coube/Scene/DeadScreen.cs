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
    class DeadScreen
    {

        Texture exitTexture = new Texture("../../Texturs/DeadScreenText.png", new IntRect(60, 13, 80, 27));
        Sprite exitSprite;
        IntRect exitRect;

        public DeadScreen(RenderWindow window)
        {
            exitSprite = new Sprite(exitTexture);
            exitSprite.Position = new Vector2f(window.Size.X / 2, window.Size.Y / 2);

            exitRect = new IntRect((int)window.Size.X / 2, (int)window.Size.Y / 2, (int)exitTexture.Size.X, (int)exitTexture.Size.Y);
            //exitSprite.TextureRect = exitRect;
        }

        public void DrawAndUpdate(RenderWindow window)
        {
            exitSprite.Color = Color.White;
            Program.deadScreenChooseNow = Program.deadScreenChoose.None;

            if (exitRect.Contains((int)Program.LastMousePosition.X, (int)Program.LastMousePosition.Y))
            {
                Program.deadScreenChooseNow = Program.deadScreenChoose.Exit;
                exitSprite.Color = Color.Red;
            }

            window.Draw(exitSprite);
        }
    }
}
