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
    class MyOb
    {
        protected int hp;
        protected Vector2 center;
        protected String spriteFile;
        protected Vector2 coords;

        //Constructors
        public MyOb()
        {
            this.hp = 1;
            this.center = Vector2.Zero;
            this.spriteFile = "fireYO";
            this.coords = Vector2.Zero;
        }
        public MyOb(int hitpoints, Vector2 spot, Vector2 centerPos)
        {
            if (hitpoints > 0)
            {
                this.hp = hitpoints;
            }

            this.center = centerPos;

            this.coords = spot;
        }


        public void takeHit()
        {
            if (hp > 0)
            {
                hp--;
            }
        }

        //SETTERS -- Note: Cannot revive the object, for what holds equal worth to a human life
        public bool setHP(int newHP)
        {
            if (newHP < 0)
            {
                return false;
            }

            hp = newHP;
            return true;
        }
        public bool setCenter(Vector2 newCenter)
        {
            if (newCenter == null)
            {
                return false;
            }

            center = newCenter;
            return true;
        }
        public bool setSprite(String newFile)
        {
            if (newFile == null)
            {
                return false;
            }

            spriteFile = newFile;
            return true;
        }
        public bool setCoords(Vector2 newCoords)
        {
            coords = newCoords;
            return true;
        }

        //GETTERS
        public int getHP()
        {
            return hp;
        }
        public Vector2 getCenter()
        {
            return center;
        }
        public String getSpriteName()
        {
            return spriteFile;
        }
        public Vector2 getCoords()
        {
            return coords;
        }

    }
}
