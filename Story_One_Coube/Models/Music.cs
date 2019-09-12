using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using SFML.Audio;

namespace Story_One_Coube.Models
{
    static class Musics
    {
        public static Music MainMenu;
        public static Music DeadScreen;
        public static Music WinScreen;
        public static Music Level1;

        public static void Init()
        {
            MainMenu = new Music("../../Music/MainMenu.wav");
            MainMenu.Loop = true;
            MainMenu.Volume = 25;

            Level1 = new Music("../../Music/Level1.wav");
            Level1.Loop = true;
            Level1.Volume = 20;

            DeadScreen = new Music("../../Music/GameOver.ogg");
            DeadScreen.Loop = true;
            DeadScreen.Volume = 20;

            WinScreen = new Music("../../Music/LevelWin.ogg");
            WinScreen.Loop = true;
            WinScreen.Volume = 20;
        }
    }
}
