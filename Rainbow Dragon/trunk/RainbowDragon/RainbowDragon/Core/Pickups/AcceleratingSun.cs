using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace RainbowDragon.Core.Pickups
{
    class AcceleratingSun:Sun
    {

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            if (isInvisible)
            {
                alpha += (float)1 / (100 * size); //TODO: change logic
                if (alpha >= 1)
                    isInvisible = false;
            }
            else
            {
                rotation += MathHelper.ToRadians(5);
            }
            //base.Update(gameTime);
            CheckEye(gameTime);
        }
    }
}
