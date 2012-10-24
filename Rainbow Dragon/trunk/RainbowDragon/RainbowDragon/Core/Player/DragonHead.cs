using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using RainbowDragon.Core.Sprite;
using RainbowDragon.HelperClasses;

namespace RainbowDragon.Core.Player
{
    class DragonHead:MovingSprite
    {
        int index; //the index will determine the position of the body part relative to the head
        DragonPart father;
        
        enum Directions { left, up, right, down, none };
        int direction;
       
        
        public DragonHead(int index)
        {
            this.index = index;
            direction = Constants.NO_DIRECTION;
            speed = 10;
        }

        public override void Update(GameTime gameTime)
        {
            if (direction == Constants.DOWN)
            {
                position.Y += speed;
            }
            else if (direction == Constants.UP)
            {
                position.Y -= speed;
            }
            else if (direction == Constants.LEFT)
            {
                position.X -= speed;
            }
            else if (direction == Constants.RIGHT)
            {
                position.X += speed;
            }
        }

        public void SetNewPosition(Vector2 newPos)
        {
            position = newPos;
        }

        public Vector2 GetPosition()
        {
            return position;
        }

        public void ChangeDirection(int  newDirection)
        {

            direction = newDirection;
        }

        public int getDirection()
        {
            return direction;
        }
        



    }
}
