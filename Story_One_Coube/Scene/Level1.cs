using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Story_One_Coube.Scene
{
    class Level1 : Level
    {
        static Texture floorTexture = new Texture("../../Texturs/floarTexture.png", new IntRect(265, 145, 240, 114));

        static RectangleShape floorSprite = new RectangleShape()
        {
            Size = new Vector2f(240, 114),
            Position = new Vector2f(400, 300),
            Texture = floorTexture,
        };

        public override void Draw(RenderWindow window)
        {
            int drawPoint = -20;

            while (drawPoint < window.Size.X)
            {
                floorSprite.Position = new Vector2f(drawPoint, window.Size.Y - 90);

                Program.TextureObjects.Add(new RectangleShape(floorSprite));

                window.Draw(floorSprite);

                drawPoint += (int)floorSprite.Size.X - 20;
            }

            floorSprite.Position = new Vector2f(640, 360);

            Program.TextureObjects.Add(new RectangleShape(floorSprite));

            window.Draw(floorSprite);

            base.Draw(window);
        }
    }
}
