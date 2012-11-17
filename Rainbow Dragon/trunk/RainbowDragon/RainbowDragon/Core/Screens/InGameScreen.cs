using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RainbowDragon.Core.Player;
using RainbowDragon.Core.Levels;
using RainbowDragon.HelperClasses;

namespace RainbowDragon.Core.Screens
{
    class InGameScreen
    {
        int[,] grid;
        int screenWidth;
        int screenHeight;
        Dragon mainPlayer;
        LevelManager levelManager;
        ContentLoader loader;

        public InGameScreen(Game1 game1)
        {

            screenHeight = game1.Window.ClientBounds.Height;
            screenWidth = game1.Window.ClientBounds.Width;
            loader = new ContentLoader(game1);
        }

        public void Initialize()
        {
            mainPlayer = new Dragon(2, loader);
            levelManager = new LevelManager(loader);
            levelManager.Initialize();
            mainPlayer.Initialize(new Vector2(screenWidth/2, screenHeight/2));
        }
        public void Update(GameTime gameTime)
        {
            levelManager.Update(gameTime);
            mainPlayer.Update(gameTime);

            levelManager.CheckForCollision(mainPlayer);
        }

        public void Draw(SpriteBatch spriteBatch)
        {

        }

    }
}
