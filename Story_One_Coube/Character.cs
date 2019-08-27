using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Story_One_Coube
{
    class Character
    {
        public enum Moves { STOP = 0, UP, DOWN, LEFT, RIGHT}

        public RectangleShape Sprite;

        int sizeH = 50;
        int sizeW = 50;

        float gravity = 5;
        float jumpHeight = 15;

        float stepLong = 5;

        uint timesToJump = 0;

        public Character()
        {
            Sprite = new RectangleShape(new SFML.System.Vector2f(sizeW, sizeH));

            Sprite.Origin = new SFML.System.Vector2f(sizeW / 2, sizeH / 2);

            Sprite.Position = new SFML.System.Vector2f(Program.width / 2, Program.height / 2);

            Sprite.OutlineThickness = 2;

            Sprite.OutlineColor = Color.Red;
        }

        public void Update(Moves move)
        {
            if (Sprite == null) return;

            switch (move)
            {
                case Moves.LEFT:
                    Sprite.Position = new SFML.System.Vector2f(Sprite.Position.X - stepLong, Sprite.Position.Y);
                    break;

                case Moves.RIGHT:
                    Sprite.Position = new SFML.System.Vector2f(Sprite.Position.X + stepLong, Sprite.Position.Y);
                    break;
            }

            if(timesToJump != 0)
            {
                timesToJump--;
                Sprite.Position = new SFML.System.Vector2f(Sprite.Position.X, Sprite.Position.Y - jumpHeight);
            }

            if(Sprite.Position.Y + sizeH / 2 != Program.height)
            {
                Sprite.Position = new SFML.System.Vector2f(Sprite.Position.X, Sprite.Position.Y + gravity);
            }
        }

        public void Draw(RenderWindow window)
        {
            if (Sprite == null) return;

            window.Draw(Sprite);
        }

        public void Jump()
        {
            if (timesToJump != 0 || Sprite.Position.Y + sizeH / 2 != Program.height) return;

            timesToJump += 30;
        }
    }
}
