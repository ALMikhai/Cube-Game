﻿using SFML.Graphics;
using SFML.System;
using Story_One_Coube.Scene;
using System;

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
            if (character.TimeToSpawn > 0) return;

            character.HP.Hit(damage);
        }

        public static void Jump(Character character)
        {
            if (character.timesToJump != 0 || !character.OnFloor) return;

            character.timesToJump += 30;

            character.OnFloor = false;
        }

        public static void Draw(RenderWindow window, Character character)
        {
            if (character.TimeToSpawn > 0)
            {
                spawnChar(window, character);
                return;
            }

            foreach (var bullet in character.bullets)
            {
                bullet.Draw(window);
            }

            character.HP.Draw(window);

            window.Draw(character.Sprite);
            window.Draw(character.gunNow.Sprite);
        }

        public static void UpdateChar(Character character)
        {
            if (character.TimeToSpawn > 0) return;

            foreach (var bullet in character.bullets.ToArray())
            {
                if (!bullet.OnWindow() || bullet.CheckHit(character) || bullet.TextureHit())
                {
                    character.bullets.Remove(bullet);
                    return;
                }

                bullet.Update();
            }

            character.HP.Update();

            if (character.timesToJump != 0)
            {
                character.timesToJump--;
                character.Sprite.Position = new Vector2f(character.Sprite.Position.X, character.Sprite.Position.Y - character.jumpHeight);
            }

            for (int i = 0; i != character.gravity; i++)
            {
                character.OnFloor = false;

                foreach (var platform in Program.levelNow.TextureObjects)
                {
                    if (character.Sprite.Position.Y + character.Thickness + character.SizeH / 2 == platform.Position.Y
                        && platform.Position.X < character.Sprite.Position.X && character.Sprite.Position.X < platform.Position.X + platform.Size.X)
                    {
                        character.OnFloor = true;
                        break;
                    }
                }

                if (character.OnFloor) break;

                character.Sprite.Position = new Vector2f(character.Sprite.Position.X, character.Sprite.Position.Y + 1);
            }
        }

        public static void UpdateMainChar(Moves move, Character character)
        {
            if (character.TimeToSpawn > 0) return;

            if(character.HP.ValueNow <= 0)
            {
                deathMainChar(character);
            }

            character.gunNow.Update(character.Sprite, Program.LastMousePosition);

            switch (move)
            {
                case Moves.LEFT:
                    if ((character.Sprite.Position.X - (character.Sprite.Size.X / 2)) <= 0) break;

                    character.Sprite.Position = new Vector2f(character.Sprite.Position.X - character.stepLong, character.Sprite.Position.Y);
                    break;

                case Moves.RIGHT:
                    if ((character.Sprite.Position.X + (character.Sprite.Size.X / 2)) >= Program.WidthWindow) break;

                    character.Sprite.Position = new Vector2f(character.Sprite.Position.X + character.stepLong, character.Sprite.Position.Y);
                    break;
            }
        }

        public static void UpdateEnemy(Character character)
        {
            if (character.TimeToSpawn > 0) return;

            character.gunNow.Update(character.Sprite, new Point(Program.levelNow.MainCharacter.Sprite.Position.X, Program.levelNow.MainCharacter.Sprite.Position.Y));

            if (character.HP.ValueNow <= 0)
            {
                Program.levelNow.Score += 10;
                deathEnemy(character);
                return;
            }

            if (CanShoot(character))
            {
                Shoot(character, new Point(Program.levelNow.MainCharacter.Sprite.Position.X, Program.levelNow.MainCharacter.Sprite.Position.Y));
            }

            ChaseMainChar(character);
        }

        public static bool CanShoot(Character character)
        {
            if(character.enemyTime <= 0)
            {
                character.enemyTime += character.enemyTimeBtwShoot;
                return true;
            }
            else
            {
                character.enemyTime -= (DateTime.Now - character.TimeNow).TotalSeconds;
                character.TimeNow = DateTime.Now;
                return false;
            }
        }

        public static void ChaseMainChar(Character enemy)
        {
            RectangleShape spriteMainChar = Program.levelNow.MainCharacter.Sprite;
            RectangleShape spriteEnemy = enemy.Sprite;

            if(mathDistanceToMainChar(enemy) > enemy.enemyAllowableDisToMainChar)
            {
                if(spriteEnemy.Position.X > spriteMainChar.Position.X)
                {
                    spriteEnemy.Position = new Vector2f(spriteEnemy.Position.X - enemy.enemyStepLong, spriteEnemy.Position.Y);
                }
                else
                {
                    spriteEnemy.Position = new Vector2f(spriteEnemy.Position.X + enemy.enemyStepLong, spriteEnemy.Position.Y);
                }
            }

            if(Program.levelNow.MainCharacter.OnFloor && spriteMainChar.Position.Y < spriteEnemy.Position.Y)
            {
                Jump(enemy);
            }
        }

        private static void deathMainChar(Character character)
        {
            Program.DeadScreenNow = new DeadScreen(Program.MainWindow);
            Program.windowModeNow = Program.windowMode.Dead;
        }

        private static void deathEnemy(Character character)
        {
            Program.levelNow.Enemies.Remove(character);
        }

        private static double mathDistanceToMainChar(Character enemy)
        {
            Vector2f mainCharPos = Program.levelNow.MainCharacter.Sprite.Position;
            Vector2f enemyPos = enemy.Sprite.Position;

            double x1 = (enemyPos.X > mainCharPos.X) ? enemyPos.X : mainCharPos.X;
            double x2 = (enemyPos.X < mainCharPos.X) ? enemyPos.X : mainCharPos.X;

            double y1 = (enemyPos.Y > mainCharPos.Y) ? enemyPos.Y : mainCharPos.Y;
            double y2 = (enemyPos.Y < mainCharPos.Y) ? enemyPos.Y : mainCharPos.Y;

            double xDist = x1 - x2;
            double yDist = y1 - y2;

            return (Math.Sqrt((xDist * xDist) + (yDist * yDist)));
        }

        private static void spawnChar(RenderWindow window, Character character)
        {
            Random rand = new Random(DateTime.Now.Millisecond);
            character.Sprite.FillColor = new Color((byte)(255 * rand.NextDouble()), (byte)(255 * rand.NextDouble()), (byte)(255 * rand.NextDouble()));
            window.Draw(character.Sprite);

            character.TimeToSpawn -= (DateTime.Now - character.TimeNow).TotalSeconds;
            character.TimeNow = DateTime.Now;
        }
    }
}
