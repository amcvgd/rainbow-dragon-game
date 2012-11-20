using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RainbowDragon.Core.Enemies;
using RainbowDragon.Core.Pickups;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using RainbowDragon.HelperClasses;

namespace RainbowDragon.Core.Levels
{
    class Level
    {
        public int levelNumber { get; set; }
        public List<Arrow> arrows { get; set; }
        public List<Sun> suns { get; set; }
        public int time { get; set; }
        private Texture2D coloredBackground;
        private Texture2D bwBackground;
        public Texture2D ColoredBackgroud {get{return coloredBackground;}set{coloredBackground = value;}}
        public Texture2D BWBackgroud { get { return bwBackground; } set { bwBackground = value; } }
        private Rectangle backgroundRectangle;
        public Rectangle BackgroundRectangle { get { return backgroundRectangle; } set { backgroundRectangle = value; } }
        RenderTarget2D circleTarget;
        RenderTarget2D mainTarget;
        Effect toColorEffect;
        List<Circle> circles;
        ContentLoader loader;
        GraphicsDevice graphics;


        public Level(ContentLoader contentLoader, GraphicsDevice graphs)
        {
            suns = new List<Sun>();
            arrows = new List<Arrow>();
            circles = new List<Circle>();
            loader = contentLoader;
            graphics = graphs;
            circleTarget = new RenderTarget2D(graphs, graphics.PresentationParameters.BackBufferWidth, graphics.PresentationParameters.BackBufferHeight);
            mainTarget = new RenderTarget2D(graphs, graphics.PresentationParameters.BackBufferWidth, graphics.PresentationParameters.BackBufferHeight);
            toColorEffect = contentLoader.AddEffect("to_color_effect");
            Messenger<int, Vector2>.AddListener("add circle", AddCircle);
            AddCircle(10, new Vector2(300, 300));
            


        }

        public void Update()
        {


        }

        public void Draw(SpriteBatch spriteBatch)
        {
            

            graphics.SetRenderTarget(circleTarget);
            graphics.Clear(Color.White);
            //spriteBatch.Draw(bwBackground, backgroundRectangle, Color.White);
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
            
            foreach (Circle circle in circles)
            {

                circle.Draw(spriteBatch);
            }

            spriteBatch.End();

            graphics.SetRenderTarget(mainTarget);
            graphics.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            spriteBatch.Draw(bwBackground, backgroundRectangle, Color.White);
            //spriteBatch.Draw(coloredBackground, backgroundRectangle, Color.White);
            spriteBatch.End();

            graphics.SetRenderTarget(null);
            graphics.Clear(Color.CornflowerBlue);
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
            spriteBatch.Draw(coloredBackground, backgroundRectangle, Color.White);
            toColorEffect.Parameters["lightMask"].SetValue(circleTarget);
            toColorEffect.CurrentTechnique.Passes[0].Apply();
            
            spriteBatch.Draw(mainTarget, Vector2.Zero, Color.White);
            spriteBatch.End();
            spriteBatch.Begin();
            

            //spriteBatch.Draw(loader.GetTexture(Constants.CHARGE_FIELD), new Vector2(300, 300), Color.White);



        }

        public void AddCircle(int radius, Vector2 position)
        {
            Circle newcircle = new Circle();
            newcircle.Initialize(loader.GetTexture(Constants.CHARGE_FIELD), new Rectangle((int)position.X, (int)position.Y, radius*10, radius*10));
            circles.Add(newcircle);
        }



    }
}
