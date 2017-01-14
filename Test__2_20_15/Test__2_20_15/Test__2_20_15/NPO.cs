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
    class NPO : MyOb
    {
        protected bool shouldRotate;
        protected double angle;

        //Constructors
        public NPO()
        {
            this.hp = 1;
            this.center = Vector2.Zero;
            this.isAlive = true;
            this.shouldRotate = false;
            angle = MathHelper.PiOver2;
        }
        public NPO(int hitpoints, Vector2 centerPos, double newAngle)
        {
            if (hitpoints > 0)
            {
                this.hp = hitpoints;
            }

            this.center = centerPos;

            this.isAlive = true;

            if ((newAngle >= 0) && (newAngle <= 2 * MathHelper.Pi))
            {
                angle = newAngle;
            }
        }

        //SETTERS
        public bool setAngle(double newAngle)
        {
            /*if ((newAngle >= 0) && (newAngle <= 2 * MathHelper.Pi))
            {
                return true;
                angle = newAngle;
            }
            return false;*/
            angle = newAngle;
            return true;
        }

        //GETTERS
        public double getAngle()
        {
            return angle;
        }

        /*public void rotate(double playerAngle)
        {
            //Gonna rotate...eventually

        }*/

    }
}
