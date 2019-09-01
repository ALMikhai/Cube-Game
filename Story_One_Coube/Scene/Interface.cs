using SFML.Graphics;
using SFML.System;
using SFML.Window;
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

        public static void Draw(RenderWindow window)
        {
            score = new Text("Score: " + Program.Score.ToString(), font, 50);
            score.Position = new Vector2f(10, 10);
            window.Draw(score);
        }
    }
}
