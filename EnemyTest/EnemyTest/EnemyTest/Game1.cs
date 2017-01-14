using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace EnemyTest
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {

        //THIS STORY IS HAPPY END

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        int originH;
        int originW;
        int size;
        Texture2D doug;
        float playerAngle;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related con
        /// tent.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            originH = graphics.PreferredBackBufferHeight / 2;
            originW = graphics.PreferredBackBufferWidth / 2;
            size = 500;

            playerAngle = MathHelper.Pi / 2;

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            doug = Content.Load<Texture2D>("kermitTheDog");

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here

            if (size > 0)
            {
                size -= 5;
            }
            else
            {
                size = 500;
            }

            /*
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                size += 10;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                size -= 10;
            }
            */

            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                playerAngle += MathHelper.Pi / 16;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                playerAngle -= MathHelper.Pi / 16;
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();

            spriteBatch.Draw(doug, new Vector2((float)((Math.Cos((double)playerAngle)*size)+225), (float)((Math.Sin((double)playerAngle))*size)+50), Color.White);

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
