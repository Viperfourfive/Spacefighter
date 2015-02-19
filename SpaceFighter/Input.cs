﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SpaceFighter
{
    class Input
    {
        public KeyboardState newInput;//, oldInput;
        public MouseState newMouse, oldMouse;

        public Input()
        {
            newInput = Keyboard.GetState();
        }
        public bool MouseFire()
        {
            oldMouse = newMouse;
            newMouse = Mouse.GetState();
            if (newMouse.LeftButton == ButtonState.Pressed && oldMouse.LeftButton == ButtonState.Released)
            {
                return true;
            }
            return false;

        }
        public bool PlayerFire()
        {
            return newInput.IsKeyDown(Keys.LeftAlt);
        }
        public Vector2 PlayerMovement(Vector2 playerPos, Rectangle bounds, float shipSpeed)
        {
            //Used for comparing position on game board below, must be before if()
            Vector2 playerPosOld = playerPos;

            newInput = Keyboard.GetState();
            if (newInput.IsKeyDown(Keys.D))       //right
            {
                playerPos.X += 100 * shipSpeed;
            }
            else if (newInput.IsKeyDown(Keys.A))  //left
            {
                playerPos.X -= 100 * shipSpeed;
            }
            else if (newInput.IsKeyDown(Keys.W))  //up
            {
                playerPos.Y -= 100 * shipSpeed;
            }
            else if (newInput.IsKeyDown(Keys.S))  //down
            {
                playerPos.Y += 100 * shipSpeed;
            }

            //Confine movement to viewport only            
            if (!bounds.Contains(playerPos))
            {
                playerPos = playerPosOld;
            }
            return playerPos;
        }
    }
}
