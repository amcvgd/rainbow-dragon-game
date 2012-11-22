using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RainbowDragon.Core.Sprites;
using RainbowDragon.HelperClasses;

namespace RainbowDragon.Core.Player
{
    class DragonHead : DragonPart
    {
        public bool inversed = false;
        
        public DragonHead(Texture2D texture, Vector2 position, float scale = 1, float speed = 300, float rotation = 0)
            : base(null, texture, position, scale, speed, rotation) {
                moves = new MoveQueue(15);
        }

        public override void Update(GameTime gameTime)
        {
            GamePadState aGamePad = GamePad.GetState(PlayerIndex.One);
            KeyboardState aKeyboard = Keyboard.GetState();

            rotation += (float)(aGamePad.ThumbSticks.Left.X * 3.0f * gameTime.ElapsedGameTime.TotalSeconds);
            if (!inversed && (aKeyboard.IsKeyDown(Keys.Down) || aKeyboard.IsKeyDown(Keys.Right)) ||
                (inversed && (aKeyboard.IsKeyDown(Keys.Up) || aKeyboard.IsKeyDown(Keys.Left))))
            {
                rotation += (float)(1 * 3.0f * gameTime.ElapsedGameTime.TotalSeconds);
            }
            else if (inversed && (aKeyboard.IsKeyDown(Keys.Down) || aKeyboard.IsKeyDown(Keys.Right)) ||
                (!inversed && (aKeyboard.IsKeyDown(Keys.Up) || aKeyboard.IsKeyDown(Keys.Left))))
            {
                rotation -= (float)(1 * 3.0f * gameTime.ElapsedGameTime.TotalSeconds);
            }

            int moveAmount = (int)(speed * gameTime.ElapsedGameTime.TotalSeconds);

            position.X += (float)(moveAmount * Math.Cos(rotation));
            position.Y += (float)(moveAmount * Math.Sin(rotation));

            //Update Move List
            moves.Push(new Move(speed, rotation, Position));

            if (position.X - size.Width / 2 > 1366)
                position.X = 0;
            if (position.X + size.Width / 2 < 0)
                position.X = 1366;
            if (position.Y + size.Height / 2 < 0)
                position.Y = 768;
            if (position.Y - size.Height / 2 > 768)
                position.Y = 0;
        }
    }
}
