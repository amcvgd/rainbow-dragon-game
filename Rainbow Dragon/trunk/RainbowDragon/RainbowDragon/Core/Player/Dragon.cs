using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using RainbowDragon.HelperClasses;
using RainbowDragon.Core.Sprite;

namespace RainbowDragon.Core.Player
{
    class Dragon
    {
        List<DragonPart> dragon;                            //The Dragon's body parts
        List<FollowingSprite> rainbow;                      //The sections of the rainbow

        Texture2D dragonHead, dragonBodyPart, dragonTail;   //Textures for the head, body, and tail of the dragon
        Texture2D rainbowTexture;                           //Texture for the rainbow

        int bodySize;                                       //How many body sections the dragon has (does not include head & tail)
                                                            //Perhaps we can have the bodySize scale with the difficulty level?
        int rainbowMeter, maxRainbowMeter = 100;            //How much rainbow meter the player has, and the max rainbow meter
        int charge, maxCharge = 20;                         //How much the color blast is charged, and the max charge
        bool isCharging;                                    //Are we currently charging the blast?

        KeyboardState prevKeyState = new KeyboardState();   //Keyboard input; used to make sure the buttons are released
        GamePadState prevPadState = new GamePadState();     //Controller input; same deal

        /// <summary>
        /// Initializes all of our variables.
        /// Then, creates a head, however many body parts there are, and finally, the tail.
        /// </summary>
        public Dragon(int bodySize)
        {
            this.bodySize = bodySize;
            dragon = new List<DragonPart>();
            rainbow = new List<FollowingSprite>();
        }

        public void LoadContent(ContentManager contentManager)
        {
            dragonHead = contentManager.Load<Texture2D>("Core\\Dragon\\Car");
            dragonBodyPart = contentManager.Load<Texture2D>("Core\\Dragon\\Car");
            dragonTail = contentManager.Load<Texture2D>("Core\\Dragon\\Car");
            rainbowTexture = contentManager.Load<Texture2D>("Core\\Dragon\\Car");

            dragon.Add(new DragonHead(dragonHead, Vector2.Zero, 0.5f));                           //Head 

            for (int i = 0; i < bodySize; i++)
            {
                dragon.Add(new DragonPart(dragon[i], dragonBodyPart, Vector2.Zero, 0.5f));        //Body Parts
            }

            dragon.Add(new DragonPart(dragon[bodySize], dragonBodyPart, Vector2.Zero, 0.5f));     //Tail
        }

        /// <summary>
        /// Calls the methods to handle our input, and to manage our rainbow trail
        /// Also calls each individual update for our dragon & rainbow trail
        /// </summary>
        public void Update(GameTime gameTime)
        {
            HandleInput();
            HandleRainbow();
            
            foreach (DragonPart part in dragon)
            {
                part.Update(gameTime);
            }

            foreach (FollowingSprite sec in rainbow)
            {
                sec.Update(gameTime);
            }
        }

        /// <summary>
        /// Calls the Draw functions for each piece of the dragon and our rainbow trail
        /// </summary>
        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (DragonPart part in dragon)
            {
                part.Draw(spriteBatch);
            }

            foreach (FollowingSprite sec in rainbow)
            {
                sec.Draw(spriteBatch);
            }
        }


        private void HandleInput()
        {
            GamePadState aGamePad = GamePad.GetState(PlayerIndex.One);
            KeyboardState aKeyboard = Keyboard.GetState();

            //If we are currently charging...
            if (isCharging)
            {
                //If we are still holding down the fire button
                if (aKeyboard.IsKeyDown(Keys.Space) || aGamePad.Triggers.Right >= 0.25f)
                {
                    if (IsFullyCharged())
                    {
                        //We are fully charged

                    }
                    else if (rainbowMeter == 0)
                    {
                        //We are out of rainbow meter, and therefore we cannot continue charge the blast

                    }
                    else
                    {
                        //Charge up the Blast -- Charging uses up our rainbowMeter
                        charge++;
                        rainbowMeter--;
                    }
                }
                else
                {
                    //Fire Color Blast Thing -- The blast radius will be dependent upon the amount of charge that has been accrued


                    //We are no longer charging, so reset charge to 0
                    isCharging = false;
                    charge = 0;
                }
            }
            else if ((aKeyboard.IsKeyDown(Keys.Space) && !prevKeyState.IsKeyDown(Keys.Space)) ||
                (aGamePad.Triggers.Right >= 0.25f && prevPadState.Triggers.Right < 0.25f))
            {
                if (rainbowMeter < 10)
                {
                    //The player does not have enough meter to fire/charge a blast
                    //Do something to let the player know that they cannot fire a blast yet

                }
                else
                {
                    charge = 10;
                    rainbowMeter -= 10;
                    isCharging = true;
                }
            }

            prevKeyState = aKeyboard;
            prevPadState = aGamePad;
        }

        /// <summary>
        /// Manages our rainbow trail
        /// </summary>
        public void HandleRainbow()
        {
           int sections = rainbowMeter / 10;

            //If this occurs, then our rainbow trail is up-to-date
            if (sections == rainbow.Count) return;

            //If our rainbow is too long, make it shorter
            while (sections < rainbow.Count)
                rainbow.RemoveAt(rainbow.Count - 1);

            //If our rainbow is too short, then create new pieces for it
            while (sections > rainbow.Count)
            {
                FollowingSprite father;

                //The first part of our rainbow follows the dragon's tail
                if (rainbow.Count == 0)
                    father = dragon[dragon.Count - 1];
                else
                    father = rainbow[rainbow.Count - 1];

                rainbow.Add(new FollowingSprite(father, rainbowTexture, father.position, 0.2f, father.speed, father.rotation));
            }
        }

        public void AddToRainbowMeter(int amt)
        {
            rainbowMeter += amt;

            if (rainbowMeter > maxRainbowMeter)
                rainbowMeter = maxRainbowMeter;
        }

        public bool IsMeterFull()
        {
            return rainbowMeter == maxRainbowMeter;
        }

        public bool IsFullyCharged()
        {
            return charge == maxCharge;
        }
    }
}
