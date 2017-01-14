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

namespace Test__3_18_15
{
   
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        //INSTANCES
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        //CONTENT
        Texture2D satelliteSprite;
        Texture2D planetSprite;
        Texture2D enemySprite;
        Texture2D bulletSprite;
        Texture2D failLogo;
        Texture2D back;
        Texture2D ui;
        Texture2D pause;
        Texture2D fail;
        Texture2D star;
        Texture2D logo;
        SoundEffect failSound;
        SpriteFont font;
        SpriteFont smallFont;

        //UTILITIES
        List<NPO> liveObs;
        List<List<Enemy>> army;
        int updateCount;
        int lastShot;
        bool isPlaying;
        bool hasBegun;

        Color randoColor;
        int randoAmount;
        Vector2[] starPositions;

        const double THETA_ONE = (MathHelper.PiOver2) + (MathHelper.Pi / 8);
        const double THETA_TWO = (MathHelper.PiOver2) - (MathHelper.Pi / 8);
        const double BULLET_THETA = MathHelper.PiOver4;
        Color debugColor;
        const double BASIC_ENEMY_SPEED = 1.50;
        Random rando = new Random();
        int stage;
        int frFactor;
        bool pauseA = false;
        bool healA = false;

        //PLAYER & PLANET
        Satellite satellite;
        const int satelliteRad = 75;
        Earth planet;
        const int planetRad = 90;
        int combo;
        int pushCombo;
        bool canPush;
        int pushCheck;
        Color pushColor;
        double paralaxAngle;
        double paralaxChange;
        double lastParalax;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        
        protected override void Initialize()
        {
            LoadGame();
        }

