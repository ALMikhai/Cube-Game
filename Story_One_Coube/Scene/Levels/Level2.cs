﻿using SFML.Graphics;
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
    class Level2 : Level
    {
        static Sprite platformTexture = new Sprite(new Texture("../../Texturs/LevelObjects/PlatformSnow.png"));
        static Sprite platformTextureRev = new Sprite(new Texture("../../Texturs/LevelObjects/PlatformSnowRev.png"));

        static Sprite platform2Texture = new Sprite(new Texture("../../Texturs/LevelObjects/PlatformSnow2.png"));
        static Sprite platform2TextureRev = new Sprite(new Texture("../../Texturs/LevelObjects/PlatformSnow2Rev.png"));

        static int numWaves;
        static int numWaveNow;
        static DateTime timeNowWave;
        static double timeToNewWave;

        public Level2()
        {
            MainCharacter = Character.SpawnCharacter(mainCharacterHP, new Point(Program.WidthWindow / 2 - 200, Program.HeightWindow / 2), CharacterMovesAnimation.StandMainCharTexture);

            TimeToAirDrop = 20;
            timeNowAirDrop = DateTime.Now;

            timeOnPause = 0;

            Inventory.Clear();
            Inventory.AddGun(new Pistol());
            Inventory.AddGun(new Smg());

            MainCharacter.gunNow = Inventory.Guns[0];

            levelMusic = Musics.Level2;
            numLevel = 2;

            Color mainColor = new Color(146, 191, 229);

            platformTexture.Color = mainColor;
            platformTextureRev.Color = mainColor;
            platform2Texture.Color = mainColor;
            platform2TextureRev.Color = mainColor;

            numWaves = 6;
            numWaveNow = 2;
            timeNowWave = DateTime.Now;
            timeToNewWave = 20;
        }

        public override void Draw(RenderWindow window)
        {
            TextureObjects.Clear();

            platformTexture.Position = new Vector2f(0, window.Size.Y - 150); // first left.

            TextureObjects.Add(new Sprite(platformTexture));

            window.Draw(platformTexture);

            platform2Texture.Position = new Vector2f(500, window.Size.Y - 350); // second left.

            TextureObjects.Add(new Sprite(platform2Texture));

            window.Draw(platform2Texture);

            platformTexture.Position = new Vector2f(0, window.Size.Y - 610); // third left.

            TextureObjects.Add(new Sprite(platformTexture));

            window.Draw(platformTexture);

            platformTextureRev.Position = new Vector2f(window.Size.X - platformTexture.Texture.Size.X, window.Size.Y - 150); // first right.

            TextureObjects.Add(new Sprite(platformTextureRev));

            window.Draw(platformTextureRev);

            platform2TextureRev.Position = new Vector2f(window.Size.X - platformTexture.Texture.Size.X - 500, window.Size.Y - 350); // second right.

            TextureObjects.Add(new Sprite(platform2TextureRev));

            window.Draw(platform2TextureRev);

            platformTextureRev.Position = new Vector2f(window.Size.X - platformTexture.Texture.Size.X, window.Size.Y - 610); // third right.

            TextureObjects.Add(new Sprite(platformTextureRev));

            window.Draw(platformTextureRev);

            base.Draw(window);
        }

        public override void Update(RenderWindow window)
        {
            if (numWaveNow == numWaves && Enemies.Count == 0)
            {
                WinScreen.Display();
                return;
            }

            if (numWaveNow != numWaves)
            {
                if ((DateTime.Now - timeNowWave).TotalSeconds > timeToNewWave || Enemies.Count == 0)
                {
                    for (var i = 0; i < numWaveNow; i++)
                    {
                        Enemies.Add(Character.SpawnCharacter(75, new Point(random.Next((int)Program.WidthWindow), -100), CharacterMovesAnimation.StandEnemyTexture, new Smg()));
                    }

                    timeNowWave = DateTime.Now;
                    numWaveNow++;
                }
            }

            base.Update(window);
        }

        public override Level RestartLevel()
        {
            base.RestartLevel();

            return new Level2();
        }
    }
}