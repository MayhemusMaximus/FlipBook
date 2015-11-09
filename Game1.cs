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

namespace FlipBook
{

    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D a;
        RenderTarget2D target;

        Texture2D Pixel;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 500;
            Globals.Graphics = graphics;

            // TODO: UNVISIBLE MOUSE
            IsMouseVisible = true;
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
            Globals.GraphicsDevice = this.GraphicsDevice;
            a = new Texture2D(GraphicsDevice, 10, 10);
            PresentationParameters pp = GraphicsDevice.PresentationParameters;
            
            target = new RenderTarget2D(GraphicsDevice, pp.BackBufferWidth, pp.BackBufferHeight, false, GraphicsDevice.DisplayMode.Format, DepthFormat.None);
            ScreenManager.AddScreen(new WorkSpaceScreen("Work Space",new Vector2(0,0),new Vector2(500,300)));

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Globals.SpriteBatch = spriteBatch;
            changepixels(Color.Red);

            SpriteFonts.Arial_8 = Content.Load<SpriteFont>("Arial_8");
            Pixel = Content.Load<Texture2D>("Frames/Pixel");

            Textures.TextBox = Content.Load<Texture2D>("TextBox");

            // TODO: use this.Content to load your game content here
            //Frame1 = Content.Load<Texture2D>("Frames/1");
        }

        public void drawPixels(Vector2 location)
        {
            Color[] colors = new Color[a.Width * a.Height];
            a.GetData(colors);

            //Color[,] data2D = new Color[a.Width, a.Height];

            int ndx = 0;

            //Color c = data[ndx];

            for(int y = 0; y < a.Width; y++)
            {
                for(int x = 0; x < a.Height; x++)
                {
                    Rectangle destination = new Rectangle((int)location.X + (x * 10), (int)location.Y + (y * 10), 10, 10);
                    
                    spriteBatch.Draw(Pixel,destination, colors[ndx]);
                    ndx++;
                }
            }

        }

        public void changepixels(Color color)
        {
            //GraphicsDevice.SetRenderTarget(target);

            GraphicsDevice.Textures[0] = null;

            Color[] data = new Color[a.Width * a.Height];
            a.GetData(data);

            int ndx = 4 * a.Width + 3;

            for (int x = 0; x < a.Width * a.Height; x = x + 5 )
            {
                data[x] = color;
            }
                //data[ndx] = Color.Red;

            a.SetData(data);

            GraphicsDevice.Textures[0] = a;

            //GraphicsDevice.SetRenderTarget(null);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
            ScreenManager.Unload();
        }

        //KeyboardState previousKeyboardState;
        Boolean change = false;
        Double scale = 1.0f;

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here
            Input.CurrentKeyboardState = Keyboard.GetState();

            if (Input.CurrentKeyboardState.IsKeyDown(Keys.Space) && !Input.PreviousKeyboardState.IsKeyDown(Keys.Space)) change = true;

            Input.CurrentMouseState = Mouse.GetState();

            if (Input.CurrentMouseState.ScrollWheelValue != Input.PreviousMouseState.ScrollWheelValue)
            {
                Globals.ScaleChanged = true;
                if (Input.CurrentMouseState.ScrollWheelValue < Input.PreviousMouseState.ScrollWheelValue)
                    Globals.Scale--;
                else
                    Globals.Scale++;
            }
            else
                Globals.ScaleChanged = false;

            //if(currentMouseState.ScrollWheelValue)

            ScreenManager.Update();

            Input.PreviousKeyboardState = Input.CurrentKeyboardState;
            Input.PreviousMouseState = Input.CurrentMouseState;



            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();


            //spriteBatch.DrawString(SpriteFonts.Arial_8, s, new Vector2(100, 100), Color.Black);

            //if(change)
            //{
            //    changepixels(Color.Blue);
            //    spriteBatch.Draw(a, new Rectangle(0, 0, (int)scale * 50, (int)scale * 50), Color.White);
            //    change = false;
            //}
            //else
            //{
            //    spriteBatch.Draw(a, new Rectangle(0, 0, (int)scale * 50, (int)scale * 50), Color.White);
            //}


            //drawPixels(new Vector2(300, 300));

            ScreenManager.Draw();

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
