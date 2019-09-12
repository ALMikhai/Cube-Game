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
        public static List<Gun> Guns { get; private set; }

        public static void Init()
        {
            Guns = new List<Gun>();
        }

        public static void AddGun(Gun newGun)
        {
            Guns.Add(newGun);
        }

        public static void Clear()
        {
            Guns.Clear();
        }
    }
}
