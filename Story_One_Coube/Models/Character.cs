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
    class Character
    {
        public enum Moves { STOP = 0, UP, DOWN, LEFT, RIGHT}

        public RectangleShape Sprite { get; set; }

        public int SizeH { get; set; }
        public int SizeW { get; set; }

        public int Thickness { get; set; }

        float gravity = 5;
        float jumpHeight = 15;

        float stepLong = 5;

        public uint timesToJump = 0;

        public Gun gunNow { get; set; }

        public List<Bullet> bullets = new List<Bullet>();

        public HPBox HP { get; set; }

        public Character(double hp, int height, int width, Point spawnPoint)
        {
            SizeH = height;
            SizeW = width;

            Thickness = 2;

            Sprite = new RectangleShape(new SFML.System.Vector2f(SizeW, SizeH));

            Sprite.Origin = new SFML.System.Vector2f(SizeW / 2, SizeH / 2);

            Sprite.Position = new SFML.System.Vector2f((float)spawnPoint.X, (float)spawnPoint.Y);

            Sprite.OutlineThickness = Thickness;

            Sprite.OutlineColor = Color.Red;

            gunNow = new Gun(this.Sprite);

            HP = new HPBox(this.Sprite, hp);
        }

        public void Update(Moves move)
        {
            if (Sprite == null) return;

            gunNow.Update(this.Sprite, Program.lastMousePosition);

            foreach (var bullet in bullets)
            {
                bullet.Update();
            }

            HP.Update();

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

            for(int i = 0;  Sprite.Position.Y + Thickness + SizeH / 2 != Program.heightWindow && i != gravity; i++)
            {
                Sprite.Position = new Vector2f(Sprite.Position.X, Sprite.Position.Y + 1);
            }
        }

        public void Draw(RenderWindow window)
        {
            if (Sprite == null) return;

            foreach(var bullet in bullets)
            {
                bullet.Draw(window);
            }

            HP.Draw(window);

            window.Draw(Sprite);
            window.Draw(gunNow.Sprite);
        }
    }
}