        private void LoadGame()
        {
            //SPRITES
            satelliteSprite = Content.Load<Texture2D>("satellite");
            planetSprite = Content.Load<Texture2D>("planet");
            enemySprite = Content.Load<Texture2D>("enemy");
            bulletSprite = Content.Load<Texture2D>("bullet");

            randoColor = new Color(rando.Next(100, 255), rando.Next(100, 255), rando.Next(100, 255));
            randoAmount = rando.Next(50, 75);
            starPositions = new Vector2[randoAmount];
            for (int x = 0; x < randoAmount; x++)
            {
                starPositions[x] = new Vector2((float)rando.Next(0, graphics.PreferredBackBufferWidth), (float)rando.Next(0, graphics.PreferredBackBufferHeight));
            }

            liveObs = new List<NPO>();
            army = new List<List<Enemy>>();
            stage = 0;
            updateCount = 0;
            lastShot = -30;
            isPlaying = true;
            debugColor = Color.CornflowerBlue;
            frFactor = 0;

            satellite = new Satellite(15, new Vector2(graphics.PreferredBackBufferWidth / 2 - satelliteSprite.Width / 2, (graphics.PreferredBackBufferHeight / 2 - satelliteSprite.Height / 2) - planetRad), new Vector2(satelliteSprite.Width / 2, satelliteSprite.Height / 2), new Rectangle(graphics.PreferredBackBufferWidth / 2 - satelliteSprite.Width / 2, (graphics.PreferredBackBufferHeight / 2 - satelliteSprite.Height / 2) - satelliteRad, satelliteSprite.Width, satelliteSprite.Height));
            liveObs.Add(planet = new Earth(5, new Vector2((graphics.PreferredBackBufferWidth / 2) - (planetSprite.Width / 2), (graphics.PreferredBackBufferHeight / 2) - (planetSprite.Height / 2)), new Vector2(planetSprite.Width / 2, planetSprite.Height / 2), MathHelper.PiOver2));

            combo = 0;
            pushCombo = 0;
            canPush = false;
            pushCheck = 0;
            pushColor = Color.Blue;
            paralaxAngle = 0;
            paralaxChange = 0;
            lastParalax = 0;

            //ADDING A TEST ENEMY
            //liveObs.Add( new Enemy(1, new Vector2( (float)(-Math.Cos(MathHelper.Pi)*500), (float)(-Math.Sin(MathHelper.Pi)*500) ), new Vector2(enemySprite.Width/2, enemySprite.Height/2), MathHelper.Pi, 4) );


            //POPULATING THE ARMY
            /*
            int armyQuantity = 5;
            for (int x = 0; x < army.Count(); x++)
            {
                army[x] = new List<Enemy>();

                armyQuantity += 2 * x;

                for (int y = 0; y < armyQuantity; y++)
                {
                    //Enemy(int hitpoints, Vector2 spot, Vector2 centerPos, double newAngle, double adv)
                    double datAngle = (2*MathHelper.Pi) * (rando.NextDouble());
                    //double datSpeed = rando.NextDouble() + (double)rando.Next(x / 12, 4);
                    army[x].Add(new Enemy(1, new Vector2((float)(-Math.Cos(datAngle) * 500), (float)(-Math.Sin(datAngle) * 500)), new Vector2(enemySprite.Width / 2, enemySprite.Height / 2), datAngle, 1));
                }
            }
            */

            army.Add(new List<Enemy>());
            for (int x = 0; x < 5/*+(stage*2)*/; x++)
            {
                double datAngle = (2 * MathHelper.Pi) * (rando.NextDouble());
                army.ElementAt(stage).Add(new Enemy(1, new Vector2((float)(-Math.Cos(datAngle) * 500), (float)(-Math.Sin(datAngle) * 500)), new Vector2(enemySprite.Width / 2, enemySprite.Height / 2), datAngle, 1));
            }

            base.Initialize();
        }

        
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            failLogo = Content.Load<Texture2D>("FailurePic");
            failSound = Content.Load<SoundEffect>("failure");
            font = Content.Load<SpriteFont>("newFont");
            smallFont = Content.Load<SpriteFont>("font");
            back = Content.Load<Texture2D>("background");
            ui = Content.Load<Texture2D>("UI");
            pause = Content.Load<Texture2D>("pause");
            fail = Content.Load<Texture2D>("gameOver");
            star = Content.Load<Texture2D>("star");
            logo = Content.Load<Texture2D>("satelliteLogo");
        }

        protected override void UnloadContent()
        {

        }


        protected override void Update(GameTime gameTime)
        {
            if ((GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed) || (Keyboard.GetState().IsKeyDown(Keys.Escape)))
                this.Exit();

            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                hasBegun = true;
            }

            if (hasBegun)
            {

                if (satellite.getHP() > 0 && planet.getHP() > 0)
                {

                    if (army.ElementAt(stage).Count() <= 0)
                    {
                        army.Add(new List<Enemy>());

                        stage++;

                        for (int x = 0; x < 5 + (stage * 2); x++)
                        {
                            double datAngle = (2 * MathHelper.Pi) * (rando.NextDouble());
                            army.ElementAt(stage).Add(new Enemy(1, new Vector2((float)(-Math.Cos(datAngle) * 500), (float)(-Math.Sin(datAngle) * 500)), new Vector2(enemySprite.Width / 2, enemySprite.Height / 2), datAngle, 1));
                        }
                    }

                    lastParalax = paralaxAngle;

                    //PAUSING
                    if (Keyboard.GetState().IsKeyDown(Keys.End))
                    {
                        pauseA = true;
                    }
                    if (pauseA && !(Keyboard.GetState().IsKeyDown(Keys.End)))
                    {
                        isPlaying = !isPlaying;
                        pauseA = false;
                    }

                    if (isPlaying)
                    {
                        updateCount++;

                        if (combo < 5)
                        {
                            frFactor = 2 * combo;
                        }
                        else
                        {
                            frFactor = 10;
                        }

                        //SPAWNING ENEMIES
                        if (updateCount > 0 && updateCount % 90 == 0)
                        {
                            liveObs.Add(army.ElementAt(stage).ElementAt(0));
                            army.ElementAt(stage).Remove(army.ElementAt(stage).ElementAt(0));
                        }

                        //PUSHING 2: PUSHING HARDER
                        if (Math.Abs(updateCount - pushCheck) >= 45 && pushCheck > 0)
                        {
                            foreach (NPO npo in liveObs)
                            {
                                if (npo is Enemy)
                                {
                                    Enemy e = (Enemy)npo;
                                    if (e.getSpeed() < 0)
                                    {
                                        //e.setSpeed(-(Math.Abs(e.getSpeed())) * (pushCombo / 5))
                                        e.setSpeed(Math.Abs(e.getSpeed()) / (pushCombo / 5));
                                    }
                                }
                            }
                            pushCombo = 0;
                        }

                        /*ALLOW PUSH AND RESET PUSH
                        if (updateCount - pushCheck >= 30)
                        {
                            if (combo > 0 && combo % 5 == 0)
                            {
                                canPush = true;
                            }

                            foreach(NPO npo in liveObs)
                            {
                                if (npo is Enemy)
                                {
                                    Enemy e = (Enemy)npo;
                                    e.setSpeed(BASIC_ENEMY_SPEED);
                                }
                            }
                        }*/

                        //TURNING
                        if (Keyboard.GetState().IsKeyDown(Keys.Left))
                        {
                            for (int x = 0; x < liveObs.Count; x++)
                            {
                                NPO thing = liveObs.ElementAt(x);
                                thing.setAngle(thing.getAngle() + MathHelper.Pi / 52);
                            }
                            paralaxAngle += MathHelper.Pi / 175;
                        }
                        if (Keyboard.GetState().IsKeyDown(Keys.Right))
                        {
                            for (int x = 0; x < liveObs.Count; x++)
                            {
                                NPO thing = liveObs.ElementAt(x);
                                thing.setAngle(thing.getAngle() - MathHelper.Pi / 52);
                            }
                            paralaxAngle -= MathHelper.Pi / 175;
                        }


                        //SHOOTING
                        if (Keyboard.GetState().IsKeyDown(Keys.Up))
                        {
                            if (updateCount - lastShot >= (30 - frFactor))
                            {
                                liveObs.Add(
                                    new Projectile(1,
                                        new Vector2(
                                                (float)(Math.Cos(MathHelper.PiOver2) + graphics.PreferredBackBufferWidth / 2), (float)(Math.Sin(MathHelper.PiOver2 * (bulletSprite.Height + satelliteRad + satelliteSprite.Height)))
                                        ), new Vector2(bulletSprite.Width / 2, bulletSprite.Height / 2), MathHelper.PiOver2, 3 + combo * .25, (satelliteRad + bulletSprite.Height + satelliteSprite.Height)
                                    )
                                );
                                lastShot = updateCount;
                            }
                        }


                        //ENEMY AND BULLET ACTION
                        for (int x = 0; x < liveObs.Count; x++)
                        {
                            NPO myThing = liveObs.ElementAt(x);
                            if (myThing is Projectile)
                            {
                                Projectile p = (Projectile)myThing;
                                double pAngle = p.getAngle();

                                for (int y = 0; y < liveObs.Count; y++)
                                {
                                    if (liveObs.ElementAt(y) is Enemy)
                                    {
                                        Enemy e = (Enemy)liveObs.ElementAt(y);
                                        double bulletRad = p.getRad() + p.getCenter().Y;
                                        double enemyRad = e.getRad() - e.getCenter().Y;
                                        if (Math.Abs(pAngle - e.getAngle()) < MathHelper.Pi / 24 && bulletRad > enemyRad)
                                        {
                                            liveObs.Remove(p);
                                            liveObs.Remove(e);
                                            combo++;

                                            if (updateCount - pushCheck >= 30)
                                            {
                                                if (combo % 5 == 0)
                                                {
                                                    canPush = true;
                                                }

                                                foreach (NPO npo in liveObs)
                                                {
                                                    if (npo is Enemy)
                                                    {
                                                        Enemy en = (Enemy)npo;
                                                        en.setSpeed(BASIC_ENEMY_SPEED);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }

                                p.advance();
                                p.setCoords(new Vector2((float)(-Math.Cos(p.getAngle()) * p.getRad()) + graphics.PreferredBackBufferWidth / 2, (float)(-Math.Sin(p.getAngle()) * p.getRad()) + graphics.PreferredBackBufferHeight / 2));

                                if (p.getRad() >= 500)
                                {
                                    liveObs.Remove(p);
                                }

                            }

                            if (myThing is Enemy)
                            {
                                Enemy e = (Enemy)myThing;

                                if (e.getAngle() <= THETA_ONE && e.getAngle() >= THETA_TWO)
                                {
                                    Rectangle eRect = new Rectangle((int)(e.getCoords().X - e.getCenter().X), (int)(e.getCoords().Y - e.getCenter().Y), enemySprite.Width, enemySprite.Height);

                                    if (eRect.Intersects(satellite.getBox()))
                                    {
                                        liveObs.Remove(e);
                                        satellite.takeHit();
                                        combo = 0;
                                        canPush = false;
                                    }
                                }

                                if (e.getRad() - e.getCenter().Y <= planet.getCenter().X)
                                {
                                    liveObs.Remove(e);
                                    planet.takeHit();
                                    combo = 0;
                                    canPush = false;
                                }

                                e.advance();

                                e.setCoords(new Vector2((float)(-Math.Cos(e.getAngle()) * e.getRad()) + graphics.PreferredBackBufferWidth / 2, (float)(-Math.Sin(e.getAngle()) * e.getRad()) + graphics.PreferredBackBufferHeight / 2));
                            }
                        }

                        //PUSHING
                        if (Keyboard.GetState().IsKeyDown(Keys.Space))
                        {
                            if (canPush)
                            {
                                pushCheck = updateCount;
                                pushCombo = combo;

                                foreach (NPO npo in liveObs)
                                {
                                    if (npo is Enemy)
                                    {
                                        Enemy e = (Enemy)npo;
                                        e.setSpeed(-(Math.Abs(e.getSpeed())) * (pushCombo / 5));
                                    }
                                }

                                canPush = false;
                            }
                        }

                        //HEALTH REDISTRIBUTION
                        if (Keyboard.GetState().IsKeyDown(Keys.Down) && satellite.getHP() > 1)
                        {
                            healA = true;
                        }
                        if (healA && !(Keyboard.GetState().IsKeyDown(Keys.Down)))
                        {
                            satellite.setHP(satellite.getHP() - 1);
                            planet.setHP(planet.getHP() + 1);
                            healA = false;
                        }

                        //PUSH COLOR
                        if (canPush)
                        {
                            pushColor = Color.Yellow;
                        }
                        else
                        {
                            if (planet.getHP() < 3)
                            {
                                pushColor = Color.Red;
                            }
                            else if (satellite.getHP() < 5)
                            {
                                pushColor = Color.Red;
                            }
                            else
                            {
                                pushColor = Color.Blue;
                            }
                        }

                        paralaxChange = paralaxAngle - lastParalax;

                    }
                }

                else
                {
                    if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                    {
                        LoadGame();
                    }
                }
            }
            
            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();

            if (hasBegun)
            {
                //BACKGROUND
                spriteBatch.Draw(back, new Vector2((graphics.PreferredBackBufferWidth / 2), (graphics.PreferredBackBufferHeight / 2)), null, Color.White, (float)paralaxAngle, new Vector2(back.Width / 2, back.Height / 2), 1f, SpriteEffects.None, 0f);

                //DRAW PLANET
                spriteBatch.Draw(planetSprite, new Vector2(graphics.PreferredBackBufferWidth / 2, graphics.PreferredBackBufferHeight / 2), null, Color.White, (float)planet.getAngle(), planet.getCenter(), 1f, SpriteEffects.None, 0f);
                //spriteBatch.DrawString(font, "Planet Angle: " + planet.getAngle(), Vector2.Zero, Color.White);
                //spriteBatch.DrawString(font, "Planet radius: " + planetRad, new Vector2(0, 100), Color.White);
                //spriteBatch.DrawString(font, "" + combo, planet.getCoords(), Color.Yellow);

                //DRAW SATELLITE
                spriteBatch.Draw(satelliteSprite, new Vector2(((graphics.PreferredBackBufferWidth / 2) - satellite.getCenter().X), ((graphics.PreferredBackBufferHeight / 2) - satellite.getCenter().Y) - satelliteRad), Color.White);
                //spriteBatch.DrawString(font, "Satellite Coords: " + satellite.getCoords(), new Vector2(0, 125), Color.White);

                //DRAW ENEMIES AND BULLETS
                foreach (NPO npo in liveObs)
                {
                    if (npo is Enemy)
                    {
                        Enemy e = (Enemy)npo;
                        spriteBatch.Draw(enemySprite, e.getCoords(), null, Color.White, (float)e.getAngle(), e.getCenter(), 1f, SpriteEffects.None, 0);
                        /*spriteBatch.DrawString(font, "Enemy Radius: " + e.getRad(), new Vector2(0, 25), Color.White);
                        spriteBatch.DrawString(font, "Enemy Coords: ("+e.getCoords().X+","+e.getCoords().Y+")", new Vector2(0, 50), Color.White);
                        //spriteBatch.DrawString(font, "Enemy Center: (" + e.getCenter().X + "," + e.getCenter().Y + ")", new Vector2(0, 75), Color.White);
                        spriteBatch.DrawString(font, "Enemy  Angle: " + e.getAngle(), new Vector2(0, 75), Color.White);
                         */
                    }
                    else if (npo is Projectile)
                    {
                        Projectile p = (Projectile)npo;
                        spriteBatch.Draw(bulletSprite, p.getCoords(), null, Color.White, (float)p.getAngle() - (float)MathHelper.PiOver2, p.getCenter(), 1f, SpriteEffects.None, 0);
                    }
                }
                /*
                spriteBatch.DrawString(smallFont, "Live Obs Count: " + liveObs.Count, new Vector2(0, 100), Color.White);

                spriteBatch.DrawString(smallFont, "Push Combo: " + pushCombo, new Vector2(0, 150), Color.White);
                spriteBatch.DrawString(smallFont, "Can Push: " + canPush, new Vector2(0, 175), Color.White);
                spriteBatch.DrawString(smallFont, "Push Check: " + pushCheck, new Vector2(0, 200), Color.White);

                spriteBatch.DrawString(smallFont, "Stage " + stage, new Vector2(graphics.PreferredBackBufferWidth - 100, 0), Color.White);
                spriteBatch.DrawString(smallFont, army[stage].Count + " Left", new Vector2(graphics.PreferredBackBufferWidth - 100, 25), Color.White);
                */

                if (!isPlaying)
                {
                    spriteBatch.Draw(pause, Vector2.Zero, Color.White);
                }

                spriteBatch.Draw(ui, Vector2.Zero, pushColor);
                //DRAW FONT
                spriteBatch.DrawString(font, "" + combo, new Vector2(50, 25), Color.White);
                spriteBatch.DrawString(smallFont, "Ship HP: " + satellite.getHP(), new Vector2(12, graphics.PreferredBackBufferHeight - 70), Color.White);
                spriteBatch.DrawString(smallFont, "Earth HP: " + planet.getHP(), new Vector2(11, graphics.PreferredBackBufferHeight - 35), Color.White);
                spriteBatch.DrawString(smallFont, "Wave: ", new Vector2(graphics.PreferredBackBufferWidth - 125, 25), Color.White);
                spriteBatch.DrawString(font, "" + (stage + 1), new Vector2(graphics.PreferredBackBufferWidth - 60, 25), Color.White);
                if (canPush)
                {
                    spriteBatch.DrawString(smallFont, "Push Ready [SPACE]", new Vector2((graphics.PreferredBackBufferWidth / 2) - 100, 7), Color.White);
                }

                if (satellite.getHP() < 1 || planet.getHP() < 1)
                {
                    spriteBatch.Draw(fail, Vector2.Zero, Color.White);
                }
            }

            else
            {
                for (int x = 0; x < randoAmount; x++)
                {
                    spriteBatch.Draw(star, starPositions[x], randoColor);
                }
                spriteBatch.Draw(logo, new Vector2(0, 0), Color.White);
                spriteBatch.DrawString(smallFont, "Press [ENTER]", new Vector2(350, 300), Color.White);
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
