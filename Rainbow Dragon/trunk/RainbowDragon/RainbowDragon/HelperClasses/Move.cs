using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace RainbowDragon.HelperClasses
{
    class Move
    {
        public float speed;
        public float rotation;
        public Vector2 position;
        public Move next;

        public Move(float speed, float rotation, Vector2 position)
        {
            this.speed = speed;
            this.rotation = rotation;
            this.position = position;
            next = null;
        }
    }
}
