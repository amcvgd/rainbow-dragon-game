using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace RainbowDragon.Core.Enemies
{
    class Arrow
    {
        public String type { get; set; }
        public int direction { get; set; } //in degrees from 0 to 360
        public Vector2 position { get; set; }
    }
}
