using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RainbowDragon.HelperClasses;
using RainbowDragon.Core.Sprites;

namespace RainbowDragon.Core.Player
{
    class DragonPart : FollowingSprite
    {
        DragonPart father;
        int index = 0;

        public DragonPart(DragonPart father, Texture2D texture, Vector2 position, float scale = 1, float speed = 0, float rotation = 0)
            : base(father, texture, position, scale, speed, rotation) 
        {
            this.father = father;
        }

        public override void Update(GameTime gameTime)
        {
            Move nextMove = father.moves.GetNextMove();

            if (nextMove != null)
            {
                rotation = nextMove.rotation;
                speed = nextMove.speed;

                int moveAmount = (int)(speed * gameTime.ElapsedGameTime.TotalSeconds);

                position.X += (float)(moveAmount * Math.Cos(rotation));
                position.Y += (float)(moveAmount * Math.Sin(rotation));

                //Update Move List
                moves.Push(new Move(speed, rotation));
            }
        }

        public void SetFather(DragonPart father)
        {
            this.father = father;
        }

        public void SetIndex(int index)
        {
            this.index = index;
        }
    }
}
