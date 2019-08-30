using SFML.Graphics;
using SFML.System;
using SFML.Window;
using SFML.Audio;
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

        int damage = 33;

        public double speedBullet;

        public Bullet(Point coord, double speed, Point coordToMove)
        {
            Sprite = new CircleShape(radius);
            Sprite.FillColor = Color.Black;
            Sprite.Origin = new Vector2f(radius / 2, radius / 2);
            Sprite.Position = new Vector2f((float)coord.X, (float)coord.Y);

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
            Character mainChar = Program.MainCharacter;

            foreach (var enemy in Program.Enemies)
            {
                if (enemy == owner) continue;

                RectangleShape sprite = enemy.Sprite;
                if (sprite.Position.X - sprite.Size.X / 2 <= Sprite.Position.X && Sprite.Position.X <= sprite.Position.X + sprite.Size.X / 2)
                {
                    if (sprite.Position.Y - sprite.Size.Y / 2 <= Sprite.Position.Y && Sprite.Position.Y <= sprite.Position.Y + sprite.Size.Y / 2)
                    {
                        CharacterEvents.Hit(enemy, damage);
                        return true;
                    }
                }
            }

            if (mainChar == owner) return false;

            if (mainChar.Sprite.Position.X - mainChar.Sprite.Size.X / 2 <= Sprite.Position.X && Sprite.Position.X <= mainChar.Sprite.Position.X + mainChar.Sprite.Size.X / 2)
            {
                if (mainChar.Sprite.Position.Y - mainChar.Sprite.Size.Y / 2 <= Sprite.Position.Y && Sprite.Position.Y <= mainChar.Sprite.Position.Y + mainChar.Sprite.Size.Y / 2)
                {
                    CharacterEvents.Hit(mainChar, damage);
                    return true;
                }
            }

            return false;
        }
    }
}
