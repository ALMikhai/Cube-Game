using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Story_One_Coube.Models
{
     static class CharacterEvents
    {
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
    }
}
