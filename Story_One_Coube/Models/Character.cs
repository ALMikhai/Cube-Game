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
        public RectangleShape Sprite { get; set; }

        public int SizeH { get; set; }
        public int SizeW { get; set; }

        public int Thickness { get; set; }

        public float gravity = 5;
        public float jumpHeight = 15;

        public float stepLong = 5;

        public uint timesToJump = 0;

        public Gun gunNow { get; set; }

        public List<Bullet> bullets = new List<Bullet>();

        public HPBox HP { get; set; }

        public bool OnFloor { get; set; }

        public double enemyTimeBtwShoot = 10;

        public double enemyTime = 0;

        public DateTime TimeNow = DateTime.Now;

        public double enemyAllowableDisToMainChar = 200;

        public float enemyStepLong = 2;

        public Color FillColor { get; set; }

        public double TimeToSpawn { get; set; }

        public Character(double hp, int height, int width, Point spawnPoint)
        {
            SizeH = height;
            SizeW = width;

            Thickness = 2;

            FillColor = new Color(255, 255, 255);

            Sprite = new RectangleShape(new Vector2f(SizeW, SizeH));

            Sprite.Origin = new Vector2f(SizeW / 2, SizeH / 2);

            Sprite.Position = new Vector2f((float)spawnPoint.X, (float)spawnPoint.Y);

            Sprite.OutlineThickness = Thickness;

            Sprite.FillColor = FillColor;

            Sprite.OutlineColor = Color.Red;

            gunNow = new Gun(this.Sprite);

            HP = new HPBox(this.Sprite, hp);

            OnFloor = false;
        }

        public static Character SpawnCharacter(double hp, int height, int width, Point spawnPoint)
        {
            var newChar = new Character(hp, height, width, spawnPoint);
            newChar.TimeToSpawn = 1.5;

            return newChar;
        }
    }
}
