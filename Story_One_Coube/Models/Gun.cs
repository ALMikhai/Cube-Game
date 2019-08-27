using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;

namespace Story_One_Coube.Models
{
    class Gun
    {
        public RectangleShape Sprite;

        int sizeH = 10;
        int sizeW = 20;

        public Gun(Character character)
        {
            Sprite = new RectangleShape(new SFML.System.Vector2f(sizeW, sizeH));
            Sprite.FillColor = Color.Black;

            Sprite.Position = character.Sprite.Position;
        }

        public void Update(Character character)
        {
            if (Sprite == null) return;

            Sprite.Position = character.Sprite.Position;
        }
    }
}
