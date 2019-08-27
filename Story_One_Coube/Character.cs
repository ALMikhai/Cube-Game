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
        public RectangleShape Sprite;

        int sizeH = 50;
        int sizeW = 50;

        float gravity = 1;

        public Character()
        {
            Sprite = new RectangleShape(new SFML.System.Vector2f(sizeW, sizeH));

            Sprite.Origin = new SFML.System.Vector2f(sizeW / 2, sizeH / 2);

            Sprite.Position = new SFML.System.Vector2f(Program.width / 2, Program.height / 2);
        }

        public void Update()
        {
            if (Sprite == null) return;

            if(Sprite.Position.Y + sizeH / 2 != Program.height)
            {
                Sprite.Position = new SFML.System.Vector2f(Sprite.Position.X, Sprite.Position.Y + gravity);
            }
        }
    }
}
