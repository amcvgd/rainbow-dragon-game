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
        //int[,] grid;
        int screenWidth;
        int screenHeight;
        Dragon mainPlayer;
        LevelManager levelManager;
        ContentLoader loader;
        MusicPlayer mPlayer;
        GraphicsDevice graphics;
        Game1 game;
        public InGameScreen(Game1 game1)
        {
            screenHeight = game1.Window.ClientBounds.Height;
            screenWidth = game1.Window.ClientBounds.Width;
            loader = new ContentLoader(game1);
            mPlayer = new MusicPlayer(game1);
            graphics = game1.GraphicsDevice;
            game = game1;
        }

        public void Initialize()
        {
            mainPlayer = new Dragon(3, loader, mPlayer);
            levelManager = new LevelManager(loader,game, mPlayer);
            levelManager.Initialize(screenWidth, screenHeight);
            mainPlayer.Initialize(new Vector2(screenWidth/2, screenHeight/2));
        }
        public void Update(GameTime gameTime)
        {
            if (levelManager.currentLevel.currentTutorial != null)
            {
                if (levelManager.currentLevel.currentTutorial.isTutorialCompleted || levelManager.currentLevel.currentTutorial.type != Constants.KEY_PRESS_TUTORIAL)
                {
                    levelManager.Update(gameTime);
                    mainPlayer.Update(gameTime);
                    levelManager.CheckForCollision(mainPlayer);
                }
                else
                {
                    levelManager.currentLevel.currentTutorial.Update(gameTime);
                }
            }
            else
            {
                levelManager.Update(gameTime);
                mainPlayer.Update(gameTime);
                levelManager.CheckForCollision(mainPlayer);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            levelManager.Draw(spriteBatch);
            spriteBatch.End();
            mainPlayer.Draw(spriteBatch);
        }


        public void UnPauseLevel()
        {
            levelManager.UnPause();
        }
    }
}
