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
    public class scrollingbackground
    {
        public Texture2D texture;
        public Vector2 bgPos1, bgPos2;
        public int speed;

        //constructor
        public scrollingbackground()
        {
            texture = null;
            bgPos1 = new Vector2(0, 0);
            bgPos2 = new Vector2(0, -1080);
            speed = 5;
        }

        public void LoadContent(ContentManager Content)
        {
            texture = Content.Load<Texture2D>("fondos\\starfield");
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, bgPos1, Color.White);
            spriteBatch.Draw(texture, bgPos2, Color.White);
        }

        public void Update(GameTime gameTime)
        {
            bgPos1.Y = bgPos1.Y + speed;
            bgPos2.Y = bgPos2.Y + speed;

            //scrolling background repeating
            if (bgPos1.Y>=1080)
            {
                bgPos1.Y = 0;
                bgPos2.Y = -1080;
            }
            
        }
    }
}
