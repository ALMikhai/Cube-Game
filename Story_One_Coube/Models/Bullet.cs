﻿using SFML.Graphics;
using SFML.System;
using SFML.Window;
using SFML.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Story_One_Coube.Scene;

namespace Story_One_Coube.Models
{
    class Bullet
    {
        Sprite Sprite;

        double sinMove;
        double cosMove;

        int damage = 33;

        public double speedBullet;

        static Texture bullettTexture = new Texture("../../Texturs/Guns/image5.png");

        public Bullet(Point coord, double speed, Point coordToMove, float Rotation)
        {
            Sprite = new Sprite(bullettTexture);
            Sprite.Origin = new Vector2f(bullettTexture.Size.X / 2, bullettTexture.Size.Y / 2);
            Sprite.Position = new Vector2f((float)coord.X, (float)coord.Y);
            Sprite.Rotation = Rotation;

            speedBullet = speed;

            double OX = coordToMove.X - coord.X;
            double OY = coordToMove.Y - coord.Y;

            double gipotenuza = Math.Sqrt((OX * OX) + (OY * OY));

            sinMove = OY / gipotenuza;
            cosMove = OX / gipotenuza;
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
                if(texture.Position.X < Sprite.Position.X && Sprite.Position.X < texture.Position.X + texture.Size.X 
                    && texture.Position.Y < Sprite.Position.Y && Sprite.Position.Y < texture.Position.Y + texture.Size.Y)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
