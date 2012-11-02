using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RainbowDragon.Core.Sprite
{
    class MovingSprite:Sprite
    {
        protected float speed;
        protected float rotation;


        public  MovingSprite(Texture2D texture, Vector2 position, float speed = 0, float rotation = 0)
            :base(texture, position)
        {
            this.speed = speed;
            this.rotation = rotation;
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
