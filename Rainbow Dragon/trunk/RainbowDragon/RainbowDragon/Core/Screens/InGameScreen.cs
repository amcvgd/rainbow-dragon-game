using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RainbowDragon.Core.Player;

namespace RainbowDragon.Core.Screens
{
    class InGameScreen
    {
        int [,]grid;
        int screenWidth;
        int screenHeight;
        Dragon mainPlayer;
        public InGameScreen(Game1 game1)
        {

            screenHeight = game1.Window.ClientBounds.Height;
            screenWidth = game1.Window.ClientBounds.Width;
        }

        public void Initialize()
        {
            mainPlayer = new Dragon();

            mainPlayer.Initialize(new Vector2(screenWidth/2, screenHeight/2)); //this adds a dragon with only head and tail
        }
        public void Update(GameTime gameTime)
        {


        }

        public void Draw(SpriteBatch spriteBatch)
        {

        }

    }
}
