using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using View.Controls;
using View.ScreenManagement;

namespace View.Screens
{
    /// <summary>
    /// 
    /// </summary>
    class HighscoreScreen : Screen
    {
        #region Fields

        ContentManager _content;
        List<Control> _controls;

        #endregion

        #region Initialization

        /// <summary>
        /// Constructor.
        /// </summary>
        public HighscoreScreen()
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

            var button_off_bg = _content.Load<Texture2D>("button_off");
            var button_on_bg = _content.Load<Texture2D>("button_on");
            var list_bg = _content.Load<Texture2D>("list");
            int width = button_off_bg.Width / 2;
            int height = button_off_bg.Height / 2;
            int topleftX = fullscreen.Center.X - width;
            int topleftY = fullscreen.Center.Y - 100 - height;

            var lstHighscores = new Controls.List(list_bg, new Vector2(topleftX, topleftY), "TOP 5");
            lstHighscores.Font = _content.Load<SpriteFont>("menufont");
            _controls.Add(lstHighscores);

            var btnBack = new Controls.Button(button_on_bg, button_off_bg,
                                          new Vector2(
                                            topleftX,
                                            topleftY + list_bg.Height + 10),
                                          "BACK TO MAIN MENU");
            btnBack.Clicked += (sender, args) =>
            {
                ScreenManager.RemoveScreen(this);
                ScreenManager.AddScreen(new MainMenu(), ControllingPlayer);
            };
            btnBack.Font = _content.Load<SpriteFont>("menufont");
            _controls.Add(btnBack);
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
