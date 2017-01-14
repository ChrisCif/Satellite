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

namespace Test__2_20_15
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        bool isPlaying;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont font;
        const int playerRadius = 75;
        const int earthRadius = 90;
        double playerAngle;
        List<MyOb> liveObs;
        Satellite player;
        Texture2D satelliteSprite;
        //Texture2D satelliteBox;
        //Rectangle sBoxHelper;
        Earth earth;
        Texture2D earthSprite;
        Enemy enemy;
        Texture2D enemySprite;
        //Texture2D enemyBox;
        //Rectangle eBoxHelper;
        const double THETA_ONE = 1.768;
        const double THETA_TWO = 1.373;
        SoundEffect failSound;
        Texture2D failure;

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
            isPlaying = true;

            playerAngle = MathHelper.Pi / 2;

            satelliteSprite = Content.Load<Texture2D>("TESTSatellite");
            earthSprite = Content.Load<Texture2D>("TESTPlanet");
            enemySprite = Content.Load<Texture2D>("TESTEnemy");

            player = new Satellite(5, new Vector2(satelliteSprite.Width / 2, satelliteSprite.Height / 2));

            //Don't need to initialize these objects in the future
            liveObs = new List<MyOb>();

            earth = new Earth(10, new Vector2(earthSprite.Width / 2, earthSprite.Height / 2));
            liveObs.Add(earth);

            enemy = new Enemy(1, new Vector2(enemySprite.Width / 2, enemySprite.Height / 2), MathHelper.PiOver2, new Rectangle( ((graphics.PreferredBackBufferWidth / 2)) + (int)(-Math.Cos(playerAngle) * 250), (int)((graphics.PreferredBackBufferHeight / 2)) + ((int)-Math.Sin(playerAngle) * 250), enemySprite.Width, enemySprite.Height));

            liveObs.Add(enemy);

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

            font = Content.Load<SpriteFont>("font");

            failSound = Content.Load<SoundEffect>("failure");
            failure = Content.Load<Texture2D>("FailurePic");

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

            isPlaying = earth.getIsAlive();

            

            if (isPlaying)
            {
                //TURNING
                if (Keyboard.GetState().IsKeyDown(Keys.Left))
                {
                    playerAngle += MathHelper.Pi / 32;

                    for (int x = 0; x < liveObs.Count; x++)
                    {
                        if (liveObs.ElementAt(x) is NPO)
                        {
                            NPO ob = (NPO)liveObs.ElementAt(x);

                            if (ob is Enemy)
                            {
                                Enemy e = (Enemy)ob;
                                e.setAngle(ob.getAngle() + MathHelper.Pi / 32);
                            }
                        }
                    }

                }
                else if (Keyboard.GetState().IsKeyDown(Keys.Right))
                {
                    playerAngle -= MathHelper.Pi / 32;

                    for (int x = 0; x < liveObs.Count; x++)
                    {
                        if (liveObs.ElementAt(x) is NPO)
                        {
                            NPO ob = (NPO)liveObs.ElementAt(x);
                            ob.setAngle(ob.getAngle() - MathHelper.Pi / 32);
                        }
                    }
                }

                //ENEMY ACTIONS
                for (int x = 0; x < liveObs.Count; x++)
                {
                    if (liveObs.ElementAt(x) is Enemy)
                    {
                        Enemy e = (Enemy)liveObs.ElementAt(x);

                        //HITBOX MOVING
                        e.setHitPos((graphics.PreferredBackBufferWidth / 2) + (int)(-Math.Cos(playerAngle) * e.getRad()), (graphics.PreferredBackBufferHeight / 2) + (int)(-Math.Sin(playerAngle) * e.getRad()));
                        

                        //ENEMIES HITTING
                        if (e.getRad() - e.getCenter().Y == earth.getCenter().X)
                        {
                            earth.takeHit();

                            /*Will actually do the following once there is a set list of enemies
                             * liveObs.Remove(e);
                             */

                            e.setRad(250);

                        }
                        else
                        {
                            //ENEMIES ADVANCING
                            e.advance();
                        }

                        //SATELLITE HIT
                        if ( /*((e.getAngle() - THETA_ONE) / (2*MathHelper.Pi) >= 1  
                            || (e.getAngle() - THETA_TWO) % (2*MathHelper.Pi) == 0)
                            && e.getRad() == playerRadius*/
                            (
                            ( (e.getAngle() - THETA_ONE) / (2*MathHelper.Pi) >= 1 && (e.getAngle() - THETA_ONE) / (2*MathHelper.Pi) <= 1.0000001 )
                            ||
                            ((e.getAngle() - THETA_TWO) / (2 * MathHelper.Pi) >= 1 && (e.getAngle() - THETA_TWO) / (2 * MathHelper.Pi) <= (-Math.Pow(10, -30)))
                            )
                            && e.getRad() == playerRadius
                            )
                        {
                            e.setRad(250);
                            e.setHitPos((graphics.PreferredBackBufferWidth / 2) + (int)(-Math.Cos(playerAngle) * e.getRad()), (graphics.PreferredBackBufferHeight / 2) + (int)(-Math.Sin(playerAngle) * e.getRad()));
                            player.takeHit();
                            //e.die();
                            //liveObs.Remove(e);
                        }
                    }
                }

                //EARTH DESTROYED
                if (earth.getHP() <= 0)
                {
                    earth.die();
                    liveObs.Remove(earth);
                    failSound.Play();
                }
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

            if (isPlaying)
            {
                //DRAW LIVEOBS
                foreach (MyOb thing in liveObs)
                {
                    if (thing is Enemy)
                    {
                        Enemy badguy = (Enemy)thing;
                        spriteBatch.Draw(enemySprite, new Vector2(((graphics.PreferredBackBufferWidth / 2)) + (float)(-Math.Cos(badguy.getAngle()) * badguy.getRad()), ((graphics.PreferredBackBufferHeight / 2)) + ((float)-Math.Sin(playerAngle) * badguy.getRad())), null, Color.White, (float)badguy.getAngle(), badguy.getCenter(), 1f, SpriteEffects.None, 0f);
                        spriteBatch.DrawString(font, "Enemy Position: (" + ((graphics.PreferredBackBufferWidth / 2)) + (float)(-Math.Cos(badguy.getAngle()) * badguy.getRad()) + "," + ((graphics.PreferredBackBufferHeight / 2)) + ((float)-Math.Sin(playerAngle) * badguy.getRad()) + ")", new Vector2(0, 50), Color.White);
                        spriteBatch.DrawString( font, "Enemy Angle: " + badguy.getAngle(), new Vector2(0, 75), Color.White );
                    }
                    else if (thing is Earth)
                    {
                        spriteBatch.Draw(earthSprite, new Vector2(graphics.PreferredBackBufferWidth / 2, graphics.PreferredBackBufferHeight / 2), null, Color.White, (float)playerAngle, earth.getCenter(), 1f, SpriteEffects.None, 0f);
                    }
                }

                //DRAW PLAYER
                spriteBatch.Draw(satelliteSprite, new Vector2(((graphics.PreferredBackBufferWidth / 2) - player.getCenter().X), ((graphics.PreferredBackBufferHeight / 2) - player.getCenter().Y) - playerRadius), Color.White);
                spriteBatch.DrawString(font, "Player Angle: " + playerAngle, new Vector2(0, 100), Color.White);

                //IN-GAME TEXT
                spriteBatch.DrawString(font, "Earth HP: " + earth.getHP(), Vector2.Zero, Color.White);
                spriteBatch.DrawString(font, "Satellite HP: " + player.getHP(), new Vector2(0, 25), Color.White);
            }
            else
            {
                GraphicsDevice.Clear(Color.White);
                spriteBatch.Draw(failure, new Vector2((graphics.PreferredBackBufferWidth / 2) - (failure.Width / 2), (graphics.PreferredBackBufferHeight / 2) - (failure.Height / 2)), Color.White);
                //spriteBatch.DrawString(font, "FAILURE", new Vector2(100, graphics.PreferredBackBufferHeight / 2), Color.White);
            }
            spriteBatch.End();
            
            base.Draw(gameTime);
        }
    }
}
