using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Story_One_Coube.Scene
{
    internal class Level
    {
        public Level()
        {

        }

        public virtual void Draw(RenderWindow window)
        {
            Interface.Draw(window);
        }
    }
}
