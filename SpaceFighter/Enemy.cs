using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceFighter
{
    class Enemy
    {
        public List<Vector2> enemyList = new List<Vector2>();
        Vector2 enemyPos;
        float enemySpeed = 0.20f;
        int playerCounter, randomCounter;
        int timer = 100;
        public int enemyScore;

        public List<Vector2> StoreEnemy(Vector2 playerPos)
        {
            PlayerFollow(playerPos);
            RandomStart();

            if (enemyList != null)
            {
                for (int i = 0; i < enemyList.Count; i++)
                {
                    enemyPos = enemyList[i];
                    enemyPos.Y += 15 * enemySpeed;
                    enemyList[i] = enemyPos;
                }
            }
            return this.enemyList;
        }
        public void PlayerFollow(Vector2 playerPos)
        {
            if (playerCounter == 50)
            {
                var enemyStart = new Vector2(playerPos.X + 5, 120);
                this.AddEnemy(enemyStart);
                playerCounter = 0;
            }
            playerCounter++;
        }
        public void RandomStart()
        {
            if (randomCounter == timer)
            {
                var rand = new Random();
                var enemyStart = new Vector2(rand.Next(301, 870), 120);
                this.AddEnemy(enemyStart);
                timer--;
                randomCounter = 0;
            }
            randomCounter++;
        }
        public void AddEnemy(Vector2 enemyStart)
        {
            if (enemyList == null)
            {
                this.enemyList[0] = new Vector2(enemyStart.X - enemySpeed, enemyStart.Y);
            }
            else
            {
                var newEnemy = new Vector2(enemyStart.X - enemySpeed, enemyStart.Y);
                this.enemyList.Add(newEnemy);
            }
        }
        public void removeEnemy(Rectangle bounds)
        {
            //out of bounds.
            if (enemyList != null)
            {
                for (int i = 0; i < enemyList.Count; i++)
                {
                    if ((bounds.Contains(enemyList[i]) == false))
                    {
                        enemyList.RemoveAt(i);
                        enemyScore++;
                    }
                }
            }
        }
        public void DrawEnemy(Texture2D enemySprite, SpriteBatch _spriteBatch, SpriteFont font)
        {
            for (int i = 0; i < enemyList.Count; i++)
            {
                if (enemyList[i] != null)
                {
                    enemyPos = enemyList[i];
                    _spriteBatch.Draw(enemySprite, enemyPos, null, Color.Red, 0, Vector2.Zero, new Vector2(20, 20), SpriteEffects.None, 0);
                    //_spriteBatch.DrawString(font, i.ToString(), enemyPos, Color.White, 0, Vector2.Zero, new Vector2(.6f, .6f), SpriteEffects.None, 0);
                }
            }
        }
    }
}
