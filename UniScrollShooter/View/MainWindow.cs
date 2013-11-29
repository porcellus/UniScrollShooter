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
using View.ScreenManagement;
using View.Screens;


namespace View
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class MainWindow : Game
    {        
        #region Fields

        readonly GraphicsDeviceManager _graphics;
        readonly ScreenManager _screenManager;

        // By preloading any assets used by UI rendering, we avoid framerate glitches
        // when they suddenly need to be loaded in the middle of a menu transition.
        static readonly string[] PreloadAssets =
        {
            "blank", "button_off", "button_on", "list"
        };

        #endregion

        public MainWindow()
        {
            IsMouseVisible = true;
            Window.AllowUserResizing = true;
            Content.RootDirectory = "Content";

            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth = 1280;
            _graphics.PreferredBackBufferHeight = 720;

            // Create the screen manager component.
            _screenManager = new ScreenManager(this);

            Components.Add(_screenManager);

            // Activate the first screens.
            _screenManager.AddScreen(new BackgroundScreen(), null);
            _screenManager.AddScreen(new MainMenu(), null);
        }

        public void ToggleFullscreen()
        {
            _graphics.ToggleFullScreen();
        }

        #region Content Load-Unload
        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            foreach (var asset in PreloadAssets)
            {
                Content.Load<object>(asset);
            }
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }
        #endregion

        #region Update&Draw
        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            _graphics.GraphicsDevice.Clear(Color.Black);

            // The real drawing happens inside the screen manager component.

            base.Draw(gameTime);
        }
        #endregion
    }

    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (var game = new MainWindow())
            {
                game.Run();
            }
        }
    }
}
