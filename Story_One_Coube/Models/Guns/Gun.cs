using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using SFML.Audio;

namespace Story_One_Coube.Models.Guns
{
    class Gun
    {
        public Sprite Sprite { get; protected set; }

        protected double sizeH;
        protected double sizeW;

        public int speedShoot { get; protected set; }

        public Point StartShootPoint;

        protected Vector2f positionForLeftSide;
        protected Vector2f positionForRightSide;

        protected Vector2f scaleRightSide;
        protected Vector2f scaleLeftSide;

        public enum GunSide { Left, Right };
        public GunSide GunSideNow { get; protected set; }

        public virtual void Update(Sprite sprite, Point coord)
        {
            Sprite.Rotation = MathRotation(coord, this);

            if(GunSideNow == GunSide.Left)
            {
                Sprite.Position = positionForLeftSide;
            }
            else
            {
                Sprite.Position = positionForRightSide;
            }

            if ((Sprite.Rotation > -180 && Sprite.Rotation < -110) || (Sprite.Rotation > 110 && Sprite.Rotation < 180))
            {
                GunSideNow = GunSide.Left;
                Sprite.Scale = scaleLeftSide;
                Sprite.Position = positionForLeftSide;
            }
            
            if((Sprite.Rotation > -70 && Sprite.Rotation < 0) || (Sprite.Rotation > 0 && Sprite.Rotation < 70))
            {
                GunSideNow = GunSide.Right;
                Sprite.Scale = scaleRightSide;
                Sprite.Position = positionForRightSide;
            }
        }

        public virtual void Draw(RenderWindow window)
        {
            window.Draw(Sprite);
        }

        public virtual Point MathStartShootPoint()
        {
            return null;
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
