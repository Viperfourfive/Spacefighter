using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceFighter
{
    public class Bullet
    {
        public List<Vector2> bulletList = new List<Vector2>();
        Vector2 bulletPos;
        float bulletSpeed = 0.20f;
                
        public List<Vector2> StoreBullets(Vector2 playerPos)
        {
            if(bulletList != null)
            {
                for (int i = 0; i < bulletList.Count; i++)
                {
                    bulletPos = bulletList[i];
                    bulletPos.Y -= 100 * bulletSpeed;
                    bulletList[i] = bulletPos;
                }
            }
            return this.bulletList;
        }
        public void AddBullets(Vector2 playerPos)
        {
            if (bulletList == null)
            {
                this.bulletList[0] = new Vector2(playerPos.X + 15, playerPos.Y);
            }
            else //if (bulletList.Count <= 10) //limit total bullets on screen.
            {
                var newBullet = new Vector2(playerPos.X + 15, playerPos.Y);
                this.bulletList.Add(newBullet);
            }
        }
        public void removeBullets(Rectangle bounds)
        {
            if (bulletList != null)
            {
                for (int i = 0; i < bulletList.Count; i++)
                {
                    if ((bounds.Contains(bulletList[i]) == false))
                    {
                        bulletList.RemoveAt(i);
                    }
                }
            }
        }
        public void DrawBullets(Texture2D bulletSprite, SpriteBatch _spriteBatch, SpriteFont font)
        {
            for (int i = 0; i < bulletList.Count; i++ )
                {
                    if(bulletList[i] != null)
                    {
                        var bulletPos = bulletList[i];
                        _spriteBatch.Draw(bulletSprite, bulletPos, null, Color.White, 0, Vector2.Zero, new Vector2(3, 7), SpriteEffects.None, 0);
                       // _spriteBatch.DrawString(font, i.ToString(), new Vector2(bulletPos.X + 5, bulletPos.Y), Color.White, 0, Vector2.Zero, new Vector2(.6f, .6f), SpriteEffects.None, 0);
                       // _spriteBatch.DrawString(font, "X: " + bulletList[i].X + ",Y: " + bulletList[i].Y, new Vector2(bulletPos.X + 20, bulletPos.Y), Color.White, 0, Vector2.Zero, new Vector2(.6f, .6f), SpriteEffects.None, 0);
                    } 
                }
        }
    }
}