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

namespace RotationTest
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont font;
        Texture2D planet; //  87 by 87
        Texture2D player; //  30 by 15
        Texture2D enemy;  //  30 by 30
        Vector2 playerPosition;
        const int PLAYER_RADIUS = 100;
        int radius;
        double playerAngle;
        int originX;
        int originY;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            radius = 500;
            playerAngle = MathHelper.Pi / 2;

            originX = (graphics.PreferredBackBufferWidth / 2) - (87 / 2);
            originY = (graphics.PreferredBackBufferHeight / 2) - (87 / 2);

            playerPosition = new Vector2((graphics.PreferredBackBufferWidth / 2)-(30/2), (graphics.PreferredBackBufferHeight / 2)-(15/2)-100);

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

            planet = Content.Load<Texture2D>("TESTPlanet");
            player = Content.Load<Texture2D>("TESTSatellite");
            enemy = Content.Load<Texture2D>("TESTEnemy");

            font = Content.Load<SpriteFont>("myFont");

            // TODO: use this.Content to load your game content here
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

            if(Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                playerAngle += MathHelper.Pi / 16;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                playerAngle -= MathHelper.Pi / 16;
            }

            if(radius <= 87 - (30/2))
            {
                radius = 500;
            }
            else 
            {
                radius -= 2;
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Yellow);

            // TODO: Add your drawing code here
            spriteBatch.Begin();

            //Draw String
            spriteBatch.DrawString(font, "Player Angle: " + playerAngle, Vector2.Zero, Color.White);

            //Draw Planet
            spriteBatch.Draw(planet, new Vector2(originX, originY), Color.White);

            //Draw Player
            spriteBatch.Draw(player, playerPosition, Color.White);

            //Draw Enemy
            spriteBatch.Draw(enemy, new Vector2((float)(Math.Cos(playerAngle)*radius)+originX+(87/2), (float)(Math.Sin(playerAngle)*radius)+originY+(87/2)), Color.White);

            //Poop
            spriteBatch.DrawString(font, "<", new Vector2(originX+(87/2), originY+(87/2)), Color.White);

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
