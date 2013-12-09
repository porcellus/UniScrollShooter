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
            var texture_bg = _content.Load<Texture2D>("list");
            var texture_plus_off = _content.Load<Texture2D>("plus_off");
            var texture_plus_on = _content.Load<Texture2D>("plus_on");
            var texture_minus_off = _content.Load<Texture2D>("minus_off");
            var texture_minus_on = _content.Load<Texture2D>("minus_on");
            var font = _content.Load<SpriteFont>("menufont");

            int width = texture_button_off.Width / 2;
            int height = texture_button_off.Height / 2;
            int shift = height * 2 + 10;
            int topleftX = fullscreen.Center.X - width;
            int topleftY = fullscreen.Center.Y - 100 - height;

            // Control to update player.
            var shopPlayer = new Controls.ShopPlayer(
                texture_bg, 
                texture_plus_off, 
                texture_plus_on, 
                texture_minus_off, 
                texture_minus_on, 
                font, 
                new Vector2(fullscreen.Left - 20 + fullscreen.Width / 2 - texture_bg.Width, fullscreen.Top + 20), 
                "PLAYER STATS", 
                _pilot);
            _controls.Add(shopPlayer);

            // Control to buy modules.
            var shopModule = new Controls.ShopModules(
                texture_bg,
                texture_plus_off, 
                texture_plus_on,
                font, 
                new Vector2(fullscreen.Left + 20 + fullscreen.Width / 2, fullscreen.Top + 20), 
                "MODULES", 
                _pilot.Ship);
            _controls.Add(shopModule);

            // Button to start the next map.
            var btnStart = new Controls.Button(
                texture_button_on,
                texture_button_off,
                new Vector2(
                    shopModule.Position.X + texture_bg.Width - texture_button_off.Width,
                    shopModule.Position.Y + texture_bg.Height + 20),
                "START GAME");
            btnStart.Clicked += (sender, args) =>
            {
                ScreenManager.RemoveScreen(this);
                ScreenManager.AddScreen(new GameScreen(_pilot), ControllingPlayer);
            };
            btnStart.Font = _content.Load<SpriteFont>("menufont");
            _controls.Add(btnStart);

            // Button to quit to the main menu.
            var btnBack = new Controls.Button(
                texture_button_on, 
                texture_button_off,
                new Vector2(shopPlayer.Position.X, shopPlayer.Position.Y + texture_bg.Height + 20),
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
