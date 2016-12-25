﻿using ConwaysGameOfLife;
using GameOfLife_WinProj.Classes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

/// <summary>
/// TODO - Create a video recorder, that records output at a set framerate
/// TODO - match window width to cell width and quantity
/// </summary>
namespace GameOfLife_WinProj
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private GameBoard gameOfLife;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            // load settings from XML File
            Misc.GetSettings();
            Misc.SetWinDemensionsToGame();
            graphics.PreferredBackBufferHeight = Misc.GameSettings.WindowHeight;
            graphics.PreferredBackBufferWidth = Misc.GameSettings.WindowWidth;

            // passing through half a second interval
            gameOfLife = new GameBoard(Misc.GameSettings.TurnTime);
            gameOfLife.Game = new GameOfLife(Misc.GameSettings.GameHeight, Misc.GameSettings.GameWidth, Misc.GameSettings.ChanceOfLife);
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


            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            if(Keyboard.GetState().IsKeyDown(Keys.R))
            {
                gameOfLife = new GameBoard(Misc.GameSettings.TurnTime);
                gameOfLife.Game = new GameOfLife(Misc.GameSettings.GameHeight, Misc.GameSettings.GameWidth, Misc.GameSettings.ChanceOfLife);
            }

            // TODO: Add your update logic here

            gameOfLife.Update(gameTime);
            if(Misc.GameSettings.RecordGame)
            {
                gameOfLife.GetScreenShot(graphics.GraphicsDevice);
            }
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.SkyBlue);

            // TODO: Add your drawing code here

            var texture1px = new Texture2D(graphics.GraphicsDevice, 1, 1);
            var rectangle = new Rectangle(0, 0, 10, 10);
            texture1px.SetData(new Color[] { Color.White });

            spriteBatch.Begin();

            // test draw

            //spriteBatch.Draw(texture1px, rectangle, Color.Red);

            gameOfLife.Draw(graphics, spriteBatch);
            
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}