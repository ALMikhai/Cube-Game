using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Story_One_Coube.Models.Guns
{
    class Smg : Gun 
    {
        string pathToTexture = "../../Texturs/Guns/smg2.png";

        public Smg(Sprite sprite)
        {
            sizeH = 50;
            sizeW = 105;

            speedShoot = 15;



            Sprite = new Sprite(new Texture(pathToTexture));
            Sprite.Origin = new Vector2f((float)((float)Sprite.Texture.Size.X / (float)3.3), (float)((float)Sprite.Texture.Size.Y / (float)2.9));

            scaleRightSide = new Vector2f((float)sizeW / (float)Sprite.Texture.Size.X, (float)sizeH / (float)Sprite.Texture.Size.Y);
            scaleLeftSide = new Vector2f((float)sizeW / (float)Sprite.Texture.Size.X, -((float)sizeH / (float)Sprite.Texture.Size.Y));

            StartShootPoint = MathStartShootPoint();

            clips = 210;
            reloadingTime = 5;
            clipSize = 30;
            clipNow = 30;

            isReloated = true;

            bulletForInterface = new Sprite(new Texture("../../Texturs/Guns/large_bullet.png"));
            bulletForInterface.Position = new Vector2f(20, Program.HeightWindow - 157);
        }

        public override void Update(Sprite sprite, Point coord)
        {
            positionForRightSide = new Vector2f(sprite.Position.X + 10, sprite.Position.Y - 5);
            positionForLeftSide = new Vector2f(sprite.Position.X - 10, sprite.Position.Y - 5);

            base.Update(sprite, coord);

            StartShootPoint = MathStartShootPoint();
        }

        public override Point MathStartShootPoint()
        {
            double graduseToRadian = Sprite.Rotation * (Math.PI / 180);

            double sinMove = Math.Sin(-graduseToRadian);
            double cosMove = Math.Cos(graduseToRadian);

            double dist = 60;

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
