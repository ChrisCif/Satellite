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
    class Enemy : NPO
    {

        protected int advanceRad;
        protected Rectangle hitBox;

        //Constructors
        public Enemy()
        {
            this.hp = 1;
            this.center = Vector2.Zero;
            this.isAlive = true;
            advanceRad = 500;
            hitBox = new Rectangle(0, 0, 15, 15);
            this.angle = MathHelper.PiOver2;
        }
        public Enemy(int hitpoints, Vector2 centerPos, double newAngle, Rectangle box)
        {
            if (hitpoints > 0)
            {
                this.hp = hitpoints;
            }

            this.center = centerPos;

            this.isAlive = true;
            advanceRad = 250;
            hitBox = box;

            if ((newAngle >= 0) && (newAngle <= 2 * MathHelper.Pi))
            {
                angle = newAngle;
            }
        }

        public void advance()
        {
            if (advanceRad > 0)
            {
                advanceRad--;
            }
        }

        //GETTERS
        public int getRad()
        {
            return advanceRad;
        }
        public Rectangle getHitBox()
        {
            return hitBox;
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
        public bool setHitPos(int x, int y)
        {
            if (x > 0 && y > 0)
            {
                hitBox.X = x;
                hitBox.Y = y;
                return true;
            }
            return false;
        }
    }
}
