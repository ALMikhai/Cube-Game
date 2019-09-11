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

        static SoundBuffer shotGunShootBuf = new SoundBuffer("../../Sounds/ShotGunShoot.wav");
        public static Sound ShotGunShoot { get; private set; }
        static SoundBuffer shotGunReloadBuf = new SoundBuffer("../../Sounds/ShotGunReload.wav");
        public static Sound ShotGunReload { get; private set; }

        static SoundBuffer smgShootBuf = new SoundBuffer("../../Sounds/SmgShoot.wav");
        public static Sound SmgShoot { get; private set; }
        static SoundBuffer smgReloadBuf = new SoundBuffer("../../Sounds/SmgReload.wav");
        public static Sound SmgReload { get; private set; }



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

            ShotGunShoot = new Sound(shotGunShootBuf);
            ShotGunShoot.Volume = 30;
            ShotGunReload = new Sound(shotGunReloadBuf);
            ShotGunReload.Volume = 30;

            SmgShoot = new Sound(smgShootBuf);
            SmgShoot.Volume = 30;
            SmgReload = new Sound(smgReloadBuf);
            SmgReload.Volume = 30;

            Jump = new Sound(jumpBuf);
            Jump.Volume = 30;
            Hit = new Sound(hitBuf);
            Hit.Volume = 30;
        }
    }
}
