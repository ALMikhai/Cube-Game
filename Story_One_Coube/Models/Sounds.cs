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
    static class Sounds
    {
        static SoundBuffer pistolShootBuf = new SoundBuffer("../../Sounds/PistolShoot.wav");
        public static Sound PistolShoot { get; private set; }
        static SoundBuffer pistolReloadBuf = new SoundBuffer("../../Sounds/PistolReload.wav");
        public static Sound PistolReload { get; private set; }
        static SoundBuffer jumpBuf = new SoundBuffer("../../Sounds/Jump.wav");
        public static Sound Jump { get; private set; }
        static SoundBuffer hitBuf = new SoundBuffer("../../Sounds/Hit.wav");
        public static Sound Hit { get; private set; }

        public static void Init()
        {
            PistolShoot = new Sound(pistolShootBuf);
            PistolShoot.Volume = 30;
            PistolReload = new Sound(pistolReloadBuf);
            PistolReload.Volume = 30;
            Jump = new Sound(jumpBuf);
            Jump.Volume = 30;
            Hit = new Sound(hitBuf);
            Hit.Volume = 30;
        }
    }
}
