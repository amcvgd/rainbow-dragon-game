using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace RainbowDragon
{
    //Represents a Link inside of a Linked List
    //This class holds a position and a rotation, and a link to the next Link in the list
    class Link
    {
        //Data
        public Vector2 position;
        public float rotation;

        //Next Link
        public Link next;

        //Link constructor
        public Link(Vector2 position, float rotation)
        {
            this.position = position;
            this.rotation = rotation;
            next = null;
        }
    }
    
    //A FIFO Queue
    //This class is used to keep track of the moves of each follow sprite, for the following sprite to be able to follow at a delay
    class Queue
    {
        //First and Last Link in the queue
        public Link first, last;

        int size;       //Maximum size of Queue
        int capacity;   //Maximum capacity of the queue, also can be seen as the amount of delay for the following sprite

        public int Size
        {
            get { return size; }
        }

        public int Capacity
        {
            get { return capacity; }
            set { capacity = value; }
        }

        //Queue constructor, which sets maximum capacity
        public Queue(int capacity)
        {
            size = 0;
            this.capacity = capacity;
        }

        //Insert a new Link at the end of the queue
        public void Insert(Vector2 position, float rotation)
        {
            //If list is empty...
            if (size == 0)
            {
                first = new Link(position, rotation);
                last = first;
            }
            else
            {
                last.next = new Link(position, rotation);
                last = last.next;
            }

            size++;

            //If the size is greater than the capacity, remove the 
            if (size > capacity)
            {
                Remove();
            }
        }

        //Remove from the head of the queue
        public Link Remove()
        {
            Link temp = first;
            first = first.next;
            size--;
            return temp;
        }

        //Check if list isEmpty
        public bool IsEmpty()
        {
            return size == 0;
        }
    }
    
    /*
     * The FollowSprite class is for sprites that follow other sprites, like the body of the dragon.
     */
    public class FollowSprite : Sprite
    {
        public Queue moveQueue;         //Holds the list of moves for any follower to follow

        protected float rotation = 0;   //The rotation
        protected FollowSprite target;  //The target we are following
        protected bool isHead = false;  //If we are leading the trail (AKA if we are the head of the dragon)

        //This is the constructor we use if we are designated the head of the dragon
        public FollowSprite(ContentManager theContent, string location, float scale = 1)
            : base(theContent, location, scale)
        {
            isHead = true;
            moveQueue = new Queue(10);
        }

        //For every other piece of the dragon, we use this and designate the sprite ahead of us
        public FollowSprite(ContentManager theContent, string location, FollowSprite target, float scale = 1)
            : base(theContent, location, scale)
        {
            this.target = target;
            moveQueue = new Queue(10);
        }

        //Perform updates for our FollowSprites.
        //All FollowSprites are in constant motion; the only thing that really gets examined is the rotation
        //It looks better if we don't force a position change on our sprites, so we only examine the rotation
        //of the Sprite ahead of us.

        //Down and Up, as well as Left and Right, are used to control the head of the dragon
        public override void Update(GameTime gameTime)
        {
            if (isHead)     //Controls for the Head of the dragon
            {
                GamePadState aGamePad = GamePad.GetState(PlayerIndex.One);
                KeyboardState aKeyboard = Keyboard.GetState();

                // TODO: Add your update logic here
                rotation += (float)(aGamePad.ThumbSticks.Left.X * 3.0f * gameTime.ElapsedGameTime.TotalSeconds);
                if (aKeyboard.IsKeyDown(Keys.Down) || aKeyboard.IsKeyDown(Keys.Right))
                {
                    rotation += (float)(1 * 3.0f * gameTime.ElapsedGameTime.TotalSeconds);
                }
                if (aKeyboard.IsKeyDown(Keys.Up) || aKeyboard.IsKeyDown(Keys.Left))
                {
                    rotation -= (float)(1 * 3.0f * gameTime.ElapsedGameTime.TotalSeconds);
                }

                int aMove = (int)(200 * gameTime.ElapsedGameTime.TotalSeconds);

                position.X += (float)(aMove * Math.Cos(rotation));
                position.Y += (float)(aMove * Math.Sin(rotation));

                moveQueue.Insert(position, rotation);
            }
            else if (target.moveQueue.Size >= target.moveQueue.Capacity)        //If not the head, update the FollowSprite with the
                                                                                //rotation of our target
            {
                rotation = target.moveQueue.Remove().rotation;

                int aMove = (int)(200 * gameTime.ElapsedGameTime.TotalSeconds);

                position.X += (float)(aMove * Math.Cos(rotation));
                position.Y += (float)(aMove * Math.Sin(rotation));

                moveQueue.Insert(position, rotation);
            }

            base.Update(gameTime);
        }

        //Draws the Sprite out onto the screen
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, new Rectangle((int)position.X, (int)position.Y, width, height),
                new Rectangle(0, 0, texture.Width, texture.Height), Color.White, rotation,
                new Vector2(texture.Width / 2, texture.Height / 2), SpriteEffects.None, 0);

            base.Draw(spriteBatch);
        }
    }
}
