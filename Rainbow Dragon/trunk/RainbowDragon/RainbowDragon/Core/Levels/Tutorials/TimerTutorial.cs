using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace RainbowDragon.Core.Levels.Tutorials
{
    class TimerTutorial:BaseTutorial
    {
        public int maxTime { get; set; }
        int elapsedTime = 0;

        public TimerTutorial(Level level, SpriteFont font, Vector2 position, Texture2D bg)
            : base(level,font, position, bg)
        {

        }
        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            elapsedTime += gameTime.ElapsedGameTime.Milliseconds;
            if (elapsedTime > maxTime)
            {
                isTutorialCompleted = true;
                level.NextTutorial();

            }

            //base.Update(gameTime);
        }

    }
}
