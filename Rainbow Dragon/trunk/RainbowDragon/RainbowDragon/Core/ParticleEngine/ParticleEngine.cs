using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RainbowDragon
{
    /// <summary>
    /// The Particle Engine class is used to create new particles.
    /// Every aspect of the engine is customizeable, from the texture and color of the particles,
    /// to the rate at which particles live and die.
    /// </summary>
    public class ParticleEngine
    {
        private Random random;                  //A random used to randomize aspects of each particle
        public Vector2 EmitterLocation;         //Location we are emitting from
        private List<Particle> particles;       //List used to hold our particles
        private List<Texture2D> textures;       //List holding our available textures
        private bool emit = false;

        /// <summary>
        /// The constructor takes in the list of available textures for the particles to be created,
        /// as well as the location to emit from.
        /// </summary>
        /// 
        /// <param name="textures"></param>
        /// The list of available textures.
        /// 
        /// <param name="location"></param>
        /// The location to emit from.
        public ParticleEngine(List<Texture2D> textures, Vector2 location)
        {
            EmitterLocation = location;
            this.textures = textures;
            this.particles = new List<Particle>();
            random = new Random();
        }

        /// <summary>
        /// Creates a new particle and returns it.
        /// The new particle is spawned with a random texture, velocity,
        /// angular velocity, color, size, and time to live (ttl).
        /// </summary>
        /// <returns></returns>
        private Particle GenerateNewParticle()
        {
            Texture2D texture = textures[random.Next(textures.Count)];
            Vector2 position = EmitterLocation;
            Vector2 velocity = new Vector2(
                1f * (float)(random.NextDouble() * 2 - 1),
                1f * (float)(random.NextDouble() * 2 - 1));
            float angle = 0;
            float angularVelocity = 0.1f * (float)(random.NextDouble() * 2 - 1);
            Color color = new Color(
                (float)random.NextDouble(),
                (float)random.NextDouble(),
                (float)random.NextDouble());
            float size = (float)random.NextDouble();
            int ttl = 20 + random.Next(40);

            return new Particle(texture, position, velocity, angle, angularVelocity, color, size, ttl);
        }

        /// <summary>
        /// Creates new particles, and removes old ones that have died out.
        /// </summary>
        public void Update()
        {
            int total = 10;

            if (emit)
            {
                for (int i = 0; i < total; i++)
                {
                    particles.Add(GenerateNewParticle());
                } 
            }

            for (int particle = 0; particle < particles.Count; particle++)
            {
                particles[particle].Update();
                if (particles[particle].TTL <= 0)
                {
                    particles.RemoveAt(particle);
                    particle--;
                }
            }
        }

        /// <summary>
        /// Calls each particles individual Draw function.
        /// BlendState is Additive to allow for color blending between the different particles and the background.
        /// Might look better without Blending; Would have to test
        /// </summary>
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive);
            for (int index = 0; index < particles.Count; index++)
            {
                particles[index].Draw(spriteBatch);
            }
            spriteBatch.End();
        }

        public void Emit()
        {
            emit = true;
        }

        public void StopEmitting()
        {
            emit = false;
        }
    }
}
