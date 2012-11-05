using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using RainbowDragon.Core.Player;

namespace RainbowDragon.Core.Screens
{
    class InGameScreen
    {
        int [,]grid;
        int screenWidth;
        int screenHeight;
        
        
        Dragon mainPlayer;

        ContentManager content;

        public InGameScreen(Game1 game1)
        {
            content = game1.Content;
            screenHeight = game1.Window.ClientBounds.Height;
            screenWidth = game1.Window.ClientBounds.Width;
        }

        public void Initialize()
        {
            mainPlayer = new Dragon(3);

            mainPlayer.LoadContent(content); //this adds a dragon with only head and tail
        }
        public void Update(GameTime gameTime)
        {


        }

        public void Draw(SpriteBatch spriteBatch)
        {

        }

    }
}
