using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Story_One_Coube.Models.CharacterMovesAnimation;

namespace Story_One_Coube.Models
{
    class EnemyMovesAnumation
    {
        DateTime timeNow = DateTime.Now;

        double timeToOneFrame = 0.07;
        int frameNow = 0;

        Character enemy;

        public EnemyMovesAnumation(Character character)
        {
            enemy = character;
        }

        public void EnemyMoveLeft()
        {
            if (DateTime.Now.Subtract(timeNow).TotalSeconds < timeToOneFrame) return;

            enemy.Sprite.Texture = moveLeftEnemy[frameNow++];

            if (frameNow == numMovesEnemyLeft)
            {
                frameNow = 0;
            }

            timeNow = DateTime.Now;
        }

        public void EnemyMoveRight()
        {
            if (DateTime.Now.Subtract(timeNow).TotalSeconds < timeToOneFrame) return;

            enemy.Sprite.Texture = moveRightEnemy[frameNow++];

            if (frameNow == numMovesEnemyRight)
            {
                frameNow = 0;
            }

            timeNow = DateTime.Now;
        }

        public void EnemyStand()
        {
            frameNow = 0;
            enemy.Sprite.Texture = StandEnemyTexture;
        }
    }
}
