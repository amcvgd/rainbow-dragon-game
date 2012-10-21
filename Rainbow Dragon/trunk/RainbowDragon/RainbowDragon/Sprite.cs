using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace RainbowDragon
{
    class Sprite
    {
        protected Vector2 position = new Vector2(300, 150);     //The position of the sprite
        protected int width, height;                            //The width and height of the sprite

        protected Texture2D texture;                            //The texture for the sprite

        float scale;                                            //The scale for the sprite

        public float Scale
        {
            get { return scale; }
            set //Whenever we change the scale, we have to re-calculate the width and height
            {
                scale = value;
                width = (int)(texture.Width * scale);
                height = (int)(texture.Height * scale);
            }
        }

        //Takes in a ContentManager, the location of the texture, and an optional scale parameter
        public Sprite(ContentManager theContent, string location, float scale = 1)
        {
            texture = theContent.Load<Texture2D>(location);
            this.scale = scale;
            width = (int)(texture.Width * scale);
            height = (int)(texture.Height * scale);
        }

        //Any children can override the Update; Feel free to change
        public virtual void Update(GameTime gameTime)
        {

        }

        //Any children can override the Draw; Feel free to change
        public virtual void Draw(SpriteBatch spriteBatch)
        {

        }
    }
}
