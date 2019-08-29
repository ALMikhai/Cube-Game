using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using SFML.Audio;

namespace Story_One_Coube.Models
{
    class Gun
    {
        public RectangleShape Sprite;

        int sizeH = 10;
        int sizeW = 20;

        public int speedShoot = 10;

        public Gun(RectangleShape sprite)
        {
            Sprite = new RectangleShape(new SFML.System.Vector2f(sizeW, sizeH));
            Sprite.Origin = new SFML.System.Vector2f(0, sizeH / 2);
            Sprite.FillColor = Color.Black;

            Sprite.Position = sprite.Position;
        }

        public void Update(RectangleShape sprite, Point coord)
        {
            if (Sprite == null) return;

            float rotation = MathRotation(coord, this);

            Sprite.Position = sprite.Position;
            Sprite.Rotation = rotation;
        }

        public static float MathRotation(Point coord, Gun gunNow)
        {
            double OX = coord.X - gunNow.Sprite.Position.X;

            double OY = coord.Y - gunNow.Sprite.Position.Y;

            float gunRotation = (float)(Math.Atan2(OY, OX) / Math.PI * 180);

            return gunRotation;
        }
    }
}
