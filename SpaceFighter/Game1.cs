using System;
using System.Collections.Generic;
using System.Drawing;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SpaceFighter
{
    public class Game1 : Game
    {
        GraphicsDeviceManager _graphics;
        SpriteBatch _spriteBatch;
        SpriteFont font;

        //Naive??
        Bullet _bullet;
        Enemy _enemy;
        Collision _c;
        Input _i;

        public int objectCount = 0;  //debug
        public int counter = 0;

        //FPS getter, derived from: http://blogs.msdn.com/b/shawnhar/archive/2007/06/08/displaying-the-framerate.aspx
        int frameRate = 0;
        int frameCounter = 0;
        TimeSpan elapsedTime = TimeSpan.Zero;

        //I should refactor these into classes...  //player //position //getter for the sprites and add data to those classes.
        Rectangle bounds;
        Vector2 playerPos, enemyPos;
        Texture2D playerSprite, bulletSprite, boundsSprite, enemySprite;
        float shipSpeed = 0.05f;
        public int playerScore = 0;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.IsFullScreen = false;
            IsMouseVisible = true;
            _graphics.PreferredBackBufferHeight = 900;
            _graphics.PreferredBackBufferWidth = 1200;
            Content.RootDirectory = "Content";
        }
        protected override void Initialize()
        {            
            base.Initialize();
        }
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            font = Content.Load<SpriteFont>("font");

            //Naive??
            _bullet = new Bullet();
            _enemy = new Enemy();
            _c = new Collision();
            _i = new Input();

            //Generate player, bullet, enemey, and game boundry textures and starting info
            playerSprite = new Texture2D(_graphics.GraphicsDevice, 1, 1);
            playerSprite.SetData<Color>(new[] { Color.White });
            playerPos = _graphics.GraphicsDevice.Viewport.Bounds.Center.ToVector2();
            playerPos = new Vector2(550, 550);

            bulletSprite = new Texture2D(_graphics.GraphicsDevice, 1, 1);
            bulletSprite.SetData<Color>(new[] { Color.White });

            enemySprite = new Texture2D(_graphics.GraphicsDevice, 1, 1);
            enemySprite.SetData<Color>(new[] { Color.White });
            enemyPos = _graphics.GraphicsDevice.Viewport.Bounds.Center.ToVector2();
            enemyPos = new Vector2(50, 550);

            boundsSprite = Content.Load<Texture2D>("border");
            bounds = new Rectangle(300, 100, boundsSprite.Width-25, boundsSprite.Height-25);
        }
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }
        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape) == true)
                Exit();

            //FPS getter, derived from: http://blogs.msdn.com/b/shawnhar/archive/2007/06/08/displaying-the-framerate.aspx
            elapsedTime += gameTime.ElapsedGameTime;
            if (elapsedTime > TimeSpan.FromSeconds(1))
            {
                elapsedTime -= TimeSpan.FromSeconds(1);
                frameRate = frameCounter;
                frameCounter = 0;
            }
            frameCounter++;

            //Update enemy Positional information, should combine into 1 update method into the enemy class...
            _enemy.StoreEnemy(playerPos);
            _enemy.removeEnemy(bounds);

            //Update user input for current frame
            Input _i = new Input();
            //_i = new Input();

            //Move player
            playerPos = _i.PlayerMovement(playerPos, bounds, shipSpeed);

            //Fire projectiles
            
            if (counter > 10) //control fireRate
            {
                if (_i.MouseFire() == true)
                {
                    _bullet.AddBullets(playerPos);
                    counter = 0;
                }
            }
            counter++;

            //Update bullet Positional information, should combine into 1 update method into the bullet class...
            _bullet.StoreBullets(playerPos);
            _bullet.removeBullets(bounds);
            
            //getting player score here seems naive... but should work for my purposes for now.
            playerScore = _c.CheckCollision(playerPos, _bullet.bulletList, _enemy.enemyList, playerScore);

            //debug - Count active objects available for being rendered "+ 1" = player
            objectCount = _bullet.bulletList.Count + _enemy.enemyList.Count + 1;  

            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin();
            _spriteBatch.Draw(boundsSprite, new Vector2(300, 100), Color.White);
            _spriteBatch.Draw(playerSprite, playerPos, null, Color.White, 0, Vector2.Zero, new Vector2(30,30), SpriteEffects.None, 0 );
            _spriteBatch.DrawString(font, "rendered objects: " + objectCount, new Vector2(5, 5), Color.Yellow, 0, Vector2.Zero, new Vector2(.6f, .6f), SpriteEffects.None, 0);
            _spriteBatch.DrawString(font, "FPS: " + frameRate, new Vector2(5, 20), Color.Yellow, 0, Vector2.Zero, new Vector2(.6f, .6f), SpriteEffects.None, 0);
            _spriteBatch.DrawString(font, "bulletListCount: " + _bullet.bulletList.Count, new Vector2(5, 50), Color.White, 0, Vector2.Zero, new Vector2(.6f, .6f), SpriteEffects.None, 0);
            _spriteBatch.DrawString(font, "enemyListCount: " + _enemy.enemyList.Count, new Vector2(5, 65), Color.White, 0, Vector2.Zero, new Vector2(.6f, .6f), SpriteEffects.None, 0);
            _spriteBatch.DrawString(font, "fireCounter: " + counter, new Vector2(5, 35), Color.White, 0, Vector2.Zero, new Vector2(.6f, .6f), SpriteEffects.None, 0);
            _spriteBatch.DrawString(font, "Player Score: " + playerScore, new Vector2(915, 100), Color.SeaGreen, 0, Vector2.Zero, new Vector2(.6f, .6f), SpriteEffects.None, 0);
            _spriteBatch.DrawString(font, "Enemy Score: " + _enemy.enemyScore, new Vector2(915, 115), Color.SeaGreen, 0, Vector2.Zero, new Vector2(.6f, .6f), SpriteEffects.None, 0);
            _enemy.DrawEnemy(enemySprite, _spriteBatch, font);
            _bullet.DrawBullets(bulletSprite, _spriteBatch, font);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
