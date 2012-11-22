using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using RainbowDragon.HelperClasses;
using RainbowDragon.Core.Sprites;
using RainbowDragon.Core.Levels;

namespace RainbowDragon.Core.Player
{
    class Dragon
    {
        public List<DragonPart> dragon;                     //The Dragon's body parts
        public DragonHead head;

        List<FollowingSprite> rainbow;                      //The sections of the rainbow

        Texture2D rainbowTexture;                           //Texture for the rainbow

        int bodySize;                                       //How many body sections the dragon has (does not include head & tail)
                                                            //Perhaps we can have the bodySize scale with the difficulty level?
        float rainbowMeter, maxRainbowMeter = 200;            //How much rainbow meter the player has, and the max rainbow meter
        float charge, maxCharge = 20;                         //How much the color blast is charged, and the max charge
        bool isCharging;                                    //Are we currently charging the blast?

        KeyboardState prevKeyState = new KeyboardState();   //Keyboard input; used to make sure the buttons are released
        GamePadState prevPadState = new GamePadState();     //Controller input; same deal

        float speedBoostTimer = 0;
        float invinciTimer = 0;
        float slowTimer = 0, poisonTimer = 0, inverseTimer = 0;
        float poiHitter = 0;

        bool invincible = false;
        
        ContentLoader contentLoader;

        List<Texture2D> particleTextures;
        ParticleEngine particleEngine;


        Circle circle;
        const int chargeMultiplier = 10; //used to find the radius of circle

        public int Meter
        {
            get { return (int)rainbowMeter; }
        }

        public int MaxMeter
        {
            get { return (int)maxRainbowMeter; }
        }

        public int Charge
        {
            get { return (int)charge; }
        }


        /// <summary>
        /// Initializes all of our variables.
        /// Then, creates a head, however many body parts there are, and finally, the tail.
        /// </summary>
        public Dragon(int bodySize, ContentLoader loader)
        {
            this.bodySize = bodySize;
            dragon = new List<DragonPart>();
            rainbow = new List<FollowingSprite>();
            contentLoader = loader;
            circle = new Circle();
        }

        //public void LoadContent(ContentManager contentManager)
        public void Initialize(Vector2 position)
        {
            //Dragon Creation
            dragon.Add(new DragonHead(contentLoader.AddTexture2(Constants.DRAGON_HEAD, Constants.DRAGON_HEAD_PATH), position, .75f));
            head = (DragonHead)dragon[0];
            for (int i = 0; i < bodySize; i++)
            {
                dragon.Add(new DragonPart(dragon[i],
                    contentLoader.AddTexture2(Constants.DRAGON_BODY + (i + 1), Constants.DRAGON_BODY_PATH + (i + 1)), position, .75f));
            }
            dragon.Add(new DragonPart(dragon[bodySize], 
                contentLoader.AddTexture2(Constants.DRAGON_TAIL, Constants.DRAGON_TAIL_PATH), position, .75f));
            
            //Rainbow Creation
            rainbowTexture = contentLoader.AddTexture2(Constants.RAINBOW_PART, Constants.RAINBOW_PART_PATH);
            rainbowMeter = 200;

            circle.Initialize(contentLoader.AddTexture2(Constants.RAINBOW_CIRCLE, Constants.EFFECT_BASE_PATH + Constants.RAINBOW_CIRCLE),
                new Rectangle((int)head.position.X + (Charge * chargeMultiplier) / 2, (int)head.position.Y + (Charge * chargeMultiplier) / 2, Charge * chargeMultiplier, Charge * chargeMultiplier));

            //Particle Engine Creation
            particleTextures = new List<Texture2D>();
            particleTextures.Add(contentLoader.AddTexture2(Constants.BLUE_PARTICLE, Constants.PARTICLE_BASE_PATH + Constants.BLUE_PARTICLE));
            particleTextures.Add(contentLoader.AddTexture2(Constants.GREEN_PARTICLE, Constants.PARTICLE_BASE_PATH + Constants.GREEN_PARTICLE));
            particleTextures.Add(contentLoader.AddTexture2(Constants.RED_PARTICLE, Constants.PARTICLE_BASE_PATH + Constants.RED_PARTICLE));
            particleEngine = new ParticleEngine(particleTextures, head.Position);

        }

        /// <summary>
        /// Calls the methods to handle our input, and to manage our rainbow trail
        /// Also calls each individual update for our dragon & rainbow trail
        /// </summary>
        public void Update(GameTime gameTime)
        {
            //Handle Controls
            HandleInput();

            //Control the Length of the Rainbow
            HandleRainbow();

            //Update Buff Timers
            UpdateTimers(gameTime);

            foreach (DragonPart part in dragon)
            {
                part.Update(gameTime);
            }

            foreach (FollowingSprite sec in rainbow)
            {
                sec.Update(gameTime);
            }

            //Update Particle Engine
            particleEngine.EmitterLocation = head.Position;
            particleEngine.Update();

        }

