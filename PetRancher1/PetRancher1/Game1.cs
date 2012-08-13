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




namespace PetRancher1
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        SpriteFont font;
        SpriteBatch ForegroundBatch;

        Sprite mSprite;

        Color screenBG = Color.CornflowerBlue;

        CurrentSession session;

        KeyboardState kbState = Keyboard.GetState();
        KeyboardState oldKbState = Keyboard.GetState();

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);

            int screenW = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            int screenH = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;


            graphics.PreferredBackBufferHeight = 1080;
            
            graphics.PreferredBackBufferWidth = screenW / 2;

            Content.RootDirectory = "Content";

            int framesPerSecond = 1000 / 7;

            TargetElapsedTime = new TimeSpan(0, 0, 0, 0, framesPerSecond);

        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {

            //Tools.Print(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width.ToString());
          // TODO: Add your initialization logic here
            mSprite = new Sprite();
            Console.WriteLine("Initializing Game1.Init stuff");
            base.Initialize();

            //window size



            //load user and pet into a session instance
            this.session = new CurrentSession();
            session.createUser();
            
            //add some food items to its inventory
            session.user.inventory.AddItem(new FoodFruit());
            session.user.inventory.AddItem(new FoodApple());

            //give him a pet
            session.addPet("PerfectPet");
            

            //TextBasedGame.GameMain(session);
            
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            ForegroundBatch = new SpriteBatch(GraphicsDevice);
            font = Content.Load<SpriteFont>("times");

            // TODO: use this.Content to load your game content here
            mSprite.LoadContent(this.Content, "dogImage", 4, 3, 23, 32, new Point(0,0));
            mSprite.index.Y = 2;
            mSprite.Position = new Vector2(100, 100);
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
        protected override void Update(GameTime gameTime)
        {
           //input manager
            InputManager(new GameStateMain());

            mSprite.GoToNextFrame();
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// Handles where to send what control to send the Input to, either
        /// a character, or a menu etc
        /// </summary>
        /// <param name="state">Where to send the input.</param>
        public void InputManager(GameState state)
        {
            kbState = Keyboard.GetState();

            //if the game is in the main gameplay mode
            if (state.GetType() == typeof(GameStateMain))
            {

                // Allows the game to exit
                if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                    this.Exit();

                else if (kbState.IsKeyDown(Keys.Escape))
                {
                    this.Exit();
                }

                else if (kbState.IsKeyDown(Keys.F) && oldKbState.IsKeyUp(Keys.F))
                {
                    FeedAction feeding = new FeedAction(session.user);
                    screenBG = Color.Aqua;
                    feeding.FeedPet(session.pet, new FoodFruit());
                }

                else if (kbState.IsKeyDown(Keys.Space) && oldKbState.IsKeyUp(Keys.Space))
                {
                    mSprite.GoToNextFrame();
                }
            }


            oldKbState = kbState;
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(screenBG);

            // TODO: Add your drawing code here
            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend,
                SamplerState.PointClamp,DepthStencilState.Default, RasterizerState.CullNone,
                effect:null);
            mSprite.Draw(this.spriteBatch);

            ForegroundBatch.Begin();
            ForegroundBatch.DrawString(font, DateTime.Now.ToString() + "\n\nJr", new Vector2(100,100), Color.White);
            ForegroundBatch.End();

            spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}
