using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Story_One_Coube.Models.Scene
{
    static class Level1
    {
        static Texture floarTexture = new Texture("../../Models/Texturs/floarTexture.png", new IntRect(265, 145, 240, 114));

        static RectangleShape floarSprite = new RectangleShape()
        {
            Size = new Vector2f(240, 114),
            Position = new Vector2f(400, 300),
            Texture = floarTexture,
        };

        public static void InitialLevel(RenderWindow window)
        {
            window.Draw(floarSprite);
        }
    }
}
