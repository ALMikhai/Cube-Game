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
    class Level1 : Level
    {
        static Texture floorTexture = new Texture("../../Texturs/floarTexture.png", new IntRect(265, 145, 240, 114));

        static RectangleShape floorSprite = new RectangleShape()
        {
            Size = new Vector2f(240, 114),
            Position = new Vector2f(400, 300),
            Texture = floorTexture,
        };

        public Level1()
        {
            MainCharacter = Character.SpawnCharacter(mainCharacterHP, new Point(Program.WidthWindow / 2, Program.HeightWindow / 2), CharacterMovesAnimation.StandMainCharTexture);

            TimeToAirDrop = 20;
            timeNow = DateTime.Now;

            timeOnPause = 0;
        }

        public override void Draw(RenderWindow window)
        {
            TextureObjects.Clear();

            int drawPoint = -20;

            while (drawPoint < window.Size.X)
            {
                floorSprite.Position = new Vector2f(drawPoint, window.Size.Y - 90);

                TextureObjects.Add(new RectangleShape(floorSprite));

                window.Draw(floorSprite);

                drawPoint += (int)floorSprite.Size.X - 20;
            }

            floorSprite.Position = new Vector2f(0, window.Size.Y - 350);

            TextureObjects.Add(new RectangleShape(floorSprite));

            window.Draw(floorSprite);

            base.Draw(window);
        }

        public override void Update(RenderWindow window)
        {
            base.Update(window);
        }

        public override Level RestartLevel()
        {
            base.RestartLevel();

            return new Level1();
        }
    }
}
