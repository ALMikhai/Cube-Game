using Story_One_Coube.Models.Guns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Story_One_Coube.Models
{
    static class Inventory
    {
        public static List<Gun> Guns { get; set; }

        public static void Init()
        {
            Guns = new List<Gun>();

            Guns.Add(new Pistol(Program.levelNow.MainCharacter.Sprite));
            Guns.Add(new Smg(Program.levelNow.MainCharacter.Sprite));
            Guns.Add(new ShotGun(Program.levelNow.MainCharacter.Sprite));
        }

        public static void AddGun(Gun newGun)
        {

        }
    }
}
