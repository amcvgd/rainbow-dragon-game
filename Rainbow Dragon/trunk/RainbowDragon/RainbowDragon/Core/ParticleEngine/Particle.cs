using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RainbowDragon
{
    /// <summary>
    /// Class representing an individual Particle.
    /// </summary>
    public class Particle
    {
        public Texture2D Texture;           //The particle's texture
        public Vector2 Position;            //The particle's position
        public Vector2 Velocity;            //The particle's velocity
        public float Angle;                 //The current rotation angle of the particle
        public float AngularVelocity;       //The angular velocity of the particle
        public Color Color;                 //The particle's color
        public float Size;                  //How big the particle is
        public int TTL;                     //Time To Live, or how long the particle will live

        /// <summary>
        /// Constructor. Simply takes in value for each field.
        /// </summary>
        public Particle(Texture2D texture, Vector2 position, Vector2 velocity, float angle,
            float angularVelocity, Color color, float size, int ttl)
        {
            Texture = texture;
            Position = position;
            Velocity = velocity;
            Angle = angle;
            AngularVelocity = angularVelocity;
            Color = color;
            Size = size;
            TTL = ttl;
        }

        /// <summary>
        /// -Decrements lifespan of the particle
        /// -Adjusts the Position and Angle according to Velocity and Ang. Velocity, respectively
        /// </summary>
        public void Update()
        {
            TTL--;
            Position += Velocity;
            Angle += AngularVelocity;
        }

        /// <summary>
        /// Draw the sprite.
        /// </summary>
        public void Draw(SpriteBatch spriteBatch)
        {
            Rectangle sourceRectangle = new Rectangle(0, 0, Texture.Width, Texture.Height);
            Vector2 origin = new Vector2(Texture.Width / 2, Texture.Height / 2);

            spriteBatch.Draw(Texture, Position, sourceRectangle, Color, Angle,
                origin, Size, SpriteEffects.None, 0);
        }
    }
}
