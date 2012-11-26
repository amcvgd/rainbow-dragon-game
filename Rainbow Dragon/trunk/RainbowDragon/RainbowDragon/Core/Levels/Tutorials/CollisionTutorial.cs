using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace RainbowDragon.Core.Levels.Tutorials
{
    class CollisionTutorial:BaseTutorial
    {
        public string target { get; set; } //the class of the target


        public CollisionTutorial(Level level, SpriteFont font, Vector2 position, Texture2D bg)
            : base(level, font, position, bg)
        {

        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            

            //base.Update(gameTime);
        }

        public void CheckForCollision(string tg)
        {
            if (tg == target)
            {
                isTutorialCompleted = true;
                level.NextTutorial();
                
            }
        }


    }
}
