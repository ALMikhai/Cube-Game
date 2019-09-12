using SFML.Graphics;
using SFML.System;
using Story_One_Coube.Models;
using Story_One_Coube.Models.Guns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Story_One_Coube.Scene.Levels
{
    class Arena : Level
    {
        static Texture floorTexture = new Texture("../../Texturs/LevelObjects/floarTexture.png", new IntRect(265, 145, 240, 114));

        static Sprite floorSprite = new Sprite(floorTexture);
        static Sprite platformTexture = new Sprite(new Texture("../../Texturs/LevelObjects/PlatformStone.png"));
        static Sprite platformTextureRev = new Sprite(new Texture("../../Texturs/LevelObjects/PlatformStoneReverse.png"));

        static int numMicroWave;
        static int numWaveNow;
        static DateTime timeNowWave;
        static double timeToNewWave;

        static Gun enemyGun;
        static Dictionary<int, Gun> guns;

        public Arena()
        {
            MainCharacter = Character.SpawnCharacter(mainCharacterHP, new Point(Program.WidthWindow / 2, Program.HeightWindow / 2), CharacterMovesAnimation.StandMainCharTexture);

            TimeToAirDrop = 15;
            timeNowAirDrop = DateTime.Now;

            timeOnPause = 0;

            Inventory.Clear();
            Inventory.AddGun(new Pistol());
            Inventory.AddGun(new Smg());
            Inventory.AddGun(new ShotGun());

            MainCharacter.gunNow = Inventory.Guns[0];

            levelMusic = Musics.Level3;
            numLevel = 3;

            Color mainColor = new Color(69, 49, 61);

            platformTexture.Color = mainColor;
            platformTextureRev.Color = mainColor;

            numMicroWave = 0;
            numWaveNow = 1;
            timeNowWave = DateTime.Now;
            timeToNewWave = 30;

            guns = new Dictionary<int, Gun>()
            {
                { 0, new Pistol() },
                { 1, new Smg() },
                { 2, new ShotGun() }
            };

            guns.TryGetValue(0, out enemyGun);
        }

        public override void Draw(RenderWindow window)
        {
            TextureObjects.Clear();

            int drawPoint = -20;

            while (drawPoint < 1000)
            {
                floorSprite.Position = new Vector2f(drawPoint, window.Size.Y - 90);

                TextureObjects.Add(new Sprite(floorSprite));

                window.Draw(floorSprite);

                drawPoint += (int)floorSprite.Texture.Size.X - 20;
            }

            floorSprite.Position = new Vector2f(1300, window.Size.Y - 90); // floor block.
            TextureObjects.Add(new Sprite(floorSprite));
            window.Draw(floorSprite);

            platformTexture.Position = new Vector2f(0, window.Size.Y - 350); // first left.
            TextureObjects.Add(new Sprite(platformTexture));
            window.Draw(platformTexture);

            platformTexture.Position = new Vector2f(platformTexture.Texture.Size.X, window.Size.Y - 600); // second left.
            TextureObjects.Add(new Sprite(platformTexture));
            window.Draw(platformTexture);

            platformTexture.Position = new Vector2f(window.Size.X / 2 - platformTexture.Texture.Size.X / 2, window.Size.Y - 800); // third left.
            TextureObjects.Add(new Sprite(platformTexture));
            window.Draw(platformTexture);

            platformTextureRev.Position = new Vector2f(window.Size.X - platformTexture.Texture.Size.X * 2 - 35, window.Size.Y - 600); // first right.
            TextureObjects.Add(new Sprite(platformTextureRev));
            window.Draw(platformTextureRev);

            platformTextureRev.Position = new Vector2f(window.Size.X - platformTexture.Texture.Size.X, window.Size.Y - 800); // second right.
            TextureObjects.Add(new Sprite(platformTextureRev));
            window.Draw(platformTextureRev);

            base.Draw(window);
        }

        public override void Update(RenderWindow window)
        {
            if ((DateTime.Now - timeNowWave).TotalSeconds > timeToNewWave || Enemies.Count == 0)
            {


                for (var i = 0; i < numWaveNow; i++)
                {
                    enemyGun = Gun.Copy(enemyGun);
                    Enemies.Add(Character.SpawnCharacter(75, new Point(random.Next((int)Program.WidthWindow), -100), CharacterMovesAnimation.StandEnemyTexture, enemyGun));
                }

                timeNowWave = DateTime.Now;
                numMicroWave++;

                if (numMicroWave == 3)
                {
                    numMicroWave = 0;
                    numWaveNow++;
                }

                guns.TryGetValue(numMicroWave, out enemyGun);
            }

            base.Update(window);
        }

        public override Level RestartLevel()
        {
            base.RestartLevel();

            return new Arena();
        }
    }
}
