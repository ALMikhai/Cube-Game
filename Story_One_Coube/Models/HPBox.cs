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
    class HPBox
    {
        Character Character;

        public RectangleShape SpriteGreen;
        public RectangleShape SpriteRed;

        double sizeH = 5;
        double sizeW;

        int thickness = 2;

        double initialValue;
        double valueNow;

        public HPBox(Character character, double HP)
        {
            Character = character;

            sizeW = character.Sprite.Size.X;

            initialValue = HP;
            valueNow = HP;

            SpriteGreen = new RectangleShape(new SFML.System.Vector2f((float)sizeW, (float)sizeH));

            SpriteGreen.FillColor = new Color(28, 212, 86);

            SpriteGreen.OutlineThickness = thickness;

            SpriteGreen.OutlineColor = Color.Black;

            SpriteRed = new RectangleShape(SpriteGreen);

            SpriteRed.FillColor = Color.Red;
        }

        public void Hit(double damage)
        {
            valueNow -= damage;

            double newSizeW = sizeW * (valueNow / initialValue);

            SpriteGreen.Size = new SFML.System.Vector2f((float)newSizeW, (float)sizeH);
        }

        public void Update()
        {
            if (Character == null) return;

            SpriteGreen.Position = new SFML.System.Vector2f(Character.Sprite.Position.X - (Character.Sprite.Size.X / 2), Character.Sprite.Position.Y - Character.Sprite.Size.Y);
            SpriteRed.Position = SpriteGreen.Position;
        }

        public void Draw(RenderWindow window)
        {
            window.Draw(SpriteRed);
            window.Draw(SpriteGreen);
        }
    }
}
