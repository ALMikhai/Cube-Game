using SFML.Graphics;
using SFML.System;
using SFML.Window;
using Story_One_Coube.Models;
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
        }
    }
}
