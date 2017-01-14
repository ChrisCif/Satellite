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
    class MyOb
    {
        protected int hp;
        protected Vector2 center;
        protected bool isAlive;
        protected String spriteFile;

        //Constructors
        public MyOb()
        {
            this.hp = 1;
            this.center = Vector2.Zero;
            this.isAlive = true;
            this.spriteFile = "fireYO";
        }
        public MyOb(int hitpoints, Vector2 centerPos)
        {
            if (hitpoints > 0)
            {
                this.hp = hitpoints;
            }

            this.center = centerPos;

            this.isAlive = true;
        }


        public void takeHit()
        {
            if (hp > 0)
            {
                hp--;
            }
        }

        public void die()
        {
            //J-J-J-Just in case WUBWUBWUBWUBWUB
            if (hp <= 0)
            {
                isAlive = false;
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

        //GETTERS
        public int getHP()
        {
            return hp;
        }
        public Vector2 getCenter()
        {
            return center;
        }
        public bool getIsAlive()
        {
            return isAlive;
        }
        public String getSpriteName()
        {
            return spriteFile;
        }

    }
}
