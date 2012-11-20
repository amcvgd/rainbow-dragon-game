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
        protected int direction; //in degrees from 0 to 360
        //public Vector2 position { get; set; }

        public int Direction { get { return direction; } set { direction = value; } }


        public Arrow()
        {

        }

    }
}
