  using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace space_bound
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D mov_nave;
        Rectangle destRect;
        Rectangle sourceRect;
        scrollingbackground sf = new scrollingbackground();
        int fframe = 1;
        int sframe = 15;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            //full-screen
            this.graphics.PreferredBackBufferWidth = 800;
            this.graphics.PreferredBackBufferHeight = 480;

            this.graphics.IsFullScreen = false;//false para debug en consola
        }
        
        protected override void Initialize()
        {
            //EN ESTE LUGAR APARECE LA NAVE 
            destRect = new Rectangle(300, 300, 83, 107);
            base.Initialize();
            keyState = Keyboard.GetState();
        }
        
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            mov_nave = Content.Load<Texture2D>("nave");
            sf.LoadContent(Content);

        }
        
        protected override void UnloadContent()
        {
        }
        

        int movSprite=0;
        private void changeSpriteLeft()
        {
            movSprite++;
            if (movSprite == fframe)
            {
                sourceRect = new Rectangle(92, 0, 83, 107);
                Console.WriteLine("primero en " + movSprite);
            }
            else
            {
                if (movSprite == sframe)
                {
                    sourceRect = new Rectangle(0, 0, 83, 107);
                    Console.WriteLine("segundo en " + movSprite);
                }
            }
        }

        private void changeSpriteRight()
        {
            movSprite++;
            if (movSprite == fframe)
            {
                sourceRect = new Rectangle(284, 0, 83, 107);
                Console.WriteLine("primero en "+movSprite);
            }
            else
            {
                if (movSprite == sframe)
                {
                    sourceRect = new Rectangle(384, 0, 83, 107);
                    Console.WriteLine("segundo en "+movSprite);
                }
            }
        }
        KeyboardState keyState;
        protected override void Update(GameTime gameTime)
        {

            sf.Update(gameTime);
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            //mover nave
            if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Escape))
                this.Exit();
            //DERECHA
            if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Right))
                destRect.X +=9;
            //Izquierda
            if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Left))
                destRect.X -= 9;
            //ARRIBA
            if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Up))
                destRect.Y -= 11;
            //ABAJO
            if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Down))
                destRect.Y += 6;

            //LIMITES DE PANTALLA
            if (destRect.X > Window.ClientBounds.Width - destRect.Width)
                destRect.X = Window.ClientBounds.Width - destRect.Width;
            if (destRect.Y > Window.ClientBounds.Height - destRect.Height)
                destRect.Y = Window.ClientBounds.Height - destRect.Height;
            if (destRect.X < 0)
                destRect.X = 0;
            if (destRect.Y < 0)
                destRect.Y = 0;

            if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Left) && Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Right))
            {
                movSprite = 0;
                sourceRect = new Rectangle(188, 0, 83, 107);
            }
            else
                if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Left))
                changeSpriteLeft();
            else
                if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Right))
                changeSpriteRight();
            else
                movSprite = 0;
            
            if ((Keyboard.GetState(PlayerIndex.One).IsKeyUp(Keys.Right)&&(Keyboard.GetState(PlayerIndex.One).IsKeyUp(Keys.Left))))
            {
                    sourceRect = new Rectangle(188, 0, 83, 107);
            }

            base.Update(gameTime);
        }
        
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            
            spriteBatch.Begin();
            sf.Draw(spriteBatch);
            spriteBatch.Draw(mov_nave, destRect,sourceRect, Color.White);
            

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
