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
        public float speed;
        public float rotation;

        public Rectangle Hitbox
        {
            get { return new Rectangle((int)position.X, (int)position.Y, size.Width, size.Height); }
        }

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
