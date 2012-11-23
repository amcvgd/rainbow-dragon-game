using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace RainbowDragon.Core.Pickups
{
    class InvincibleSun:Sun
    {

        Color[] colors = { Color.Red, Color.Orange, Color.Yellow, Color.Green, Color.Blue, Color.Indigo, Color.Violet };
        int currentColor = 0;
        int maxTime = 100;
        int timeElapsed;
        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            color = colors[currentColor];
            timeElapsed += gameTime.ElapsedGameTime.Milliseconds;
            if (timeElapsed >= maxTime)
            {
                currentColor++;
                timeElapsed = 0;
            }
                if (currentColor >= colors.Length)
                currentColor = 0;
            base.Update(gameTime);
        }
    }
}
