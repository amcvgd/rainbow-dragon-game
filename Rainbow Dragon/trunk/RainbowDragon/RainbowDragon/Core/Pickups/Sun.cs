using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RainbowDragon.Core.Sprites;

namespace RainbowDragon.Core.Pickups
{
    class Sun : MovingSprite
    {
        public string type { get; set; }
        public bool isInvisible = false;
        public Sun(Texture2D texture, Vector2 position, int size, string type)
            : base(texture, position, size)
        {
            this.type = type;

        }



        public Sun()
        {

            

        }
        protected int size;
        //protected Texture2D texture;
        //protected Vector2 position;
        public int Size { get { return size; } set { size = value; } }


        public override void Update(GameTime gameTime)
        {
            
            //base.Update(gameTime);
            if (isInvisible)
            {
                alpha += (float)1 / (100 * size); //TODO: change logic
                if (alpha >= 1)
                    isInvisible = false;
            }
            else
            {
                rotation += MathHelper.ToRadians(1);
            }
        }

       
        public void DisappearSun()
        {

            alpha = 0;
            isInvisible = true;
        }

    }
}
