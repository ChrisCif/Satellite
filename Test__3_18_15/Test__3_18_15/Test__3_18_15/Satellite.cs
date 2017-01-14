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
    class Satellite : MyOb
    {
        protected Rectangle hitbox;

        //Constructors
        public Satellite()
        {
            this.hp = 1;
            this.center = Vector2.Zero;
            this.coords = Vector2.Zero;
            hitbox = new Rectangle();
        }
        public Satellite(int hitpoints, Vector2 spot, Vector2 centerPos, Rectangle box)
        {
            if (hitpoints > 0)
            {
                this.hp = hitpoints;
            }

            this.center = centerPos;

            this.coords = spot;

            hitbox = box;
        }

        //GETTERS
        public Rectangle getBox()
        {
            return hitbox;
        }
    }
}
