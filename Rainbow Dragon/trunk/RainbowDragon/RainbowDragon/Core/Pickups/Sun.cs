using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RainbowDragon.Core.Sprites;

namespace RainbowDragon.Core.Pickups
{
    class Sun : MovingSprite
    {
        public string type { get; set; }
        public bool isInvisible = false;
        protected Texture2D pupilTexture;
        protected Texture2D eyelidTexture;
        protected Texture2D eyeBaseTexture;
        protected Texture2D closedEyelid;
        protected Texture2D zzzTexture;
        protected Vector2 playerPosition;
        protected bool closedEyed = false;
        protected bool animatingEye = false;
        protected int currentTimeEye = 0;
        protected int maxTimeEye = 500;
        protected float zzzScale = 0;
        public Random rand { get; set; }

        public Sun(Texture2D texture, Vector2 position, int size, string type)
            : base(texture, position, size)
        {
            this.type = type;

            
        }

        public void InitializeTextures(Texture2D pupil, Texture2D eyelid, Texture2D closedEyelid, Texture2D eyebase, Texture2D zzz)
        {
            pupilTexture = pupil;
            eyelidTexture = eyelid;
            eyeBaseTexture = eyebase;
            this.closedEyelid = closedEyelid;
            this.zzzTexture = zzz;

        }


        public Sun()
        {
            //rand = new Random((int)position.X + (int)position.Y);
            

        }
        protected int size;
        //protected Texture2D texture;
        //protected Vector2 position;
        public int Size { get { return size; } set { size = value; } }


        public override void Update(GameTime gameTime)
        {
            
            //base.Update(gameTime);
            if (isInvisible)
            {
                alpha += (float)1 / (100 * size); //TODO: change logic
                if (alpha >= 1)
                    isInvisible = false;
            }
            else
            {
                rotation += MathHelper.ToRadians(1);
            }

            CheckEye(gameTime);

            

        }


        protected void CheckEye(GameTime gameTime)
        {
            if (!closedEyed)
            {
                if (rand.Next(1, 50) == 1)
                {
                    closedEyed = true;


                }
            }
            if (closedEyed)
            {
                currentTimeEye += gameTime.ElapsedGameTime.Milliseconds;
                if (currentTimeEye >= maxTimeEye)
                {
                    currentTimeEye = 0;
                    closedEyed = false;


                }
            }
        }
        public void UpdatePlayerPosition(Vector2 pos)
        {
            playerPosition = pos;
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            int xRatio = texture.Width / Source.Width;
            int yRatio = texture.Height / Source.Height;

           


            float newAngle = (float)Math.Atan2(playerPosition.Y - position.Y, playerPosition.X - position.X);

      
            spriteBatch.Draw(eyeBaseTexture, new Rectangle((int)(position.X + 20/xRatio* Math.Cos(newAngle)), (int)(position.Y + 20/yRatio * Math.Sin(newAngle)), eyeBaseTexture.Width / xRatio, eyeBaseTexture.Height / yRatio), null, Color.White, 0f, new Vector2(eyeBaseTexture.Width / 2, eyeBaseTexture.Height / 2), SpriteEffects.None, 0);
            spriteBatch.Draw(eyelidTexture, new Rectangle((int)(position.X + 30/xRatio * Math.Cos(newAngle)), (int)(position.Y + 30/yRatio * Math.Sin(newAngle)), eyelidTexture.Width / xRatio, eyelidTexture.Height / yRatio), null, Color.White, 0f, new Vector2(eyelidTexture.Width / 2, eyelidTexture.Height / 2), SpriteEffects.None, 0);
            spriteBatch.Draw(pupilTexture, new Rectangle((int)(position.X + 50 / xRatio* Math.Cos(newAngle)), (int)(position.Y + 30 / yRatio *Math.Sin(newAngle)), pupilTexture.Width / xRatio, pupilTexture.Height / yRatio), null, Color.White, 0f, new Vector2(pupilTexture.Width / 2, pupilTexture.Height / 2), SpriteEffects.None, 0);
            if (closedEyed || isInvisible)
            {
                spriteBatch.Draw(closedEyelid, new Rectangle((int)(position.X + 30 / xRatio * Math.Cos(newAngle)), (int)(position.Y + 30 / yRatio * Math.Sin(newAngle)), eyelidTexture.Width / xRatio, eyelidTexture.Height / yRatio), null, Color.White, 0f, new Vector2(eyelidTexture.Width / 2, eyelidTexture.Height / 2), SpriteEffects.None, 0);
                if (isInvisible)
                {
                    zzzScale += .01f;
                    if (zzzScale >= 1.0f)
                    {
                        zzzScale = 0;
                    }
                    spriteBatch.Draw(zzzTexture, new Rectangle((int)position.X , (int)position.Y, (int)(zzzTexture.Width / xRatio * zzzScale),(int) (zzzTexture.Height / yRatio * zzzScale)), null, Color.White, 0f, new Vector2(0, zzzTexture.Height), SpriteEffects.None, 0);

                }
            }

            
        }
       
        public void DisappearSun()
        {

            alpha = 0;
            isInvisible = true;
        }

    }
}
