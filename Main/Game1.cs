using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FlipBook
{
    // TODO: CLEAN UP SOLUTION; REMOVE UNEEDED COMMENTS AND ADD NOTES

    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        //RenderTarget2D target;

        Texture2D Pixel;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 1100;
            graphics.PreferredBackBufferHeight = 800;
            Globals.Graphics = graphics;

            Globals.ImageSize = new Vector2(128, 128);

            Globals.MouseIsVisible = true;
            this.Window.AllowUserResizing = true;
            this.Window.ClientSizeChanged += Window_ClientSizeChanged;
        }

        /// <summary>
        /// Windows API Event Handler to catch the Wondow_ClientSizeChanged Event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Window_ClientSizeChanged(object sender, EventArgs e)
        {
            //TODO: Make changes to handle the new window size.
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // Initialization Logic
            Globals.GraphicsDevice = this.GraphicsDevice;
            //a = new Texture2D(GraphicsDevice, 10, 10);
            PresentationParameters pp = GraphicsDevice.PresentationParameters;
            
            //target = new RenderTarget2D(GraphicsDevice, pp.BackBufferWidth, pp.BackBufferHeight, false, GraphicsDevice.DisplayMode.Format, DepthFormat.None);

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

            SpriteFonts.Arial_8 = Content.Load<SpriteFont>("Arial_8");
            Pixel = Content.Load<Texture2D>("Frames/Pixel");

            Textures.BuildSimpleTexture();


            // Load Game Content
            Textures.TextBox = this.Content.Load<Texture2D>("TextBox");
            Textures.Pencil = this.Content.Load<Texture2D>("Pencil");
            Textures.Eraser = this.Content.Load<Texture2D>("Eraser");
            Textures.Line = this.Content.Load<Texture2D>("Line");
            Textures.Grid = this.Content.Load<Texture2D>("Grid");
            Textures.AddFrame = this.Content.Load<Texture2D>("NewFrame");
            Textures.DeleteFrame = this.Content.Load<Texture2D>("DeleteFrame");
            Textures.PaintCan = this.Content.Load<Texture2D>("PaintCan");
            Textures.Rectangle = this.Content.Load<Texture2D>("Rectangle");
            Textures.Circle = this.Content.Load<Texture2D>("Circle");

            FrameManager.addNewFrame();
            FrameManager.addNewFrame();
            FrameManager.addNewFrame();
            FrameManager.addNewFrame();

            FrameManager.ActiveFrame = FrameManager.Frames[0];

            ScreenManager.AddScreen(new WorkSpaceScreen("Work Space", new Vector2(0, 0), new Vector2(500, 300)));
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            ScreenManager.Unload();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {

            // Update Logic
            Globals.GameTime = gameTime;

            this.IsMouseVisible = Globals.MouseIsVisible;

            Input.CurrentKeyboardState = Keyboard.GetState();

            Input.CurrentMouseState = Mouse.GetState();

            //TODO: Grid should fit within PaintScreen on Open

            if(Input.CurrentMouseState.LeftButton == ButtonState.Pressed)
            {
                if (Input.PreviousMouseState.LeftButton == ButtonState.Released)
                    Input.MouseLeftButtonState = MouseButtonState.Pressed;
                else
                    Input.MouseLeftButtonState = MouseButtonState.Held;
            }
            else
            {
                if (Input.PreviousMouseState.LeftButton == ButtonState.Released)
                    Input.MouseLeftButtonState = MouseButtonState.Inactive;
                else
                    Input.MouseLeftButtonState = MouseButtonState.Released;
            }


            ScreenManager.Update();
            FrameManager.Update();

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

            // Drawing
            spriteBatch.Begin();

            ScreenManager.Draw();

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
