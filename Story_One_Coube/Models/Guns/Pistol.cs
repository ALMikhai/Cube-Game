using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Story_One_Coube.Models.Guns
{
    class Pistol : Gun
    {
        string pathToTexture = "../../Texturs/Guns/pistol.png";

        public Pistol(Sprite sprite)
        {
            sizeH = 35;
            sizeW = 50;

            speedShoot = 10;

            Sprite = new Sprite(new Texture(pathToTexture));
            Sprite.Origin = new Vector2f(0, (float)((float)Sprite.Texture.Size.Y / (float)8));

            scaleRightSide = new Vector2f((float)sizeW / (float)Sprite.Texture.Size.X, (float)sizeH / (float)Sprite.Texture.Size.Y);
            scaleLeftSide = new Vector2f((float)sizeW / (float)Sprite.Texture.Size.X, -((float)sizeH / (float)Sprite.Texture.Size.Y));
        }

        public override void Update(Sprite sprite, Point coord)
        {
            positionForRightSide = new Vector2f(sprite.Position.X + 15, sprite.Position.Y - 10);
            positionForLeftSide = new Vector2f(sprite.Position.X - 15, sprite.Position.Y - 10);

            StartShootPoint = MathStartShootPoint();

            base.Update(sprite, coord);
        }

        public override Point MathStartShootPoint()
        {
            //double OX = sizeW;
            //double OY = sizeH / 2;

            //double gipotenuza = Math.Sqrt((OX * OX) + (OY * OY));

            double graduseToRadian = Sprite.Rotation * (Math.PI / 180);

            double sinMove = Math.Sin(-graduseToRadian);
            double cosMove = Math.Cos(graduseToRadian);

            double dist = 40;

            //double scale = 90 - Sprite.Rotation;
            //scale = scale * (Math.PI / 180);

            //double sinScale= Math.Sin(scale);
            //double cosScale = Math.Cos(scale);

            //Point startPoint;

            //if (Sprite.Rotation > -90 && Sprite.Rotation < 90)
            //{
            //    startPoint = new Point((float)(Sprite.Position.X + (sizeH / 3) * cosScale), (float)(Sprite.Position.Y - (sizeH / 3) * sinScale));
            //}
            //else
            //{
            //    startPoint = new Point((float)(Sprite.Position.X + (sizeH / 3) * cosScale), (float)(Sprite.Position.Y + (sizeH / 3) * sinScale));
            //}

            return new Point((float)(Sprite.Position.X + (dist * cosMove)), (float)(Sprite.Position.Y - (dist * sinMove)));
        }
    }
}
