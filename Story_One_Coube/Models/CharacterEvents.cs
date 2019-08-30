using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Story_One_Coube.Models
{
     static class CharacterEvents
    {
        public enum Moves { STOP = 0, UP, DOWN, LEFT, RIGHT }

        public static void Shoot(Character character, Point coord)
        {
            character.bullets.Add(new Bullet(new Point(character.Sprite.Position.X, character.Sprite.Position.Y), character.gunNow.speedShoot, coord));
        }

        public static void Hit(Character character, double damage)
        {
            character.HP.Hit(damage);
        }

        public static void Jump(Character character)
        {
            if (character.timesToJump != 0 || character.Sprite.Position.Y + character.Thickness + character.SizeH / 2 != Program.heightWindow) return;

            character.timesToJump += 30;
        }

        public static void Draw(RenderWindow window, Character character)
        {
            if (character.Sprite == null) return;

            foreach (var bullet in character.bullets)
            {
                bullet.Draw(window);
            }

            character.HP.Draw(window);

            window.Draw(character.Sprite);
            window.Draw(character.gunNow.Sprite);
        }

        public static void UpdateMainChar(Moves move, Character character)
        {
            if (character.Sprite == null) return;

            character.gunNow.Update(character.Sprite, Program.lastMousePosition);

            foreach (var bullet in character.bullets.ToArray())
            {
                if (!bullet.OnWindow() || bullet.CheckHit())
                {
                    character.bullets.Remove(bullet);
                    return;
                }

                bullet.Update();
            }

            character.HP.Update();

            switch (move)
            {
                case Moves.LEFT:
                    character.Sprite.Position = new Vector2f(character.Sprite.Position.X - character.stepLong, character.Sprite.Position.Y);
                    break;

                case Moves.RIGHT:
                    character.Sprite.Position = new Vector2f(character.Sprite.Position.X + character.stepLong, character.Sprite.Position.Y);
                    break;
            }

            if (character.timesToJump != 0)
            {
                character.timesToJump--;
                character.Sprite.Position = new Vector2f(character.Sprite.Position.X, character.Sprite.Position.Y - character.jumpHeight);
            }

            for (int i = 0; character.Sprite.Position.Y + character.Thickness + character.SizeH / 2 != Program.heightWindow && i != character.gravity; i++)
            {
                character.Sprite.Position = new Vector2f(character.Sprite.Position.X, character.Sprite.Position.Y + 1);
            }
        }

        public static void UpdateEnemy(Character character)
        {
            if (character.Sprite == null) return;

            if (character.HP.ValueNow <= 0)
            {
                Death(character);
                return;
            }

            character.HP.Update();

            character.gunNow.Update(character.Sprite, new Point(0, 0));

            for (int i = 0; character.Sprite.Position.Y + character.Thickness + character.SizeH / 2 != Program.heightWindow && i != character.gravity; i++)
            {
                character.Sprite.Position = new Vector2f(character.Sprite.Position.X, character.Sprite.Position.Y + 1);
            }
        }

        public static void Death(Character character)
        {
            Program.Enemies.Remove(character);
        }
    }
}
