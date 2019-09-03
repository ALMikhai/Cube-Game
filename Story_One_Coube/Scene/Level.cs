using SFML.Graphics;
using Story_One_Coube.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Story_One_Coube.Scene
{
    internal class Level
    {
        public Character MainCharacter;
        public double mainCharacterHP = 100;
        public CharacterEvents.Moves moveNow = CharacterEvents.Moves.STOP;

        public List<Character> Enemies = new List<Character>();

        public List<RectangleShape> TextureObjects = new List<RectangleShape>();

        public int Score = 0;

        public Level()
        {
            MainCharacter = Character.SpawnCharacter(mainCharacterHP, new Point(Program.WidthWindow / 2, Program.HeightWindow / 2), CharacterMovesAnimation.StandMainCharTexture);
        }

        public virtual Level RestartLevel()
        {
            Program.windowModeNow = Program.windowMode.Game;
            return null;
        }

        public virtual void Update(RenderWindow window)
        {
            CharacterEvents.UpdateChar(MainCharacter);

            CharacterEvents.UpdateMainChar(moveNow, MainCharacter);

            foreach (var enemy in Enemies.ToArray())
            {
                CharacterEvents.UpdateChar(enemy);
                CharacterEvents.UpdateEnemy(enemy);
            }
        }

        public virtual void Draw(RenderWindow window)
        {
            CharacterEvents.Draw(window, MainCharacter);

            foreach (var enemy in Enemies.ToArray())
            {
                CharacterEvents.Draw(window, enemy);
            }

            Interface.Draw(window);
        }
    }
}
