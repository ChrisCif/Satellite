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
    class Earth : NPO
    {
        //Constructors
        public Earth()
        {
            this.hp = 1;
            this.center = Vector2.Zero;
            this.coords = Vector2.Zero;
            this.angle = 0;
        }
        public Earth(int hitpoints, Vector2 spot,Vector2 centerPos, double newAngle)
        {
            if (hitpoints > 0)
            {
                this.hp = hitpoints;
            }

            this.center = centerPos;

            this.coords = spot;

            if ((newAngle >= 0) && (newAngle <= 2 * MathHelper.Pi))
            {
                angle = newAngle;
            }
        }

        /*public void rotate(double playerAngle)
        {
            //Will OVERRIDE the parent's method...eventually
        }*/

    }
}
