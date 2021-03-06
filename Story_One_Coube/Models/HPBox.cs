﻿using SFML.Graphics;
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
        Character baseCharacter;

        public RectangleShape SpriteGreen;
        public RectangleShape SpriteRed;

        double sizeH = 5;
        double sizeW;

        int thickness = 2;

        public double InitialValue;
        public double ValueNow;

        public HPBox(Character character, double HP)
        {
            baseCharacter = character;

            sizeW = character.SizeW;

            InitialValue = HP;
            ValueNow = HP;

            SpriteGreen = new RectangleShape(new Vector2f((float)sizeW, (float)sizeH));

            SpriteGreen.FillColor = new Color(28, 212, 86);

            SpriteGreen.OutlineThickness = thickness;

            SpriteGreen.OutlineColor = Color.Black;

            SpriteRed = new RectangleShape(SpriteGreen);

            SpriteRed.Origin = new Vector2f(SpriteRed.Size.X / 2, SpriteRed.Size.Y / 2);

            SpriteRed.FillColor = Color.Red;
        }

        public void Hit(double damage)
        {
            ValueNow -= damage;

            ValueNow = (ValueNow <= 0) ? 0 : ValueNow;
        }

        public void Update()
        {
            double newSizeW = sizeW * (ValueNow / InitialValue);

            SpriteGreen.Size = new Vector2f((float)newSizeW, (float)sizeH);

            SpriteRed.Position = new Vector2f(baseCharacter.Sprite.Position.X, baseCharacter.Sprite.Position.Y - (baseCharacter.SizeH / 2) - 20);
            SpriteGreen.Position = new Vector2f(SpriteRed.Position.X - SpriteRed.Size.X / 2, SpriteRed.Position.Y - SpriteRed.Size.Y / 2);
        }

        public void Draw(RenderWindow window)
        {
            window.Draw(SpriteRed);
            window.Draw(SpriteGreen);
        }
    }
}
