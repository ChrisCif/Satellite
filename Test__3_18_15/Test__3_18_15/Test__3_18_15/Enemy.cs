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
    class Enemy : NPO
    {

        protected double advanceRad;
        protected double speed;

        //Constructors
        public Enemy()
        {
            this.hp = 1;
            this.center = Vector2.Zero;
            advanceRad = 250;
            speed = 1;
            this.angle = MathHelper.PiOver2;
            this.coords = Vector2.Zero;
        }
        public Enemy(int hitpoints, Vector2 spot, Vector2 centerPos, double newAngle, double adv)
        {
            if (hitpoints > 0)
            {
                this.hp = hitpoints;
            }

            this.center = centerPos;

            advanceRad = 400;

            if ((newAngle >= 0) && (newAngle <= 2 * MathHelper.Pi))
            {
                angle = newAngle;
            }

            this.coords = spot;

            speed = adv;
        }

        public void advance()
        {
            if (advanceRad > 0)
            {
                advanceRad -= speed;
            }
        }

        //GETTERS
        public double getRad()
        {
            return advanceRad;
        }
        public double getSpeed()
        {
            return speed;
        }

        //SETTERS
        public bool setRad(int newRad)
        {
            if (newRad < 0)
            {
                return false;
            }

            advanceRad = newRad;
            return true;
        }
        public bool setSpeed(double newSpeed)
        {
            speed = newSpeed;
            return true;
        }
    }
}
