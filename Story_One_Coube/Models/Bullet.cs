using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Story_One_Coube.Models
{
    class Bullet
    {
        CircleShape Sprite;

        int radius = 5;

        double sinMove;
        double cosMove;

        public double speedBullet;

        public Bullet(Point coord, double speed, Point coordToMove)
        {
            Sprite = new CircleShape(radius);
            Sprite.FillColor = Color.Black;
            Sprite.Origin = new SFML.System.Vector2f(radius / 2, radius / 2);
            Sprite.Position = new SFML.System.Vector2f((float)coord.X, (float)coord.Y);

            speedBullet = speed;

            double OX = coordToMove.X - coord.X;
            double OY = coordToMove.Y - coord.Y;

            double gipotenuza = Math.Sqrt((OX * OX) + (OY * OY));

            sinMove = OY / gipotenuza;
            cosMove = OX / gipotenuza;
        }

        public void Update()
        {
            Sprite.Position = new SFML.System.Vector2f((float)(Sprite.Position.X + speedBullet * cosMove), (float)(Sprite.Position.Y + speedBullet * sinMove));
        }

        public void Draw(RenderWindow renderWindow)
        {
            renderWindow.Draw(Sprite);
        }
    }
}
