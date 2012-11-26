using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RainbowDragon.Core.Levels.Tutorials
{
    class BaseTutorial
    {

        protected String text;
        public bool isTutorialCompleted;
        protected SpriteFont font;
        protected Vector2 position;
        protected Level level;
        public string type { get; set; }
        protected Vector2 initialPos;
        protected Texture2D background;

        public String Text { get { return text; } set { text = value; } }
        

        public BaseTutorial(Level level, SpriteFont font, Vector2 position, Texture2D bg )
        {
            this.level = level;
            this.font = font;
            this.position = position;
            initialPos = position;
            this.position.Y = 0;
            this.position.X = initialPos.X / 2;
            background = bg;
            
        }


        public virtual void Update(GameTime gameTime)
        {

        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            position.X--;
            
            if (position.X + font.MeasureString(text).X < 0)
                position.X = initialPos.X;
            spriteBatch.Draw(background, new Rectangle(0, 0, (int)initialPos.X, (int)font.MeasureString(text).Y), Color.White);
            spriteBatch.DrawString(font, text, position, Color.White);
           

        }






    }
}
