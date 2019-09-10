using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Story_One_Coube.Models.Guns
{
    class ShotGun : Gun
    {
        string pathToTexture = "../../Texturs/Guns/shotgun2.png";

        public ShotGun(Sprite sprite)
        {
            sizeH = 45;
            sizeW = 100;

            speedShoot = 9;

            damage = 34;

            Sprite = new Sprite(new Texture(pathToTexture));
            Sprite.Origin = new Vector2f((float)((float)Sprite.Texture.Size.X / (float)3.3), (float)((float)Sprite.Texture.Size.Y / (float)4.3));

            scaleRightSide = new Vector2f((float)sizeW / (float)Sprite.Texture.Size.X, (float)sizeH / (float)Sprite.Texture.Size.Y);
            scaleLeftSide = new Vector2f((float)sizeW / (float)Sprite.Texture.Size.X, -((float)sizeH / (float)Sprite.Texture.Size.Y));

            GunSideNow = GunSide.Left;
            Sprite.Scale = scaleLeftSide;

            StartShootPoint = MathStartShootPoint();

            clips = 30;
            reloadingTime = 7;
            clipSize = 5;
            clipNow = 5;

            isReloated = true;

            bulletForInterface = new Sprite(new Texture("../../Texturs/Guns/medium_bullet.png"));
            bulletForInterface.Position = new Vector2f(15, Program.HeightWindow - 127);
        }

        public override void Update(Sprite sprite, Point coord)
        {
            positionForRightSide = new Vector2f(sprite.Position.X + 10, sprite.Position.Y);
            positionForLeftSide = new Vector2f(sprite.Position.X - 10, sprite.Position.Y);

            base.Update(sprite, coord);

            StartShootPoint = MathStartShootPoint();
        }

        public override Point MathStartShootPoint()
        {
            double graduseToRadian = Sprite.Rotation * (Math.PI / 180);

            double sinMove = Math.Sin(-graduseToRadian);
            double cosMove = Math.Cos(graduseToRadian);

            double dist = 64;

            return new Point((float)(Sprite.Position.X + (dist * cosMove)), (float)(Sprite.Position.Y - (dist * sinMove)));
        }

        public override void MainCharShoot(Character character, Point coord)
        {
            if (!isReloated || clipNow == 0) return;

            Sounds.PistolShoot.Play();

            clipNow--;

            character.bullets.Add(new Bullet(this, character.gunNow.Sprite.Rotation + 2));
            character.bullets.Add(new Bullet(this, character.gunNow.Sprite.Rotation - 2));
            character.bullets.Add(new Bullet(this, character.gunNow.Sprite.Rotation));
        }

        public override void Reload()
        {
            if (clips == 0 || clipNow == clipSize)
            {
                return;
            }

            if (isReloated == true)
            {
                Sounds.PistolReload.Play();
            }

            base.Reload();
        }
    }
}
