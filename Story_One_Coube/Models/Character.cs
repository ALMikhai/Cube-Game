using SFML.Graphics;
using SFML.System;
using SFML.Window;
using SFML.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Story_One_Coube.Models
{
    class Character
    {
        public Sprite Sprite { get; set; }

        public int SizeH { get; set; }
        public int SizeW { get; set; }

        public float gravity = 5;
        public float jumpHeight = 15;

        public float stepLong = 5;

        public uint timesToJump = 0;

        public Gun gunNow { get; set; }

        public List<Bullet> bullets = new List<Bullet>();

        public HPBox HP { get; set; }

        public bool OnFloor { get; set; }

        public double enemyTimeBtwShoot = 0.1;

        public double enemyTime = 0;

        public DateTime TimeNow = DateTime.Now;

        public double enemyAllowableDisToMainChar = 200;

        public float enemyStepLong = 2;

        public double TimeToSpawn { get; set; }

        double scaleTexture = 2.6;

        public Character(double hp, Point spawnPoint, Texture texture)
        {
            SizeH = (int)(texture.Size.Y * scaleTexture);
            SizeW = (int)(texture.Size.X * scaleTexture);

            Sprite = new Sprite(texture);

            Sprite.Scale = new Vector2f((float)scaleTexture, (float)scaleTexture);

            Sprite.Origin = new Vector2f((float)(Sprite.Texture.Size.X) / 2, (float)(Sprite.Texture.Size.Y) / 2);

            Sprite.Position = new Vector2f((float)spawnPoint.X, (float)spawnPoint.Y);

            gunNow = new Gun(this.Sprite);

            HP = new HPBox(this, hp);

            OnFloor = false;
        }

        public static Character SpawnCharacter(double hp, Point spawnPoint, Texture texture)
        {
            var newChar = new Character(hp, spawnPoint, texture);
            newChar.TimeToSpawn = 1.5;

            return newChar;
        }
    }
}
