using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using RainbowDragon.HelperClasses;
using RainbowDragon.Core.Sprite;

namespace RainbowDragon.Core.Player
{
    class DragonPart:MovingSprite
    {

        int index; //the index will determine the position of the body part relative to the head
        DragonPart father;
        
        enum Directions { left, up, right, down, none };
        int direction;
        
        public DragonPart(int index)
        {
            this.index = index;
            direction = Constants.NO_DIRECTION;
            speed = 10;
        }

        public override void Update(GameTime gameTime)
        {
            if (direction == Constants.DOWN)
            {
                if (position.X == father.GetPosition().X)
                {
                    position.Y += speed;
                }
                else if (position.X < father.GetPosition().X)
                {
                    position.X += speed;
                }
                else
                {
                    position.X -= speed;
                }
            }
            else if (direction == Constants.UP)
            {
                if (position.X == father.GetPosition().X)
                {
                    position.Y -= speed;
                }
                else if (position.X < father.GetPosition().X)
                {
                    position.X += speed;
                }
                else
                {
                    position.X -= speed;
                }
            }
            else if (direction == Constants.LEFT)
            {
                if (position.Y == father.GetPosition().Y)
                {
                    position.X -= speed;
                }
                else if (position.Y < father.GetPosition().Y)
                {
                    position.Y += speed;
                }
                else
                {
                    position.Y -= speed;
                }
            }
            else if (direction == Constants.RIGHT)
            {
                if (position.Y == father.GetPosition().Y)
                {
                    position.X += speed;
                }
                else if (position.Y < father.GetPosition().Y)
                {
                    position.Y += speed;
                }
                else
                {
                    position.Y -= speed;
                }
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

        public void SetFather(DragonPart newFather)
        {
            father = newFather;
        }

    }
}
