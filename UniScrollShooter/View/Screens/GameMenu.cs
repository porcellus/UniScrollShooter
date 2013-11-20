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
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
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
    class GameMenu : Screen
    {
        #region Fields

        ContentManager _content;

        List<Control> _controls;

        #endregion

        #region Initialization


        /// <summary>
        /// Constructor.
        /// </summary>
        public GameMenu()
        {
            TransitionOnTime = TimeSpan.FromSeconds(0.5);
            TransitionOffTime = TimeSpan.FromSeconds(0.5);
            _controls = new List<Control>();
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
            
            var bg = _content.Load<Texture2D>("button");
            var blank = _content.Load<Texture2D>("blank");

            var btn1 = new Controls.Button(bg, bg,
                                          new Vector2(
                                            fullscreen.Center.X - fullscreen.Width * 0.125f,
                                            fullscreen.Center.Y - fullscreen.Height * 0.125f),
                                          "Game");
            btn1.Clicked += (sender, args) => { ScreenManager.RemoveScreen(this); ScreenManager.AddScreen(new GameScreen(), ControllingPlayer); };
            btn1.Font = _content.Load<SpriteFont>("menufont");
            _controls.Add(btn1);

            var btn2 = new Controls.Button(bg, bg,
                                          new Vector2(
                                            fullscreen.Center.X - fullscreen.Width * 0.125f,
                                            fullscreen.Center.Y + fullscreen.Height * 0.125f),
                                          "Back");
            btn2.Clicked += (sender, args) => { ScreenManager.RemoveScreen(this); ScreenManager.AddScreen(new MainMenu(), ControllingPlayer); };
            btn2.Font = _content.Load<SpriteFont>("menufont");
            _controls.Add(btn2);
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
        /// Handles input and passes it on to it's controls.
        /// </summary>
        public override void HandleInput(InputState input)
        {
            base.HandleInput(input);

            _controls.ForEach(a=> a.UpdateInput(input));
        }

        /// <summary>
        /// Draws the screen calling every controls Draw.
        /// </summary>
        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;

            spriteBatch.Begin();

            _controls.ForEach(a => a.Draw(spriteBatch));

            spriteBatch.End();
        }


        #endregion
    }
}
