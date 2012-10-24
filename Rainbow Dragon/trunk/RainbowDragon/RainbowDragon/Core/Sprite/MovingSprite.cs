using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace RainbowDragon.Core.Sprite
{
    class MovingSprite:Sprite
    {
        protected float speed;
        protected float acceleration;


        public  MovingSprite()
        {


        }

        public virtual void Update(GameTime gameTime)
        {

        }

        public bool CheckCollision()
        {
            return false;
        }


    }
}
