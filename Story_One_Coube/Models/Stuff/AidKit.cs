using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Story_One_Coube.Models.Stuff
{
    class AidKit : Stuff
    {
        public AidKit(Vector2f spawnPoint)
        {
            Sprite = new Sprite(new Texture("../../Texturs/Interface/MedKit.png"));
            Sprite.Scale = new Vector2f(0.4f, 0.4f);
            Sprite.Origin = new Vector2f(Sprite.Texture.Size.X / 2, Sprite.Texture.Size.Y / 2);
            Sprite.Position = spawnPoint;

            CanUsed = false;
            OnFloor = false;
        }

        public override bool Event()
        {
            if (!CanUsed) return false;

            Sounds.Aid.Play();

            Program.levelNow.MainCharacter.HP.ValueNow = Program.levelNow.MainCharacter.HP.InitialValue;

            Program.levelNow.Stuffs.Remove(this);

            return true;
        }
    }
}
