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
        public static List<object> Stuff { get; set; }

        public static void Init()
        {
            Stuff = new List<object>();

            Stuff.Add(new Pistol(Program.levelNow.MainCharacter.Sprite));
            Stuff.Add(new Smg(Program.levelNow.MainCharacter.Sprite));
            Stuff.Add(new ShotGun(Program.levelNow.MainCharacter.Sprite));
        }

        public static void AddGun(Gun newGun)
        {

        }
    }
}
