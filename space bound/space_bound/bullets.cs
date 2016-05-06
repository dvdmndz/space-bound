using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace space_bound
{
    public class bullets
    {
        public Rectangle boundBox;
        public Texture2D texture;
        public Vector2 origin;
        public Vector2 position;
        public bool isvisible;
        public float speed;

        public bullets(Texture2D newTexture)
        {
            speed = 9   ;
            texture = newTexture;
            isvisible = false;
        }

        public void draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }

    }
}
