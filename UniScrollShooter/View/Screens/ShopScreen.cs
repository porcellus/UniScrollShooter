using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using View.Controls;
using View.ScreenManagement;
using Data;

namespace View.Screens
{
    class ShopScreen : Screen
    {
        #region Fields

        private ContentManager _content;
        private List<Control> _controls;
        private Pilot _pilot;

        #endregion

        #region Initialization

        /// <summary>
        /// Constructor.
        /// </summary>
        public ShopScreen(Pilot pilot)
        {
            TransitionOnTime = TimeSpan.FromSeconds(0.5);
            TransitionOffTime = TimeSpan.FromSeconds(0.5);
            _controls = new List<Control>();
            _pilot = pilot;
        }

        /// <summary>
        /// Loads graphics content for this screen. 
        /// </summary>
        public override void LoadContent()
        {
            if (_content == null)
                _content = new ContentManager(ScreenManager.Game.Services, "Content");

            Viewport viewport = ScreenManager.GraphicsDevice.Viewport;
            var fullscreen = new Rectangle(0, 0, viewport.Width, viewport.Height);

            var texture_button_off = _content.Load<Texture2D>("button_off");
            var texture_button_on = _content.Load<Texture2D>("button_on");
            var texture_list = _content.Load<Texture2D>("list");
            var texture_plus = _content.Load<Texture2D>("plus");
            var texture_minus = _content.Load<Texture2D>("minus");

            int width = texture_button_off.Width / 2;
            int height = texture_button_off.Height / 2;
            int shift = height * 2 + 10;
            int topleftX = fullscreen.Center.X - width;
            int topleftY = fullscreen.Center.Y - 100 - height;

            var shopPlayer = new Controls.ShopPlayer(texture_list, texture_plus, texture_minus, new Vector2(fullscreen.Left - 20 + fullscreen.Width / 2 - texture_list.Width, fullscreen.Top + 20), "PLAYER STATS", _pilot);
            shopPlayer.Font = _content.Load<SpriteFont>("menufont");
            _controls.Add(shopPlayer);

            var shopModule = new Controls.ShopModules(texture_list, new Vector2(fullscreen.Left + 20 + fullscreen.Width / 2, fullscreen.Top + 20), "MODULES", _pilot);
            shopModule.Font = _content.Load<SpriteFont>("menufont");
            _controls.Add(shopModule);

            var btnStart = new Controls.Button(texture_button_on, texture_button_off,
                                          new Vector2(
                                            shopModule.Position.X + texture_list.Width - texture_button_off.Width,
                                            shopModule.Position.Y + texture_list.Height + 20),
                                          "START GAME");
            btnStart.Clicked += (sender, args) =>
            {
                ScreenManager.RemoveScreen(this);
                ScreenManager.AddScreen(new GameScreen(_pilot), ControllingPlayer);
            };
            btnStart.Font = _content.Load<SpriteFont>("menufont");
            _controls.Add(btnStart);

            var btnBack = new Controls.Button(texture_button_on, texture_button_off,
                                          new Vector2(
                                            shopPlayer.Position.X,
                                            shopPlayer.Position.Y + texture_list.Height + 20),
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

            _controls.ForEach(a => a.UpdateInput(input));
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
