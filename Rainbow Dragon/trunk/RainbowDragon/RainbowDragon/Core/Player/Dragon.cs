using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using RainbowDragon.HelperClasses;

namespace RainbowDragon.Core.Player
{
    class Dragon
    {
        //This is my version of the dragon. It doesn't mean we have to use it.

        List<DragonPart> dragonBody;
        LinkedList<DragonPart> dragonBody1;
        DragonHead dragonHead;
        int bodySize; //how many body parts it currently has
        bool isMoving;
        //enum Directions {left, up, right, down, none};
        int currentDirection;

        Dictionary<Vector2, int> dragonPath; 
        public Dragon()
        {

        }

        public void Initialize(Vector2 startingPos)
        {
            isMoving = false;
            currentDirection = Constants.NO_DIRECTION;
            bodySize = 1;
            dragonBody = new List<DragonPart>();
            dragonBody.Add(new DragonPart(bodySize)); //this adds the head
            bodySize++;
            dragonBody.Add(new DragonPart(bodySize)); //this will be the tail for now
            dragonBody[bodySize].SetFather(dragonBody[bodySize - 1]);
            //dragonBody[dragonBody.Count - 1].CheckCollision();
            dragonBody1 = new LinkedList<DragonPart>();
            dragonBody1.AddFirst(new DragonPart(0));
            dragonBody1.AddLast(new DragonPart(1));
            dragonHead = new DragonHead(0);
            //dragonBody1.AddAfter(dragonBody1.First, new DragonPart(2));

        }
        //here it is overriden because the dragon might need a more complicated version of the spritebatch.draw() method.
        public void Draw(SpriteBatch spriteBatch)
        {

        }

        public void Update(GameTime gameTime)
        {
            KeyboardState keyState = Keyboard.GetState();
            if(keyState.IsKeyDown(Keys.Up))
            {
                if(currentDirection != Constants.DOWN)
                    currentDirection = Constants.UP;


            }
            else if(keyState.IsKeyDown(Keys.Down))
            {
                if (currentDirection != Constants.UP)
                    currentDirection = Constants.DOWN;


            }
            else if(keyState.IsKeyDown(Keys.Left))
            {
                if (currentDirection != Constants.RIGHT)
                    currentDirection = Constants.LEFT;


            }
            else if(keyState.IsKeyDown(Keys.Right))
            {
                if (currentDirection != Constants.LEFT)
                    currentDirection = Constants.RIGHT;


            }

            if (dragonHead.getDirection() != currentDirection)
            {
                dragonHead.ChangeDirection(currentDirection);
            }

            Vector2 prevPos = dragonBody[0].GetPosition();

            dragonBody[0].Update(gameTime);
            dragonBody1.First.Value.Update(gameTime);
            

            foreach (DragonPart d in dragonBody)
            {
                /*Vector2 tempPos;
                tempPos = d.GetPosition();
                d.SetNewPosition(prevPos);*/
                d.Update(gameTime);

            }


        }

    }
}
