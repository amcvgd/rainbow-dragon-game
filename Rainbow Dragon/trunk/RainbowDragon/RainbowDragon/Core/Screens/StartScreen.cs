using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RainbowDragon.Core.Player;
using RainbowDragon.HelperClasses;
using Microsoft.Xna.Framework.Input;

namespace RainbowDragon.Core.Screens
{
    class StartScreen
    {
        Texture2D titleTexture;
        Texture2D background;
        Texture2D start;
        Texture2D options;
        Texture2D exit;
        Texture2D arrow;
        Game1 game;
        int scrWidth;
        int scrHeight;
        float alpha = 0;
        bool titleIsShown = false;
        Dragon dragon;
        ContentLoader loader;
        Vector2 startPosition;
        Rectangle bgRectangle;
        int arrowState;
        int arrowPosition;
        int maxTime = 150;
        int timeElapsed = 0;
        public StartScreen(Game1 game1)
        {
            game = game1;
            scrWidth = game.GraphicsDevice.Viewport.Width;
            scrHeight = game.GraphicsDevice.Viewport.Height;
            titleTexture = game.Content.Load<Texture2D>("Title\\title");
            background = game.Content.Load<Texture2D>("Title\\Clash");
            start = game.Content.Load<Texture2D>("Title\\start");
            options = game.Content.Load<Texture2D>("Title\\options");
            exit = game.Content.Load<Texture2D>("Title\\exit");
            arrow = game.Content.Load<Texture2D>("Title\\arrow");
            loader = new ContentLoader(game);
            dragon = new Dragon(3, loader);
            dragon.Initialize(new Vector2(scrWidth/2, scrHeight/2));
            bgRectangle = new Rectangle(scrWidth / 2 - titleTexture.Width / 2, scrHeight / 2 - titleTexture.Height / 2, titleTexture.Width, titleTexture.Height);
            startPosition.Y = bgRectangle.Bottom;
            startPosition.X = bgRectangle.Center.X - start.Width/2;
            arrowState = 0;
        }

        public void Update(GameTime gameTime)
        {
            if (alpha >= 1.0)
                titleIsShown = true;
            else
                alpha += 0.005f;

            timeElapsed += gameTime.ElapsedGameTime.Milliseconds;
            if (timeElapsed > maxTime)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Up))
                {
                    arrowState--;
                    if (arrowState < 0)
                        arrowState = 2;

                }
                else if (Keyboard.GetState().IsKeyDown(Keys.Down))
                {
                    arrowState++;
                    if (arrowState > 2)
                        arrowState = 0;

                   
                }

                timeElapsed = 0;

                if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                {
                    if (arrowState == 0)
                    {
                        game.ChangeState(Constants.GAME_STATE_INGAME);
                        game.InitializeScreen();
                    }
                    else if (arrowState == 1)
                    {

                    }
                    else if (arrowState == 2)
                    {
                        game.Exit();

                    }

                }

            }

            
            //dragon.Update(gameTime);
            
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(background, new Rectangle(0,0,scrWidth, scrHeight), Color.White);
            spriteBatch.Draw(titleTexture, bgRectangle, Color.White * alpha);
            if (titleIsShown)
            {
                switch (arrowState)
                {
                    case 0:
                        arrowPosition = (int)startPosition.Y;
                        break;
                    case 1:
                        arrowPosition = (int)startPosition.Y + start.Height;
                        break;
                    case 2:
                        arrowPosition = (int)startPosition.Y + start.Height*2;
                        break;


                }
                spriteBatch.Draw(arrow, new Rectangle((int)startPosition.X - arrow.Width, arrowPosition, arrow.Width, arrow.Height), Color.White);
                spriteBatch.Draw(start, new Rectangle((int)startPosition.X, (int)startPosition.Y, start.Width, start.Height), Color.White);
                spriteBatch.Draw(options, new Rectangle((int)startPosition.X, (int)startPosition.Y + start.Height, start.Width, start.Height), Color.White);
                spriteBatch.Draw(exit, new Rectangle((int)startPosition.X, (int)startPosition.Y  + start.Height*2, start.Width, start.Height), Color.White);
                
            }
            spriteBatch.End();
            //dragon.Draw(spriteBatch);

        }
    }
}
