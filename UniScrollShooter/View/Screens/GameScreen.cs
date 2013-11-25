#region File Description
//-----------------------------------------------------------------------------
// BackgroundScreen.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements

using System;
using System.Collections.Generic;
using System.Diagnostics;
using GameLogic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using View.Controls;
using View.ScreenManagement;

#endregion

namespace View.Screens
{
    /// <summary>
    /// The background screen sits behind all the other menu screens.
    /// It draws a background image that remains fixed in place regardless
    /// of whatever transitions the screens on top of it may be doing.
    /// </summary>
    class GameScreen: Screen
    {
        #region Fields

        ContentManager _content;

        Texture2D _shipTexture;
        private Texture2D _starTexture;
        private List<Vector2> _stars; // a hatterben elrepulo csillagok
        GameLogic.Game _game;

        #endregion

        #region Initialization


        /// <summary>
        /// Constructor.
        /// </summary>
        public GameScreen()
        {
            TransitionOnTime = TimeSpan.FromSeconds(0.5);
            TransitionOffTime = TimeSpan.FromSeconds(0.5);
            _stars = new List<Vector2>();
        }


        /// <summary>
        /// Loads graphics content for this screen. 
        /// </summary>
        public override void LoadContent()
        {
            if(_content == null)
                _content = new ContentManager(ScreenManager.Game.Services, "Content");

            Viewport viewport = ScreenManager.GraphicsDevice.Viewport;
            var fullscreen = new Rectangle(0, 0, viewport.Width, viewport.Height);

            _shipTexture = _content.Load<Texture2D>("spaceship");
            _starTexture = _content.Load<Texture2D>("star1");

            _game = new GameLogic.Game();
            _game.GameEnded += (sender, args) =>
                {
                    ScreenManager.RemoveScreen(this);
                    ScreenManager.AddScreen(new GameMenu(), ControllingPlayer);
                };

            var rnd = new Random();
            for (int i = 0; i < 100; ++i)
                _stars.Add(new Vector2(rnd.Next(0, 125) / 100f, rnd.Next(0, 100) / 100f));
            _game.Start();           
        }


        /// <summary>
        /// Unloads graphics content for this screen.
        /// </summary>
        public override void UnloadContent()
        {
            _content.Unload();
        }

        #endregion

        #region Update and Draw

        /// <summary>
        /// Updates background objects
        /// </summary>
        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
 	        base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);

            for (int i = 0; i < _stars.Count; ++i)
                _stars[i] = new Vector2((float) (_stars[i].X - gameTime.ElapsedGameTime.TotalSeconds/10), _stars[i].Y);

            _stars.RemoveAll(a => a.X < 0);
            var rnd = new Random();
            if (_stars.Count < 150 && rnd.Next(0, 100) > 70)
            {
                _stars.Add(new Vector2(1 + rnd.Next(0, 25) / 100f, rnd.Next(0, 100) / 100f));
            }
        }

        /// <summary>
        /// Handles input and passes it on to it's controls.
        /// </summary>
        public override void HandleInput(InputState input)
        {
            base.HandleInput(input);

            _game.Input = 
                new Input
                    {   InputPos = new Vector2(input.MouseState.X, input.MouseState.Y)
                    ,   ExitButtonPressed = input.CurrentKeyboardStates[0].IsKeyDown(Keys.Escape) && !input.LastKeyboardStates[0].IsKeyDown(Keys.Escape)
                    ,   PauseToggePressed = input.CurrentKeyboardStates[0].IsKeyDown(Keys.P) && !input.LastKeyboardStates[0].IsKeyDown(Keys.P)
                    };
        }

        /// <summary>
        /// Draws the screen calling every controls Draw.
        /// </summary>
        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;
            Viewport viewport = ScreenManager.GraphicsDevice.Viewport;
            var fullscreen = new Rectangle(0, 0, viewport.Width, viewport.Height);

            spriteBatch.Begin();
            var blank = _content.Load<Texture2D>("blank");
            spriteBatch.Draw(blank, fullscreen, Color.Black);

            foreach (var star in _stars)
            {
                spriteBatch.Draw(_starTexture, new Vector2(star.X * fullscreen.Width, star.Y * fullscreen.Height), Color.White);
            }

            var shipCenter = new Vector2(_shipTexture.Width / 2f, _shipTexture.Height / 2f);
            spriteBatch.Draw(_shipTexture, _game.CurrState.PlayerPosition - shipCenter, Color.White);

            spriteBatch.End();
        }

        #endregion
    }
}
