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
    class Satellite : MyOb
    {

        //Constructors
        public Satellite()
        {
            this.hp = 1;
            this.center = Vector2.Zero;
            this.isAlive = true;
        }
        public Satellite(int hitpoints, Vector2 centerPos)
        {
            if (hitpoints > 0)
            {
                this.hp = hitpoints;
            }

            this.center = centerPos;

            this.isAlive = true;
        }

        /*public bool fire(double playerAngle)
        {
            /*
             * Will fire a bullet depending on the player's angle
             * Returns whether or not the shot hits
             
        }*/

    }
}
