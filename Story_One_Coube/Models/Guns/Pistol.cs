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

            speedShoot = 12;

            damage = 20;

            Sprite = new Sprite(new Texture(pathToTexture));
            Sprite.Origin = new Vector2f(0, (float)((float)Sprite.Texture.Size.Y / (float)8));

            scaleRightSide = new Vector2f((float)sizeW / (float)Sprite.Texture.Size.X, (float)sizeH / (float)Sprite.Texture.Size.Y);
            scaleLeftSide = new Vector2f((float)sizeW / (float)Sprite.Texture.Size.X, -((float)sizeH / (float)Sprite.Texture.Size.Y));

            GunSideNow = GunSide.Left;
            Sprite.Scale = scaleLeftSide;

            StartShootPoint = MathStartShootPoint();

            clips = 100;
            reloadingTime = 2;
            clipSize = 10;
            clipNow = 10;

            isReloated = true;

            bulletForInterface = new Sprite(new Texture("../../Texturs/Guns/small_bullet.png"));
            bulletForInterface.Position = new Vector2f(20, Program.HeightWindow - 90);
        }

        public override void Update(Sprite sprite, Point coord)
        {
            positionForRightSide = new Vector2f(sprite.Position.X + 15, sprite.Position.Y - 10);
            positionForLeftSide = new Vector2f(sprite.Position.X - 15, sprite.Position.Y - 10);

            base.Update(sprite, coord);

            StartShootPoint = MathStartShootPoint();
        }

        public override Point MathStartShootPoint()
        {
            double graduseToRadian = Sprite.Rotation * (Math.PI / 180);

            double sinMove = Math.Sin(-graduseToRadian);
            double cosMove = Math.Cos(graduseToRadian);

            double dist = 40;

            return new Point((float)(Sprite.Position.X + (dist * cosMove)), (float)(Sprite.Position.Y - (dist * sinMove)));
        }

        public override void MainCharShoot(Character character, Point coord)
        {
            if (!isReloated || clipNow == 0) return;

            Sounds.PistolShoot.Play();

            base.MainCharShoot(character, coord);
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
