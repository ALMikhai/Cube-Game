using SFML.Graphics;
using SFML.System;
using SFML.Window;
using SFML.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Story_One_Coube.Scene;
using Story_One_Coube.Models.Guns;

namespace Story_One_Coube.Models
{
    class Bullet
    {
        Sprite Sprite;

        double sinMove;
        double cosMove;

        int damage;

        public double speedBullet;

        static Texture pistolBulletTexture = new Texture("../../Texturs/Guns/SmallBullet.png");
        static Texture smgBulletTexture = new Texture("../../Texturs/Guns/LargeBullet.png");

        public Bullet(Gun gun, float Rotation)
        {

            if (gun is Smg)
            {
                Sprite = new Sprite(smgBulletTexture);
            }
            else
            {
                Sprite = new Sprite(pistolBulletTexture);
            }

            Sprite.Origin = new Vector2f(pistolBulletTexture.Size.X / 2, pistolBulletTexture.Size.Y / 2);
            Sprite.Position = new Vector2f((float)gun.StartShootPoint.X, (float)gun.StartShootPoint.Y);
            Sprite.Rotation = Rotation;

            damage = gun.damage;

            speedBullet = gun.speedShoot;

            sinMove = Math.Sin(Rotation * (Math.PI / 180));
            cosMove = Math.Cos(Rotation * (Math.PI / 180));
        }

        public void Update()
        {
            Sprite.Position = new Vector2f((float)(Sprite.Position.X + speedBullet * cosMove), (float)(Sprite.Position.Y + speedBullet * sinMove));
        }

        public void Draw(RenderWindow renderWindow)
        {
            renderWindow.Draw(Sprite);
        }

        public bool OnWindow()
        {
            if (Sprite.Position.X < 0 || Sprite.Position.X > Program.WidthWindow || Sprite.Position.Y < 0 || Sprite.Position.Y > Program.HeightWindow) return false;

            return true;
        }

        public bool CheckHit(Character owner)
        {
            Character mainChar = Program.levelNow.MainCharacter;

            foreach (var enemy in Program.levelNow.Enemies)
            {
                if (enemy == owner) continue;

                Sprite sprite = enemy.Sprite;
                if (sprite.Position.X - enemy.SizeW / 2 <= Sprite.Position.X && Sprite.Position.X <= sprite.Position.X + enemy.SizeW / 2)
                {
                    if (sprite.Position.Y - enemy.SizeH / 2 <= Sprite.Position.Y && Sprite.Position.Y <= sprite.Position.Y + enemy.SizeH / 2)
                    {
                        CharacterEvents.Hit(enemy, damage);
                        return true;
                    }
                }
            }

            if (mainChar == owner) return false;

            if (mainChar.Sprite.Position.X - mainChar.SizeW / 2 <= Sprite.Position.X && Sprite.Position.X <= mainChar.Sprite.Position.X + mainChar.SizeW / 2)
            {
                if (mainChar.Sprite.Position.Y - mainChar.SizeH / 2 <= Sprite.Position.Y && Sprite.Position.Y <= mainChar.Sprite.Position.Y + mainChar.SizeH / 2)
                {
                    CharacterEvents.Hit(mainChar, damage);
                    return true;
                }
            }

            return false;
        }

        public bool TextureHit()
        {
            foreach(var texture in Program.levelNow.TextureObjects)
            {
                if(texture.Position.X < Sprite.Position.X && Sprite.Position.X < texture.Position.X + texture.Texture.Size.X 
                    && texture.Position.Y < Sprite.Position.Y && Sprite.Position.Y < texture.Position.Y + texture.Texture.Size.Y)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
