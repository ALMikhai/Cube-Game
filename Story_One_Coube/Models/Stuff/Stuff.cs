using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;

namespace Story_One_Coube.Models.Stuff
{
    class Stuff
    {
        public Sprite Sprite { get; protected set; }

        public bool OnFloor { get; protected set; }

        public virtual void Draw(RenderWindow window)
        {
            if (CanUsed)
            {
                Sprite.Color = Color.Green;
            }
            else
            {
                Sprite.Color = Color.White;
            }

            window.Draw(Sprite);

            CircleShape circle = new CircleShape()
            {
                Radius = 2.5f,
                FillColor = Color.White,
                Position = new Vector2f(Sprite.Position.X, Sprite.Position.Y),
            };

            circle.Origin = new Vector2f(circle.Radius / 2, circle.Radius / 2);

            Program.MainWindow.Draw(circle);
        }

        public bool CanUsed { get; protected set; }

        public virtual void Update(RenderWindow window)
        {
            for (int i = 0; i != CharacterEvents.gravity; i++)
            {
                if (OnFloor) break;

                int scale = (int)(Sprite.Texture.Size.Y * Sprite.Scale.Y);

                foreach (var platform in Program.levelNow.TextureObjects)
                {
                    if (Sprite.Position.Y + (int)(scale / 2) == platform.Position.Y
                        && platform.Position.X < Sprite.Position.X && Sprite.Position.X < platform.Position.X + platform.Size.X)
                    {
                        OnFloor = true;
                        Sounds.BagDrop.Play();
                        break;
                    }
                }

                if (OnFloor) break;

                Sprite.Position = new Vector2f(Sprite.Position.X, Sprite.Position.Y + 1);
            }

            if (mathDistToStuff(Sprite) < 50)
            {
                CanUsed = true;
            }
            else
            {
                CanUsed = false;
            }
        }

        public virtual bool Event()
        {
            return false;
        }

        protected static double mathDistToStuff(Sprite stuff)
        {
            Vector2f mainCharPos = Program.levelNow.MainCharacter.Sprite.Position;
            Vector2f enemyPos = stuff.Position;

            double x1 = (enemyPos.X > mainCharPos.X) ? enemyPos.X : mainCharPos.X;
            double x2 = (enemyPos.X < mainCharPos.X) ? enemyPos.X : mainCharPos.X;

            double y1 = (enemyPos.Y > mainCharPos.Y) ? enemyPos.Y : mainCharPos.Y;
            double y2 = (enemyPos.Y < mainCharPos.Y) ? enemyPos.Y : mainCharPos.Y;

            double xDist = x1 - x2;
            double yDist = y1 - y2;

            return (Math.Sqrt((xDist * xDist) + (yDist * yDist)));
        }
    }
}