        /// <summary>
        /// Calls the Draw functions for each piece of the dragon and our rainbow trail
        /// </summary>
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            foreach (FollowingSprite sec in rainbow)
            {
                sec.Draw(spriteBatch);
            }
            
            foreach (DragonPart part in dragon)
            {
                part.Draw(spriteBatch);
            }

            if (isCharging)
            {
                circle.UpdateRectangle(new Rectangle((int)head.position.X - (Charge * chargeMultiplier) / 2, (int)head.position.Y - (Charge * chargeMultiplier) / 2, Charge * chargeMultiplier, Charge * chargeMultiplier));
                circle.Draw(spriteBatch);
            }

            spriteBatch.End();

            //Particle Drawing (Uses it's own Additive sprite batch drawing method for cooler color effects)
            particleEngine.Draw(spriteBatch);
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
                        particleEngine.Emit();
                    }
                    else if (rainbowMeter != 0)
                    {
                        //Charge up the Blast -- Charging uses up our rainbowMeter
                        charge+=.25f;
                        rainbowMeter-=.25f;
                    }
                }
                else
                {
                    //Fire Color Blast Thing -- The blast radius will be dependent upon the amount of charge that has been accrued
                    Messenger<int, Vector2>.Broadcast("add circle", Charge*chargeMultiplier, head.position);

                    //We are no longer charging, so reset charge to 0
                    isCharging = false;
                    charge = 0;
                    particleEngine.StopEmitting();
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
           int sections = (int)(rainbowMeter)/2;
                
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

                rainbow.Add(new FollowingSprite(father, rainbowTexture, father.position, .5f, father.speed, father.rotation));
            }
        }

        public void AddToRainbowMeter(int amt)
        {
            if (amt < 0)
            {
                if (invincible)
                    return;
                else if (rainbowMeter == 0)
                    return;             //Insert Death Code here.
            }


            rainbowMeter += amt;

            if (rainbowMeter > maxRainbowMeter)
                rainbowMeter = maxRainbowMeter;
            if (rainbowMeter < 0)
            {
                rainbowMeter = 0;
            }
        }

        public bool IsMeterFull()
        {
            return rainbowMeter == maxRainbowMeter;
        }

        public bool IsFullyCharged()
        {
            return charge == maxCharge;
        }

        //BUFFS
        public void SpeedBoost()
        {
            if (speedBoostTimer == 0)
                head.speed *= 2;
            speedBoostTimer = 7.5f;
        }

        public void Invinciblility()
        {
            if (invinciTimer == 0)
                invincible = true;
            invinciTimer = 3;
        }

        //DEBUFFS
        public void Slow()
        {
            if (slowTimer == 0)
                head.speed /= 2;
            slowTimer = 7.5f;
        }

        public void Inverse()
        {
            if (inverseTimer == 0)
                head.inversed = true;
            inverseTimer = 5;
        }

        public void Poison()
        {
            poisonTimer = 10;
            poiHitter = 1;
        }

        public void UpdateTimers(GameTime gameTime)
        {
            //Speed Boost Powerup
            if (speedBoostTimer > 0)
                speedBoostTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (speedBoostTimer < 0)
            {
                speedBoostTimer = 0;
                head.speed /= 2;
            }

            //Invincibility Powerup
            if (invinciTimer > 0)
                invinciTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (invinciTimer < 0)
            {
                invinciTimer = 0;
                invincible = false;
            }

            //Slow Debuff
            if (slowTimer > 0)
                slowTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (slowTimer < 0)
            {
                slowTimer = 0;
                head.speed *= 2;
            }

            //Inverse Debuff
            if (inverseTimer > 0)
                inverseTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (inverseTimer < 0)
            {
                inverseTimer = 0;
                head.inversed = false;
            }
            //Console.WriteLine("Inverse Timer is: " + inverseTimer);

            //Poison Debuff
            if (poisonTimer > 0)
                poisonTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (poisonTimer < 0)
            {
                poisonTimer = 0;
                poiHitter = 0;
            }
            //Console.WriteLine("Poison Timer is: " + poisonTimer);

            if (poiHitter > 0)
                poiHitter -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (poiHitter < 0)
            {
                AddToRainbowMeter(-2);
                if (poisonTimer > 0)
                    poiHitter = 1;
                else
                    poiHitter = 0;
            }
        }
    }
}
