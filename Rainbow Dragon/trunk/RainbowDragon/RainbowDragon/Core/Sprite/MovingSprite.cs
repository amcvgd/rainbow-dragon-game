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
        public float speed {get; set;}
        public float rotation{ get; set; }

        public Rectangle Hitbox
        {
            get { return new Rectangle((int)position.X, (int)position.Y, size.Width, size.Height); }
        }

        public  MovingSprite(Texture2D texture, Vector2 position, float scale = 1, float speed = 0, float rotation = 0)
            :base(texture, position, scale)
        {
            this.speed = speed;
            this.rotation = rotation;
        }

        public virtual void Update(GameTime gameTime)
        {

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, new Rectangle((int)position.X, (int)position.Y, size.Width, size.Height),
                new Rectangle(0, 0, texture.Width, texture.Height), Color.White, rotation,
                new Vector2(texture.Width / 2, texture.Height / 2), SpriteEffects.None, 0);
        }
        
        public bool CheckCollision()
        {
            return false;
        }


    }
}
