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
    class Projectile : NPO
    {
        protected double speed;
        protected double radius;

        //Constructors
        public Projectile()
        {
            this.hp = 1;
            this.center = Vector2.Zero;
            this.shouldRotate = false;
            this.coords = Vector2.Zero;
            this.angle = MathHelper.PiOver2;
            speed = 1;
            radius = 90;
        }
        public Projectile(int hitpoints, Vector2 spot, Vector2 centerPos, double newAngle, double mySpeed, double myRadius)
        {
            if (hitpoints > 0)
            {
                this.hp = hitpoints;
            }

            this.center = centerPos;

            this.coords = spot;

            if ((newAngle >= 0) && (newAngle <= 2 * MathHelper.Pi))
            {
                this.angle = newAngle;
            }

            if (mySpeed > 0)
            {
                speed = mySpeed;
            }

            if (myRadius > 0)
            {
                radius = myRadius;
            }
        }

        //Bullet will use this to advance "forward"
        public void advance()
        {
            radius += speed;
        }

        //SETTERS
        public bool setSpeed(double newSpeed)
        {
            if (newSpeed > 0)
            {
                speed = newSpeed;
                return true;
            }
            return false;
        }
        public bool setRadius(double newRad)
        {
            if (newRad > 0)
            {
                radius = newRad;
                return true;
            }
            return false;
        }

        //GETTERS
        public double getSpeed()
        {
            return speed;
        }
        public double getRad()
        {
            return radius;
        }

    }
}
