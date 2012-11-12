using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace RainbowDragon.HelperClasses
{
    /// <summary>
    /// This class is only used for the loading and retrieval of 
    /// content. Should be initialized at the beginning of game.
    /// </summary>
    class ContentLoader
    {
        Dictionary<string, Texture2D> textures;
        Game1 game;
        public ContentLoader(Game1 game1)
        {
            game = game1;

            textures = new Dictionary<string, Texture2D>();



        }

       
        /// <summary>
        /// Only adds and loads texture.
        /// </summary>
        /// <param name="nameTexture">Key for the dictionary</param>
        /// <param name="path">Path for content pipeline</param>
        public void AddTexture(string nameTexture, string path)
        {
            
            Texture2D tempTexture = game.Content.Load<Texture2D>(path);
            textures.Add(nameTexture, tempTexture);
        }

        /// <summary>
        /// Combination of AddTexture and GetTexture.
        /// It adds, loads texture and then it returns it.
        /// </summary>
        /// <param name="nameTexture">Key for dictionary</param>
        /// <param name="path">Path for content pipeline</param>
        /// <returns>returns texture</returns>
        public Texture2D AddTexture2(string nameTexture, string path)
        {

            if (!textures.ContainsKey(nameTexture))
            {
                Texture2D tempTexture = game.Content.Load<Texture2D>(path);
                textures.Add(nameTexture, tempTexture);
            }

            return textures[nameTexture];
        }

        /// <summary>
        /// Only returns a texture given a key
        /// </summary>
        /// <param name="key">Key for dictionary</param>
        /// <returns></returns>
        public Texture2D GetTexture(string key)
        {
            
            return textures[key];

        }

    }
}
