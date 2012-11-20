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
        protected MoveQueue moves;
        protected FollowingSprite father;

        public FollowingSprite(FollowingSprite father, Texture2D texture, Vector2 position, float scale = 1, float speed = 0, 
            float rotation = 0):base(texture, position, scale, speed, rotation)
        {
            this.father = father;
            moves = new MoveQueue(10);
        }

        public override void Update(GameTime gameTime)
        {
            Move nextMove = father.moves.GetNextMove();

            if (nextMove != null)
            {
                rotation = nextMove.rotation;
                speed = nextMove.speed;

                int moveAmount = (int)(speed * gameTime.ElapsedGameTime.TotalSeconds);

                position.X += (float)(moveAmount * Math.Cos(rotation));
                position.Y += (float)(moveAmount * Math.Sin(rotation));

                //Update Move List
                moves.Push(new Move(speed, rotation));
            }
        }

        public void SetFather(FollowingSprite father)
        {
            this.father = father;
        }

    }
}
