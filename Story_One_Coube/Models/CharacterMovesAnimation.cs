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

        public static List<Texture> moveLeftEnemy = new List<Texture>();
        static string moveEnemyLeftPath = "../../Texturs/EnemyMoves/MoveLeft/";
        static int numMovesEnemyLeft = 7;

        static List<Texture> moveRightMainChar = new List<Texture>();
        static string moveRightPath = "../../Texturs/MainCharMoves/MoveRight/";
        static int numMovesRight = 7;

        public static List<Texture> moveRightEnemy = new List<Texture>();
        static string moveEnemyRightPath = "../../Texturs/EnemyMoves/MoveRight/";
        static int numMovesEnemyRight = 7;

        static List<Texture> moveJumpMainChar = new List<Texture>();
        static string moveJumpPath = "../../Texturs/MainCharMoves/Jump/";
        static int numMovesJump = 6;
        static int jumpFrameNow = 0;
        public static bool JumpFinished = true;
        static double timeToOneJumpFrame = 0.1;

        static double timeToOneFrame = 0.07;
        static int frameNow = 0;

        static DateTime timeNow = DateTime.Now;

        public static void Init()
        {
            initChar(moveLeftMainChar, moveLeftPath, numMovesLeft);
            initChar(moveRightMainChar, moveRightPath, numMovesRight);
            initChar(moveJumpMainChar, moveJumpPath, numMovesJump);
            initChar(moveLeftEnemy, moveEnemyLeftPath, numMovesEnemyLeft);
            initChar(moveRightEnemy, moveEnemyRightPath, numMovesEnemyRight);
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

        public static void MainCharMoveRight()
        {
            if (DateTime.Now.Subtract(timeNow).TotalSeconds < timeToOneFrame) return;

            Character character = Program.levelNow.MainCharacter;

            character.Sprite.Texture = moveRightMainChar[frameNow++];

            if (frameNow == moveLeftMainChar.Count)
            {
                frameNow = 0;
            }

            timeNow = DateTime.Now;
        }

        public static void MainCharMoveJump()
        {
            if (DateTime.Now.Subtract(timeNow).TotalSeconds < timeToOneJumpFrame) return;

            Character character = Program.levelNow.MainCharacter;

            character.Sprite.Texture = moveJumpMainChar[jumpFrameNow++];

            if (jumpFrameNow == moveJumpMainChar.Count || character.OnFloor)
            {
                jumpFrameNow = 0;
                JumpFinished = true;
                MainCharStand();
            }

            timeNow = DateTime.Now;
        }

        public static void MainCharStand()
        {
            frameNow = 0;
            Program.levelNow.MainCharacter.Sprite.Texture = StandMainCharTexture;
        }
    }
}
