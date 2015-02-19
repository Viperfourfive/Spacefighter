﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceFighter
{
    class Collision
    {
        public Collision()
        {
        }
        public Rectangle CreateBulletRectangle(Vector2 bulletList)
        {
            return new Rectangle((int)bulletList.X, (int)bulletList.Y, 3, 7);
        }
        public Rectangle CreateEnemyRectangle(Vector2 enemyList)
        {
            return new Rectangle((int)enemyList.X, (int)enemyList.Y, 20, 20); 
        }
        public Rectangle CreatePlayerRectangle(Vector2 playerPos)
        {
            return new Rectangle((int)playerPos.X, (int)playerPos.Y, 30, 30);
        }
        //Check for collision against all active game obejcts:  Had help from Vic increasing performance of my loops.
        public int CheckCollision(Vector2 playerPos, List<Vector2> bulletList, List<Vector2> enemyList, int playerScore)
        {
            if (bulletList == null || enemyList == null)
            {
                return playerScore;
            }
            for (int i = 0; i < bulletList.Count; i++)
            {
                //var bulletRectangle = CreateBulletRectangle(bulletList[i]);  //<-- works.

                for (int j = 0; j < enemyList.Count; j++)
                {
                    var bulletRectangle = CreateBulletRectangle(bulletList[i]);  //<-- index out of range on this line.
                    var enemyRectangle = CreateEnemyRectangle(enemyList[j]);

                    if (bulletRectangle.Intersects(enemyRectangle))
                    {
                        enemyList.RemoveAt(j);
                        bulletList.RemoveAt(i);
                        playerScore++;
                        break;
                    }
                }
            }
            for (int j = 0; j < enemyList.Count; j++)
            {
                var EnemyRectangle = CreateEnemyRectangle(enemyList[j]);
                var playerRectangle = CreatePlayerRectangle(playerPos);

                if (EnemyRectangle.Intersects(playerRectangle))
                {
                    enemyList.RemoveAt(j);
                }
                //if health <= 0 {end game} //right place, naive??
            }
            return playerScore;
        }
    }
}
