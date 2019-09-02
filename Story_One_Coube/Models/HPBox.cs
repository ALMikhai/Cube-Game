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
        RectangleShape baseSprite;

        public RectangleShape SpriteGreen;
        public RectangleShape SpriteRed;

        double sizeH = 5;
        double sizeW;

        int thickness = 2;

        public double InitialValue;
        public double ValueNow;

        public HPBox(RectangleShape sprite, double HP)
        {
            baseSprite = sprite;

            sizeW = sprite.Size.X;

            InitialValue = HP;
            ValueNow = HP;

            SpriteGreen = new RectangleShape(new Vector2f((float)sizeW, (float)sizeH));

            SpriteGreen.FillColor = new Color(28, 212, 86);

            SpriteGreen.OutlineThickness = thickness;

            SpriteGreen.OutlineColor = Color.Black;

            SpriteRed = new RectangleShape(SpriteGreen);

            SpriteRed.FillColor = Color.Red;
        }

        public void Hit(double damage)
        {
            ValueNow -= damage;

            ValueNow = (ValueNow <= 0) ? 0 : ValueNow;

            double newSizeW = sizeW * (ValueNow / InitialValue);

            SpriteGreen.Size = new Vector2f((float)newSizeW, (float)sizeH);
        }

        public void Update()
        {
            SpriteGreen.Position = new Vector2f(baseSprite.Position.X - (baseSprite.Size.X / 2), (float)(baseSprite.Position.Y - baseSprite.Size.Y / 2) - 20);
            SpriteRed.Position = SpriteGreen.Position;
        }

        public void Draw(RenderWindow window)
        {
            window.Draw(SpriteRed);
            window.Draw(SpriteGreen);
        }
    }
}
