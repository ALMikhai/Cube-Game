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

        public int clips { get; protected set; }
        public int clipSize { get; protected set; }
        public int clipNow { get; protected set; }
        public int reloadingTime { get; protected set; }
        public DateTime reloadingTimeNow { get; protected set; }
        public bool isReloated { get; protected set; }
        public int reloadPercentForInterface { get; protected set; }

        public Sprite bulletForInterface { get; protected set; }

        public virtual void Update(Sprite sprite, Point coord)
        {
            Sprite.Rotation = MathRotation(coord, this);

            if (GunSideNow == GunSide.Left)
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

            if ((Sprite.Rotation > -70 && Sprite.Rotation < 0) || (Sprite.Rotation > 0 && Sprite.Rotation < 70))
            {
                GunSideNow = GunSide.Right;
                Sprite.Scale = scaleRightSide;
                Sprite.Position = positionForRightSide;
            }

            if (clipNow == 0 || !isReloated)
            {
                Reload();
            }
        }

        public virtual void MainCharShoot(Character character, Point coord)
        {
            if (!isReloated || clipNow == 0) return;

            clipNow--;
            character.bullets.Add(new Bullet(character.gunNow.StartShootPoint, character.gunNow.speedShoot, coord, character.gunNow.Sprite.Rotation));
        }

        public virtual void Reload()
        {
            if (clips == 0 || clipNow == clipSize)
            {
                return;
            }

            if (isReloated == true)
            {
                RestartReloadTime();
            }

            isReloated = false;

            if ((DateTime.Now - reloadingTimeNow).TotalSeconds > reloadingTime)
            {
                isReloated = true;
                clips -= clipSize - clipNow;
                clipNow = clipSize;

                if(clips < 0)
                {
                    clipNow += clips;
                    clips = 0;
                }
            }
            else
            {
                reloadPercentForInterface = (int)(((DateTime.Now - reloadingTimeNow).TotalSeconds / reloadingTime) * 100);
            }
        }

        public virtual void RestartReloadTime()
        {
            reloadingTimeNow = DateTime.Now;
        }

        public virtual void EnemyShoot(Character character, Point coord)
        {
            character.bullets.Add(new Bullet(character.gunNow.StartShootPoint, character.gunNow.speedShoot, coord, character.gunNow.Sprite.Rotation));
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
