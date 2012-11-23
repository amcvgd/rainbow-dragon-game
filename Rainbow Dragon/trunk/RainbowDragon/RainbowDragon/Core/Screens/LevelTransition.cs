using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using RainbowDragon.HelperClasses;

namespace RainbowDragon.Core.Screens
{
    class LevelTransition
    {

        string levelName;
        int timeToWait;
        int timeElapsed;
        Game1 game;
        SpriteFont font;
        ContentLoader loader;
        public LevelTransition(Game1 game, int levelNumber)
        {
            timeToWait = 2500;
            levelName = "LEVEL " + levelNumber;
            this.game = game;
            font = game.Content.Load<SpriteFont>("Fonts\\chineseFont");
        }

        public void Update(GameTime gameTime)
        {

            timeElapsed += gameTime.ElapsedGameTime.Milliseconds;
            if (timeElapsed > timeToWait)
            {
                game.ChangeState(Constants.GAME_STATE_INGAME);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {

            spriteBatch.Begin();
            game.GraphicsDevice.Clear(Color.Black);
            spriteBatch.DrawString(font, levelName, new Vector2(game.GraphicsDevice.Viewport.Width / 2 - font.MeasureString(levelName).X/2, game.GraphicsDevice.Viewport.Height / 2 - font.MeasureString(levelName).Y/2), Color.White);
            spriteBatch.End();
        }

    }
}
