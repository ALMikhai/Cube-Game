using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;

namespace Story_One_Coube.Models.Stuff
{
    class Ammo : Stuff
    {
        public Ammo(Vector2f spawnPoint)
        {
            Sprite = new Sprite(new Texture("../../Texturs/Interface/AmmoBox.png"));
            Sprite.Scale = new Vector2f(0.24f, 0.24f);
            Sprite.Origin = new Vector2f(Sprite.Texture.Size.X / 2, Sprite.Texture.Size.Y / 2);
            Sprite.Position = spawnPoint;

            CanUsed = false;
            OnFloor = false;
        }

        public override bool Event()
        {
            if (!CanUsed) return false;

            Sounds.Ammo.Play();

            foreach(var gun in Inventory.Guns)
            {
                gun.AddAmmo();
            }

            Program.levelNow.Stuffs.Remove(this);

            return true;
        }
    }
}
