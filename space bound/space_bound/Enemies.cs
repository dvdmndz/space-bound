using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;


namespace space_bound
{
     class Enemies
    {
        public Texture2D texture;
        public Vector2 position;
        public Vector2 velocity;

        public bool isVisible = true;

        Random random = new Random();
        int randX, randY;

        public Enemies(Texture2D newTexture, Vector2 newPosition)
        {
            texture = newTexture;
            position = newPosition;

            randY = random.Next(-3, 3);
            randX = random.Next(-8, -3);

            velocity = new Vector2(randY, randX);
        }

        public void Update(GraphicsDevice graphics)
        {
            position -= velocity;
            if (position.X <= -1 || position.X+1 >= graphics.Viewport.Width - texture.Width)
                velocity.X = -velocity.X;
            if (position.Y >= graphics.Viewport.Height)
                isVisible = false;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }
    }
}
