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
        Texture2D mov_nave,bulletTexture,enemyTexture;
        Texture2D ene;
        Rectangle destRect;//cuadro nave que se mueve
        Rectangle sourceRect;//imagen de sprite
        scrollingbackground sf = new scrollingbackground();
        int fframe = 1;
        int sframe = 15;
        float bulletDelay;
        public List<bullets> bulletList;
        //game world
        List<Enemies> enemies = new List<Enemies>();
        Random random = new Random();

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            //fullscreen
            this.graphics.PreferredBackBufferWidth = 1280;
            this.graphics.PreferredBackBufferHeight = 720;
            this.graphics.IsFullScreen = false;//false para debug en consola
            bulletList = new List<bullets>();
            bulletDelay = 10;
        }
        
        protected override void Initialize()
        {
            //EN ESTE LUGAR APARECE LA NAVE 
            destRect = new Rectangle(Window.ClientBounds.Width/2, Window.ClientBounds.Height/2, 83, 107);
            base.Initialize();
        }
        
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            bulletTexture = Content.Load<Texture2D>("bullets");
            mov_nave = Content.Load<Texture2D>("nave");
            enemyTexture = Content.Load<Texture2D>("123");
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

        float spawn = 0;
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

            if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Space))
            {
                shoot();
            }
            updateBullet();

            //enemigos lokis
            spawn += (float)gameTime.ElapsedGameTime.TotalSeconds;
            foreach (Enemies enemy in enemies)
                enemy.Update(graphics.GraphicsDevice);
            LoadEnemies();
            

            base.Update(gameTime);
        }
        int spriteBala = 0;
        private void shoot()
        {

            if (bulletDelay>=0)
            {
                bulletDelay--;
            }
            if (bulletDelay <= 0)
            {
                bullets bullet = new bullets(bulletTexture);
                bullet.position = new Vector2(destRect.X+3, destRect.Y-58);
                bullet.isvisible = true;
                if (bulletList.Count() < 20)
                    bulletList.Add(bullet);
            }
            if (bulletDelay==0)
            {
                bulletDelay = 10;
            }
                    Console.WriteLine(spriteBala);
        }

        public void updateBullet()
        {
            spriteBala++;
            foreach (bullets b in bulletList)
            {
                b.position.Y = b.position.Y - b.speed;
                
                if (b.position.Y <= -200)
                    b.isvisible = false;
            }

            for (int i=0;i<bulletList.Count();i++)
            {
                if (bulletList[i].isvisible != true)
                {
                    bulletList.RemoveAt(i);
                    i--;
                }
            }
            if (spriteBala < 4)
            {
                foreach (bullets b in bulletList)
                    b.boundBox = new Rectangle(0, 0, 41, 202);
            }
            else
            {
                if (spriteBala < 8)
                {
                    foreach (bullets b in bulletList)
                        b.boundBox = new Rectangle(41, 0, 41, 202);
                }
                else
                    spriteBala = 0;
            }

        }

        public void LoadEnemies()
        {
            int randx = random.Next(1,Window.ClientBounds.Width-1);
            if (spawn >= 1)
            {
                spawn = (float)0.8;
                if (enemies.Count() < 10)
                {
                    enemies.Add(new Enemies(enemyTexture, new Vector2(randx, Window.ClientBounds.Height - Window.ClientBounds.Height)));
                }
            }
            for (int i = 0; i < enemies.Count; i++)
                if (!enemies[i].isVisible)
                {
                    enemies.RemoveAt(i);
                    i--;
                }
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            
            spriteBatch.Begin();
            sf.Draw(spriteBatch);
            foreach (bullets b in bulletList)
                b.draw(spriteBatch);
            spriteBatch.Draw(mov_nave, destRect, sourceRect, Color.White);

            foreach (Enemies enemy in enemies)
                enemy.Draw(spriteBatch);

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
