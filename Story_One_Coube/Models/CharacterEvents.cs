#define Debug

using SFML.Graphics;
using SFML.System;
using Story_One_Coube.Scene;
using System;

namespace Story_One_Coube.Models
{
    static class CharacterEvents
    {
        public enum Moves { STOP = 0, UP, DOWN, LEFT, RIGHT }

        public static float gravity = 5;

        public static void Hit(Character character, double damage)
        {
            Sounds.Hit.Play();

            character.HP.Hit(damage);
        }

        public static void Jump(Character character)
        {
            if (character.timesToJump != 0 || !character.OnFloor) return;

            Sounds.Jump.Play();

            character.timesToJump += 30;

            character.OnFloor = false;
        }

        public static void Draw(RenderWindow window, Character character)
        {
            foreach (var bullet in character.bullets)
            {
                bullet.Draw(window);
            }

            character.HP.Draw(window);

            window.Draw(character.Sprite);
            character.gunNow.Draw(window);

#if !Debug

            CircleShape circle = new CircleShape()
            {
                Radius = 2.5f,
                FillColor = Color.White,
                Position = new Vector2f(character.gunNow.StartShootPoint.X, character.gunNow.StartShootPoint.Y),
            };

            circle.Origin = new Vector2f(circle.Radius / 2, circle.Radius / 2);

            Program.MainWindow.Draw(circle);

            RectangleShape rectangle = new RectangleShape()
            {
                Size = (Vector2f)character.gunNow.Sprite.Texture.Size,
                Position = character.gunNow.Sprite.Position,
                Scale = character.gunNow.Sprite.Scale,
                Origin = character.gunNow.Sprite.Origin,
                OutlineThickness = 5,
                FillColor = Color.Transparent,
                Rotation = character.gunNow.Sprite.Rotation,
            };

            Program.MainWindow.Draw(rectangle);
#endif
        }

        public static void UpdateChar(Character character)
        {
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

            for (int i = 0; i != gravity; i++)
            {
                character.OnFloor = false;

                foreach (var platform in Program.levelNow.TextureObjects)
                {
                    if (character.Sprite.Position.Y + character.SizeH / 2 == platform.Position.Y
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
            if(character.HP.ValueNow <= 0)
            {
                //character.HP.ValueNow = character.HP.InitialValue;
                deathMainChar(character);
            }

            character.gunNow.Update(character.Sprite, Program.LastMousePosition);

            if (!CharacterMovesAnimation.JumpFinished)
            {
                CharacterMovesAnimation.MainCharMoveJump();
            }

            switch (move)
            {
                case Moves.STOP:
                    if (character.OnFloor && CharacterMovesAnimation.JumpFinished)
                    {
                        CharacterMovesAnimation.MainCharStand();
                    }
                    break;

                case Moves.LEFT:
                    if ((character.Sprite.Position.X - (character.SizeW / 2)) <= 0) break;

                    if (character.OnFloor && CharacterMovesAnimation.JumpFinished)
                    {
                        switch (character.gunNow.GunSideNow)
                        {
                            case Guns.Gun.GunSide.Left:
                                {
                                    CharacterMovesAnimation.MainCharMoveLeft();
                                    break;
                                }

                            case Guns.Gun.GunSide.Right:
                                {
                                    CharacterMovesAnimation.MainCharMoveLeftBack();
                                    break;
                                }
                        }
                    }

                    character.Sprite.Position = new Vector2f(character.Sprite.Position.X - character.stepLong, character.Sprite.Position.Y);
                    break;

                case Moves.RIGHT:
                    if ((character.Sprite.Position.X + (character.SizeW / 2)) >= Program.WidthWindow) break;

                    if (character.OnFloor && CharacterMovesAnimation.JumpFinished)
                    {
                        switch (character.gunNow.GunSideNow)
                        {
                            case Guns.Gun.GunSide.Right:
                                {
                                    CharacterMovesAnimation.MainCharMoveRight();
                                    break;
                                }

                            case Guns.Gun.GunSide.Left:
                                {
                                    CharacterMovesAnimation.MainCharMoveRightBack();
                                    break;
                                }
                        }
                    }

                    character.Sprite.Position = new Vector2f(character.Sprite.Position.X + character.stepLong, character.Sprite.Position.Y);
                    break;
            }
        }

        public static void UpdateEnemy(Character character)
        {
            character.gunNow.Update(character.Sprite, new Point(Program.levelNow.MainCharacter.Sprite.Position.X, Program.levelNow.MainCharacter.Sprite.Position.Y));

            if (character.HP.ValueNow <= 0)
            {
                Program.levelNow.Score += 10;
                deathEnemy(character);
                return;
            }

            if (CanShoot(character))
            {
                character.gunNow.EnemyShoot(character, new Point(Program.levelNow.MainCharacter.Sprite.Position.X, Program.levelNow.MainCharacter.Sprite.Position.Y));
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
            Sprite spriteMainChar = Program.levelNow.MainCharacter.Sprite;
            Sprite spriteEnemy = enemy.Sprite;

            if(mathDistanceToMainChar(enemy) > enemy.enemyAllowableDisToMainChar)
            {
                if(spriteEnemy.Position.X > spriteMainChar.Position.X)
                {
                    if (enemy.OnFloor)
                    {
                        enemy.EnemyAnimation.EnemyMoveLeft();
                    }
                    spriteEnemy.Position = new Vector2f(spriteEnemy.Position.X - enemy.enemyStepLong, spriteEnemy.Position.Y);
                }
                else
                {
                    if (enemy.OnFloor)
                    {
                        enemy.EnemyAnimation.EnemyMoveRight();
                    }
                    spriteEnemy.Position = new Vector2f(spriteEnemy.Position.X + enemy.enemyStepLong, spriteEnemy.Position.Y);
                }
            }
            else
            {
                enemy.EnemyAnimation.EnemyStand();
            }

            if(Program.levelNow.MainCharacter.OnFloor && spriteMainChar.Position.Y < spriteEnemy.Position.Y)
            {
                Jump(enemy);
            }
        }

        private static void deathMainChar(Character character)
        {
            DeadScreen.Restart();
            Program.windowModeNow = Program.WindowMode.Dead;
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
            //Random rand = new Random(DateTime.Now.Millisecond);
            //character.Sprite.FillColor = new Color((byte)(255 * rand.NextDouble()), (byte)(255 * rand.NextDouble()), (byte)(255 * rand.NextDouble()));
            //window.Draw(character.Sprite);

            //character.TimeToSpawn -= (DateTime.Now - character.TimeNow).TotalSeconds;
            //character.TimeNow = DateTime.Now;
        }
    }
}
