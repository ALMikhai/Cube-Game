using SFML.Graphics;
using SFML.System;
using SFML.Window;
using Story_One_Coube.Models;
using Story_One_Coube.Models.Guns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Story_One_Coube.Scene
{
    static class Interface
    {
        static Font font = new Font("../../Texturs/ScoreFont.otf");
        static Text score;

        static Text clip;

        static Sprite inventoryBox;


        public static void Init()
        {
            inventoryBox = new Sprite(new Texture("../../Texturs/Interface/box.png"));
            inventoryBox.Scale = new Vector2f(0.6f, 0.6f);
            inventoryBox.Origin = new Vector2f((inventoryBox.Texture.Size.X) / 2, (inventoryBox.Texture.Size.Y) / 2);
            inventoryBox.Position = new Vector2f(Program.WidthWindow - ((Inventory.Stuff.Count * ((inventoryBox.Texture.Size.X * inventoryBox.Scale.X) + 15))), inventoryBox.Texture.Size.Y * inventoryBox.Scale.Y / 1.5f);
        }

        public static void Draw(RenderWindow window)
        {
            score = new Text("Score: " + Program.levelNow.Score.ToString(), font, 50); // Draw score.
            score.Position = new Vector2f(10, 10);
            window.Draw(score);

            Character mainCharacter = Program.levelNow.MainCharacter; // Draw clip.

            window.Draw(mainCharacter.gunNow.bulletForInterface);

            if (mainCharacter.gunNow.isReloated)
            {
                clip = new Text(mainCharacter.gunNow.clipNow.ToString() + " / " + mainCharacter.gunNow.clips.ToString(), font, 60);
            }
            else
            {
                clip = new Text("Reload " + mainCharacter.gunNow.reloadPercentForInterface.ToString() + " %", font, 60);
            }

            clip.Position = new Vector2f(60, Program.MainWindow.Size.Y - 70);
            window.Draw(clip);

            foreach (var obj in Inventory.Stuff)
            {
                if (obj is Gun)
                {
                    Sprite gun = new Sprite((obj as Gun).Sprite);
                    window.Draw(inventoryBox);

                    gun.Scale = new Vector2f(80f / gun.Texture.Size.X, 40f / gun.Texture.Size.Y);
                    gun.Origin = new Vector2f((gun.Texture.Size.X) / 2, (gun.Texture.Size.Y) / 2);
                    gun.Position = inventoryBox.Position;
                    gun.Rotation = 0;

                    window.Draw(gun);

                    //CircleShape circle = new CircleShape()
                    //{
                    //    Radius = 2.5f,
                    //    FillColor = Color.White,
                    //    Position = new Vector2f(gun.Position.X, gun.Position.Y),
                    //};

                    //circle.Origin = new Vector2f(circle.Radius / 2, circle.Radius / 2);

                    //Program.MainWindow.Draw(circle);

                    inventoryBox.Position = new Vector2f(inventoryBox.Position.X + (inventoryBox.Texture.Size.X * inventoryBox.Scale.X) + 15, inventoryBox.Position.Y);
                }
            }

            inventoryBox.Position = new Vector2f(Program.WidthWindow - ((Inventory.Stuff.Count * ((inventoryBox.Texture.Size.X * inventoryBox.Scale.X) + 15))), inventoryBox.Texture.Size.Y * inventoryBox.Scale.Y / 1.5f);
        }
    }
}
