using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RainbowDragon.HelperClasses;

namespace RainbowDragon.Core.Screens
{
    class PauseScreen
    {

        Texture2D background;
        Texture2D continueGame;
        Texture2D backToMainScreen;
        Texture2D exit;
        Texture2D inGameTexture;
        Texture2D arrow;
        Rectangle scrollRect;
        Game1 game;
        int arrowState;
        Vector2 arrowPosition;
        Vector2 continuePosition;
        int timeElapsed = 0;
        int maxTime = 150;

        public PauseScreen(Game1 game1, Texture2D inGameTexture)
        {
            this.inGameTexture = inGameTexture;
            game = game1;
            background = game1.Content.Load<Texture2D>("Pause\\scroll");
            continueGame = game1.Content.Load<Texture2D>("Pause\\continue");
            backToMainScreen = game1.Content.Load<Texture2D>("Pause\\mainScreen");
            exit = game1.Content.Load<Texture2D>("Pause\\exit");
            arrow = game1.Content.Load<Texture2D>("Title\\arrow");
            scrollRect = new Rectangle(game.Window.ClientBounds.Width / 2 - background.Width / 2, game.Window.ClientBounds.Height / 2 - background.Height / 2,
                                        background.Width, background.Height);
            continuePosition = new Vector2(scrollRect.Center.X - continueGame.Width / 2, scrollRect.Center.Y - continueGame.Height*2);
        }

        public void Update(GameTime gameTime)
        {
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

               

            }

            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                if (arrowState == 0)
                {
                    game.ChangeState(Constants.GAME_STATE_INGAME);
                    game.UnPauseGame();
                    
                }
                else if (arrowState == 1)
                {
                    game.ChangeState(Constants.GAME_STATE_START);
                }
                else if (arrowState == 2)
                {
                    game.Exit();

                }

            }

            

        }


        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(inGameTexture, Vector2.Zero, Color.White);
            spriteBatch.Draw(background, scrollRect, Color.White);

            switch (arrowState)
            {
                case 0:
                    arrowPosition = new Vector2(continuePosition.X - arrow.Width,continuePosition.Y);
                    break;
                case 1:
                    arrowPosition = new Vector2(continuePosition.X - arrow.Width, continuePosition.Y + continueGame.Height * 2);
                    break;
                case 2:
                    arrowPosition = new Vector2(continuePosition.X - arrow.Width, continuePosition.Y + continueGame.Height * 4);
                    break;


            }

            spriteBatch.Draw(arrow, arrowPosition, Color.White);
            spriteBatch.Draw(continueGame, continuePosition, Color.White);
            spriteBatch.Draw(backToMainScreen, new Vector2(continuePosition.X, continuePosition.Y + continueGame.Height*2), Color.White);
            spriteBatch.Draw(exit, new Vector2(continuePosition.X, continuePosition.Y + continueGame.Height*4), Color.White);
            spriteBatch.End();

        }
    }
}
