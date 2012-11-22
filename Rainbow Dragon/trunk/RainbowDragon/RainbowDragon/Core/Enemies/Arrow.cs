using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using RainbowDragon.Core.Sprites;

namespace RainbowDragon.Core.Enemies
{
    class Arrow:MovingSprite
    {
        protected float direction; //in degrees from 0 to 360
        //public Vector2 position { get; set; }
        public Vector2 initialPosition { get; set; }

        public float Direction { get { return direction; } set { rotation = MathHelper.ToRadians(value); direction = value; } }


        public Arrow()
        {
            speed = 5;
        }

        public override void Update(GameTime gameTime)
        {

            position.X = position.X + speed * (float)Math.Cos(rotation);
            position.Y = position.Y + speed * (float)Math.Sin(rotation);
            //base.Update(gameTime);
        }

        public void RestartPosition()
        {
            position.X = initialPosition.X;
            position.Y = initialPosition.Y;
        }

    }
}
