using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RainbowDragon.Core.Sprite
{
    class FollowingSprite:MovingSprite
    {
        MovingSprite father; //this is the sprite to be followed

        public FollowingSprite(MovingSprite father, Texture2D texture, Vector2 position, float speed = 0, float rotation = 0)
            :base(texture, position, speed, rotation)
        {
            this.father = father;
        }

    }
}
