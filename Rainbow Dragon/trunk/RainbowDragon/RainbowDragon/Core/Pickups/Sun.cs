using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RainbowDragon.Core.Sprite;

namespace RainbowDragon.Core.Pickups
{
    class Sun : MovingSprite
    {
        public string type { get; set; }

        public Sun(Texture2D texture, Vector2 position, int size, string type)
            : base(texture, position, size)
        {
            this.type = type;
        }


    }
}
