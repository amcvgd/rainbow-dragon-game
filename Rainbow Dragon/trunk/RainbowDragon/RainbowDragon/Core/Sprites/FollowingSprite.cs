using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using RainbowDragon.HelperClasses;

namespace RainbowDragon.Core.Sprites
{
    class FollowingSprite:MovingSprite
    {
        public MoveQueue moves;
        protected FollowingSprite father;

        public FollowingSprite(FollowingSprite father, Texture2D texture, Vector2 position, float scale = 1, float speed = 0, 
            float rotation = 0):base(texture, position, scale, speed, rotation)
        {
            this.father = father;
            moves = new MoveQueue(2);
        }

        public override void Update(GameTime gameTime)
        {
            Move nextMove = father.moves.GetNextMove();

            if (nextMove != null)
            {
                rotation = nextMove.rotation;
                speed = nextMove.speed;
                position = nextMove.position;

                //Update Move List
                moves.Push(new Move(speed, rotation, Position));
            }
        }

        public void SetFather(FollowingSprite father)
        {
            this.father = father;
        }

    }
}
