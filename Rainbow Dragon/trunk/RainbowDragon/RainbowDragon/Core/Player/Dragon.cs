using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using RainbowDragon.HelperClasses;
using RainbowDragon.Core.Sprite;

namespace RainbowDragon.Core.Player
{
    class Dragon
    {
        List<DragonPart> dragon;

        Texture2D dragonHead, dragonBodyPart, dragonTail;

        int bodySize;

        public Dragon(int bodySize)
        {
            this.bodySize = bodySize;
            
            dragon.Add(new DragonHead(dragonHead, Vector2.Zero));                           //Head 

            for (int i = 0; i < bodySize; i++)
            {
                dragon.Add(new DragonPart(dragon[i], dragonBodyPart, Vector2.Zero));        //Body Parts
            }

            dragon.Add(new DragonPart(dragon[bodySize], dragonBodyPart, Vector2.Zero));     //Tail
        }

        public void Update(GameTime gameTime)
        {
            foreach (DragonPart part in dragon)
            {
                part.Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (DragonPart part in dragon)
            {
                part.Draw(spriteBatch);
            }
        }
    }
}
