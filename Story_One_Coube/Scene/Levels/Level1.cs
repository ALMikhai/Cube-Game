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

        static int numWaves;
        static int numWaveNow;
        static DateTime timeNowWave;
        static double timeToNewWave;
        static double timeBetweenWaves;

        public Level1()
        {
            MainCharacter = Character.SpawnCharacter(mainCharacterHP, new Point(Program.WidthWindow / 2, Program.HeightWindow / 2), CharacterMovesAnimation.StandMainCharTexture);

            TimeToAirDrop = 20;
            timeNowAirDrop = DateTime.Now;

            timeOnPause = 0;

            Inventory.Clear();
            Inventory.AddGun(new Pistol(MainCharacter.Sprite));

            MainCharacter.gunNow = Inventory.Guns[0];

            levelMusic = Musics.Level1;
            numLevel = 1;

            platformTexture.Color = new Color(129, 77, 88);
            platformTextureRev.Color = new Color(129, 77, 88);

            numWaves = 6;
            numWaveNow = 1;
            timeNowWave = DateTime.Now;
            timeToNewWave = 15;
            timeBetweenWaves = 15;
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
            if(numWaveNow == numWaves && Enemies.Count == 0)
            {
                WinScreen.Display();
            }

            if (numWaveNow != numWaves)
            {
                if ((DateTime.Now - timeNowWave).TotalSeconds > timeToNewWave || Enemies.Count == 0)
                {
                    for (var i = 0; i < numWaveNow; i++)
                    {
                        Enemies.Add(Character.SpawnCharacter(75, new Point(random.Next((int)Program.WidthWindow), -100), CharacterMovesAnimation.StandEnemyTexture, new ShotGun(new Sprite())));
                    }

                    timeNowWave = DateTime.Now;
                    numWaveNow++;
                    timeToNewWave = timeBetweenWaves;
                }
            }

            base.Update(window);
        }

        public override Level RestartLevel()
        {
            base.RestartLevel();

            return new Level1();
        }
    }
}
