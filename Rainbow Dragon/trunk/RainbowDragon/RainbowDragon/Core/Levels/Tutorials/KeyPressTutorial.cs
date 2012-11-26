using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace RainbowDragon.Core.Levels.Tutorials
{
    class KeyPressTutorial:BaseTutorial
    {

        public List<Keys> keys { get; set; }


        public KeyPressTutorial(Level level, SpriteFont font, Vector2 position, Texture2D bg)
            : base(level, font, position, bg)
        {
            keys = new List<Keys>();
        }
        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            KeyboardState state= Keyboard.GetState();
            foreach (Keys key in keys)
            {
                if (state.IsKeyDown(key))
                {
                    isTutorialCompleted = true;
                    level.NextTutorial();
                    break;
                }
                

            }
        } 

        


    }
}
