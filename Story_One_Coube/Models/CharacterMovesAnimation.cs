using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Story_One_Coube.Models
{
    static class CharacterMovesAnimation
    {
        public static Texture StandMainCharTexture = new Texture("../../Texturs/MainCharMoves/Stand/1.png", new IntRect(5, 3, 23, 25));
        public static Texture StandEnemyTexture = new Texture("../../Texturs/EnemyMoves/Stand/1.png", new IntRect(5, 3, 23, 25));

        static List<Texture> moveLeftMainChar = new List<Texture>();
        static string moveLeftPath = "../../Texturs/MainCharMoves/MoveLeft/";
        static int numMovesLeft = 7;
        static double timeToOneFrame = 0.07;
        static int frameNow = 0;

        static DateTime timeNow = DateTime.Now;

        public static void Init()
        {
            initChar(moveLeftMainChar, moveLeftPath, numMovesLeft);
        }

        private static void initChar(List<Texture> textures, string path, int numOfTextures)
        {
            for(var i = 1; i <= numOfTextures; i++)
            {
                textures.Add(new Texture(path + i.ToString() + ".png", new IntRect(5, 3, 23, 25)));
            }
        }

        public static void MainCharMoveLeft()
        {
            if (DateTime.Now.Subtract(timeNow).TotalSeconds < timeToOneFrame) return;

            Character character = Program.levelNow.MainCharacter;

            character.Sprite.Texture = moveLeftMainChar[frameNow++];

            if(frameNow == moveLeftMainChar.Count)
            {
                frameNow = 0;
            }

            timeNow = DateTime.Now;
        }

        public static void MainCharStand()
        {
            Program.levelNow.MainCharacter.Sprite.Texture = StandMainCharTexture;
        }
    }
}
