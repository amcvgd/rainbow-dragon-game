using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RainbowDragon.Core.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RainbowDragon.Core.Levels
{
    class Circle:Sprite
    {
        Rectangle destRect;
        public Rectangle DestRectangle { get { return destRect; } set { destRect = value; } }

        public Circle()
        {
            

        }

        public void Initialize(Texture2D texture,  Rectangle rect)
        {
            this.texture = texture;
            //this.position = position;
            destRect = rect;
        }

        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(texture, destRect, Color.White);
            //base.Draw(spriteBatch);
        }

        public void UpdateRectangle(Rectangle newRect)
        {
            destRect = newRect; 

        }

    }
}
