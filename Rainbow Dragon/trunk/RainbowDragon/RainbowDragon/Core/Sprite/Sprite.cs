using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace RainbowDragon.Core.Sprite
{
    class Sprite
    {


        protected Texture2D texture;
        protected Vector2 position;

        public Sprite()
        {

        }


        //I am making this method virtual because depending on the type of sprite, you might need to do more complicated things
        //like rotation or scaling.
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);

        }

        //Update method is not included because at its core a sprite shouldn't need update. Other types of sprites, like the ones that move,
        //will have their own update method.



    }
}
