using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RainbowDragon.Core.Enemies;
using RainbowDragon.Core.Pickups;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using RainbowDragon.HelperClasses;
using Microsoft.Xna.Framework.Input;
using RainbowDragon.Core.Levels.Tutorials;

namespace RainbowDragon.Core.Levels
{
    class Level
    {
        public int levelNumber { get; set; }
        public List<Arrow> arrows { get; set; }
        public List<Arrow> deadArrows { get; set; }
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
        MusicPlayer mPlayer;
        GraphicsDevice graphics;
        bool circleAdded = false;
        LevelManager manager;
        bool isPaused = false;
        RenderTarget2D pauseTexture;
        public List<BaseTutorial> tutorials {get; set;}
        public BaseTutorial currentTutorial;
        int timeToChangeTutorial = 3000;
        int timeElapsedForTutorial;
        bool readyToChangeTutorial = false;
        


        public Level(ContentLoader contentLoader, GraphicsDevice graphs, LevelManager manager, MusicPlayer musicPlayer)
        {
            suns = new List<Sun>();
            arrows = new List<Arrow>();
            circles = new List<Circle>();
            loader = contentLoader;
            mPlayer = musicPlayer;
            graphics = graphs;
            circleTarget = new RenderTarget2D(graphs, graphics.PresentationParameters.BackBufferWidth, graphics.PresentationParameters.BackBufferHeight);
            mainTarget = new RenderTarget2D(graphs, graphics.PresentationParameters.BackBufferWidth, graphics.PresentationParameters.BackBufferHeight);
            pauseTexture = new RenderTarget2D(graphs, graphics.PresentationParameters.BackBufferWidth, graphics.PresentationParameters.BackBufferHeight);
            toColorEffect = contentLoader.AddEffect("to_color_effect");
           // Messenger<int, Vector2>.AddListener("add circle", AddCircle);
            //AddCircle(10, new Vector2(300, 300));
            deadArrows = new List<Arrow>();
            this.manager = manager;
            tutorials = new List<BaseTutorial>();
            
                 
        }

        public void Initialize()
        {
            Messenger<int, Vector2>.AddListener("add circle", AddCircle);
            mPlayer.PlaySong(Constants.IN_GAME_SONG);
            if (tutorials.Count != 0)
                currentTutorial = tutorials.ElementAt(0);
            else
                currentTutorial = null;
        }

        public void Kill()
        {
            Messenger<int, Vector2>.RemoveListener("add circle", AddCircle);
            mPlayer.StopSong();
        }

        public void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                isPaused = true;
                mPlayer.PauseSong();
            }

            foreach (Arrow arrow in arrows)
            {

                arrow.Update(gameTime);
            }


            foreach (Sun sun in suns)
            {
                sun.Update(gameTime);
            }

            if (!isPaused && !mPlayer.IsSongPlaying())
                mPlayer.ResumeSong();

            if (currentTutorial != null)
            {
                if (readyToChangeTutorial)
                {
                    timeElapsedForTutorial -= gameTime.ElapsedGameTime.Milliseconds;
                    if (timeElapsedForTutorial <= 0)
                    {
                        ChangeTutorial();
                        readyToChangeTutorial = false;
                    }
                }


                if (!currentTutorial.isTutorialCompleted)
                    currentTutorial.Update(gameTime);
            }


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
            graphics.SetRenderTarget(null);
            
            graphics.SetRenderTarget(mainTarget);
            graphics.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            spriteBatch.Draw(bwBackground, backgroundRectangle, Color.White);
            //spriteBatch.Draw(coloredBackground, backgroundRectangle, Color.White);
            spriteBatch.End();

            if (!isPaused)
            {
                graphics.SetRenderTarget(null);
            }
            else
            {
                graphics.SetRenderTarget(pauseTexture);
            }
            graphics.Clear(Color.CornflowerBlue);
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
            spriteBatch.Draw(coloredBackground, backgroundRectangle, Color.White);
            toColorEffect.Parameters["lightMask"].SetValue(circleTarget);
            toColorEffect.CurrentTechnique.Passes[0].Apply();
            spriteBatch.Draw(mainTarget, Vector2.Zero, Color.White);
            spriteBatch.End();
            spriteBatch.Begin();

            foreach (Arrow arrow in arrows)
            {
                arrow.Draw(spriteBatch);
            }

            foreach (Sun sun in suns)
            {
                sun.Draw(spriteBatch);
            }

            if (circleAdded)
            {
                if (IsBackgroundColored(circleTarget) > 10)
                {
                    manager.IncreaseLevel();
                }
                circleAdded = false;
            }

            
            if (isPaused)
            {
                graphics.SetRenderTarget(null);
                manager.PauseGame(pauseTexture);
                
            }

            if (currentTutorial != null)
            {
                if (!currentTutorial.isTutorialCompleted)
                    currentTutorial.Draw(spriteBatch);
                //spriteBatch.Draw(loader.GetTexture(Constants.CHARGE_FIELD), new Vector2(300, 300), Color.White);
            }



        }

        public void UnPause()
        {
            isPaused = false;
        }

        public void RemoveArrows()
        {
            foreach(Arrow arrow in deadArrows)
            {
                arrows.Remove(arrow);

            }
            deadArrows.Clear();
        }

        public int IsBackgroundColored(Texture2D bg)
        {
            int size = circleTarget.Width * circleTarget.Height;
            Color[] buffer = new Color[size];
            bg.GetData(0, new Rectangle(0, 0, circleTarget.Width, circleTarget.Height), buffer, 0, size);

            //if (buffer.All(c => c == Color.White))
                //return true;
            int count = 0;
            int percentage = 0;
            for (int i = 0; i < buffer.Length; i++)
            {
                if (buffer[i] == Color.Black)
                {
                    count++;
                }
            }

            percentage = count * 100 / size;

            return percentage;
        }

        public void AddCircle(int radius, Vector2 position)
        {
            Circle newcircle = new Circle();
            newcircle.Initialize(loader.GetTexture(Constants.CHARGE_FIELD), new Rectangle((int)position.X - radius/2, (int)position.Y-radius/2, radius, radius));
            circles.Add(newcircle);
            circleAdded = true;
        }

        public void ChangeTutorial()
        {
            if (tutorials.IndexOf(currentTutorial) + 1 < tutorials.Count)
                currentTutorial = tutorials.ElementAt(tutorials.IndexOf(currentTutorial) + 1);
        }

        public void NextTutorial()
        {
            
            readyToChangeTutorial = true;
            timeElapsedForTutorial = timeToChangeTutorial;
            
        }



    }
}
