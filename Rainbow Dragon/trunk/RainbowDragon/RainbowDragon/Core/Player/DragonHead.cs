using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RainbowDragon.Core.Sprite;
using RainbowDragon.HelperClasses;

namespace RainbowDragon.Core.Player
{
    class DragonHead : DragonPart
    {
        public DragonHead(Texture2D texture, Vector2 position, float scale = 1, float speed = 200, float rotation = 0)
            : base(null, texture, position, scale, speed, rotation) { }

        public override void Update(GameTime gameTime)
        {
            GamePadState aGamePad = GamePad.GetState(PlayerIndex.One);
            KeyboardState aKeyboard = Keyboard.GetState();

            rotation += (float)(aGamePad.ThumbSticks.Left.X * 3.0f * gameTime.ElapsedGameTime.TotalSeconds);
            if (aKeyboard.IsKeyDown(Keys.Down) || aKeyboard.IsKeyDown(Keys.Right))
            {
                rotation += (float)(1 * 3.0f * gameTime.ElapsedGameTime.TotalSeconds);
            }
            if (aKeyboard.IsKeyDown(Keys.Up) || aKeyboard.IsKeyDown(Keys.Left))
            {
                rotation -= (float)(1 * 3.0f * gameTime.ElapsedGameTime.TotalSeconds);
            }

            int moveAmount = (int)(speed * gameTime.ElapsedGameTime.TotalSeconds);

            position.X += (float)(moveAmount * Math.Cos(rotation));
            position.Y += (float)(moveAmount * Math.Sin(rotation));

            //Update Move List
            moves.Push(new Move(speed, rotation));
        }
    }
}
