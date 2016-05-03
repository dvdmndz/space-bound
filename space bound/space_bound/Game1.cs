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
        float elapsed;
        float elapsed2;
        float delay = 200f;
        int frames = 0;
        float timer;
        int timecounter;
        Double seconds = 0;
        const float TIMER = 1;
        float onesecondtimer = 0;
      
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            //full-screen
            this.graphics.PreferredBackBufferWidth = 800;
            this.graphics.PreferredBackBufferHeight = 480;

            this.graphics.IsFullScreen = false;//false para debug en consola
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            //EN ESTE LUGAR APARECE LA NAVE 
            destRect = new Rectangle(300, 300, 83, 107);
            base.Initialize();
            keyState = Keyboard.GetState();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            mov_nave = Content.Load<Texture2D>("nave");
            // TODO: use this.Content to load your game content here

        }

        /// <summary> 
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>


        int movSprite=0;
        private void changeSpriteLeft()
        {
            movSprite++;
            if (movSprite == 1)
            {
                sourceRect = new Rectangle(92, 0, 83, 107);
                Console.WriteLine("primero en " + movSprite);
            }
            else
            {
                if (movSprite == 31)
                {
                    sourceRect = new Rectangle(0, 0, 83, 107);
                    Console.WriteLine("segundo en " + movSprite);
                }
            }
        }

        private void changeSpriteRight()
        {
            movSprite++;
            if (movSprite == 1)
            {
                sourceRect = new Rectangle(284, 0, 83, 107);
                Console.WriteLine("primero en "+movSprite);
            }
            else
            {
                if (movSprite == 31)
                {
                    sourceRect = new Rectangle(384, 0, 83, 107);
                    Console.WriteLine("segundo en "+movSprite);
                }
            }
        }
        KeyboardState keyState;
        protected override void Update(GameTime gameTime)
        {
         
          
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            elapsed += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

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


            //ANIMACION DE SPRITE
            //     (parte de cuadro a tomar,,,parte de cuadro a tomar,,,tamaño de cuadro,,,tamaño de cuadro)   

            //   sourceRect = new Rectangle(188, 0, 83, 107);

            //Sprites a la izquierda
                if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Left))
                    changeSpriteLeft();
                else
                if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Right))
                    changeSpriteRight();
                else
                    movSprite = 0;
            //released
            
            if ((Keyboard.GetState(PlayerIndex.One).IsKeyUp(Keys.Right)&&(Keyboard.GetState(PlayerIndex.One).IsKeyUp(Keys.Left))))
            {
                
                    sourceRect = new Rectangle(188, 0, 83, 107);

                

            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            
            // TODO: Add your drawing code here
            spriteBatch.Begin();
            
            spriteBatch.Draw(mov_nave, destRect,sourceRect, Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
