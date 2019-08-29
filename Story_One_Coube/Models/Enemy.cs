using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Story_One_Coube.Models
{
    class Enemy
    {
        public RectangleShape Sprite { get; set; }

        public int SizeH { get; set; }
        public int SizeW { get; set; }

        public int Thickness { get; set; }

        float gravity = 5;
        float jumpHeight = 15;

        float stepLong = 5;

        public uint timesToJump = 0;

        public Gun gunNow { get; set; }

        public List<Bullet> bullets = new List<Bullet>();

        public HPBox HP { get; set; }

        public Enemy(double hp, int height, int width, Point spawnPoint)
        {
            SizeH = height;
            SizeW = width;

            Thickness = 2;

            Sprite = new RectangleShape(new SFML.System.Vector2f(SizeW, SizeH));

            Sprite.Origin = new SFML.System.Vector2f(SizeW / 2, SizeH / 2);

            Sprite.Position = new SFML.System.Vector2f((float)spawnPoint.X, (float)spawnPoint.Y);

            Sprite.OutlineThickness = Thickness;

            Sprite.FillColor = Color.Red;

            Sprite.OutlineColor = Color.Black;

            gunNow = new Gun(this.Sprite);

            HP = new HPBox(this.Sprite, hp);
        }

        public void Update()
        {
            if (Sprite == null) return;

            HP.Update();

            gunNow.Update(Sprite, new Point(0, 0));

            for (int i = 0; Sprite.Position.Y + Thickness + SizeH / 2 != Program.heightWindow && i != gravity; i++)
            {
                Sprite.Position = new Vector2f(Sprite.Position.X, Sprite.Position.Y + 1);
            }
        }

        public void Draw(RenderWindow window)
        {
            if (Sprite == null) return;

            foreach (var bullet in bullets)
            {
                bullet.Draw(window);
            }

            HP.Draw(window);

            window.Draw(Sprite);
            window.Draw(gunNow.Sprite);
        }
    }
}
