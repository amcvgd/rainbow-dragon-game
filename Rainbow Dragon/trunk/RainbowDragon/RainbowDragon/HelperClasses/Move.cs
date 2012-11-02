using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RainbowDragon.HelperClasses
{
    class Move
    {
        public float speed;
        public float rotation;
        public Move next;

        public Move(float speed, float rotation)
        {
            this.speed = speed;
            this.rotation = rotation;
            next = null;
        }
    }
}
