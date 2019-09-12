using SFML.Graphics;
using SFML.System;
using Story_One_Coube.Models;
using Story_One_Coube.Models.Guns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Story_One_Coube.Scene
{
    class Level1 : Level
    {
        static Texture floorTexture = new Texture("../../Texturs/LevelObjects/floarTexture.png", new IntRect(265, 145, 240, 114));

        static Sprite floorSprite = new Sprite(floorTexture);
        static Sprite platformTexture = new Sprite(new Texture("../../Texturs/LevelObjects/PlatformStone.png"));
        static Sprite platformTextureRev = new Sprite(new Texture("../../Texturs/LevelObjects/PlatformStoneReverse.png"));

        public Level1()
        {
            MainCharacter = Character.SpawnCharacter(mainCharacterHP, new Point(Program.WidthWindow / 2, Program.HeightWindow / 2), CharacterMovesAnimation.StandMainCharTexture);

            TimeToAirDrop = random.Next(30, 60);
            timeNow = DateTime.Now;

            timeOnPause = 0;

            Inventory.Clear();
            Inventory.AddGun(new Pistol(MainCharacter.Sprite));

            MainCharacter.gunNow = Inventory.Guns[0];

            platformTexture.Color = new Color(129, 77, 88);
            platformTextureRev.Color = new Color(129, 77, 88);
        }

        public override void Draw(RenderWindow window)
        {
            TextureObjects.Clear();

            int drawPoint = -20;

            while (drawPoint < window.Size.X)
            {
                floorSprite.Position = new Vector2f(drawPoint, window.Size.Y - 90);

                TextureObjects.Add(new Sprite(floorSprite));

                window.Draw(floorSprite);

                drawPoint += (int)floorSprite.Texture.Size.X - 20;
            }

            platformTexture.Position = new Vector2f(0, window.Size.Y - 350); // first left.

            TextureObjects.Add(new Sprite(platformTexture));

            window.Draw(platformTexture);

            platformTexture.Position = new Vector2f(0, window.Size.Y - 600); // second left.

            TextureObjects.Add(new Sprite(platformTexture));

            window.Draw(platformTexture);

            platformTexture.Position = new Vector2f(platformTexture.Texture.Size.X + 35, window.Size.Y - 800); // third left.

            TextureObjects.Add(new Sprite(platformTexture));

            window.Draw(platformTexture);

            platformTextureRev.Position = new Vector2f(window.Size.X - platformTexture.Texture.Size.X, window.Size.Y - 350); // first right.

            TextureObjects.Add(new Sprite(platformTextureRev));

            window.Draw(platformTextureRev);

            platformTextureRev.Position = new Vector2f(window.Size.X - platformTexture.Texture.Size.X, window.Size.Y - 600); // second right.

            TextureObjects.Add(new Sprite(platformTextureRev));

            window.Draw(platformTextureRev);

            platformTextureRev.Position = new Vector2f(window.Size.X - platformTexture.Texture.Size.X * 2 - 35, window.Size.Y - 800); // third right.

            TextureObjects.Add(new Sprite(platformTextureRev));

            window.Draw(platformTextureRev);

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
