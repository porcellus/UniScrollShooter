using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using View.ScreenManagement;
using Data;

namespace View.Controls
{
    class ShopModules : Control
    {
        private Rectangle _bounds;
        private Texture2D _texture;
        private Texture2D _texture_buy_off;
        private Texture2D _texture_buy_on;
        private FileManager _fm;
        private Ship _ship;
        private List<Control>_controls;

        public new Vector2 Position
        {
            get { return base.Position; }

            set
            {
                base.Position = value;

                _bounds = new Rectangle((int)base.Position.X, (int)base.Position.Y, _texture.Width, _texture.Height);
            }
        }

        public ShopModules(Texture2D texture, Texture2D texture_buy_off, Texture2D texture_buy_on, SpriteFont font, SpriteFont hintFont, Vector2 position, string text, Ship ship)
            : base(position)
        {
            base.Text = text;
            base.Font = font;
            _texture = texture;
            _texture_buy_off = texture_buy_off;
            _texture_buy_on = texture_buy_on;
            _fm = new FileManager();
            _ship = ship;
            _bounds = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
            _controls = new List<Control>();

            int count = 0;
            foreach (Module module in _ship.GetModuleList())
            {
                ModuleControl _moduleControl = new ModuleControl(
                    _texture_buy_off,
                    _texture_buy_on,
                    new Vector2(base.Position.X + 20, 100 + 120*count),
                    _ship.GetModuleList()[count]);
                _moduleControl.Font = Font;
                _moduleControl.BuyClicked += (sender, args) =>
                {
                    //_ship.
                };
                _moduleControl.HintFont = hintFont;
                _controls.Add(_moduleControl);
                count++;
            }

            /*
            ModuleControl _bulletControl = new ModuleControl(
                    _texture_buy_off,
                    _texture_buy_on,
                    new Vector2(base.Position.X + 20, 100 + 50 * count),
                    _ship.);
            _bulletControl.Font = this.Font;
            _bulletControl.BuyClicked += (sender, args) =>
            {
                //_ship.
            };
            _controls.Add(_bulletControl);
            */

        }

        public override void UpdateInput(InputState input)
        {
            _controls.ForEach(a => a.UpdateInput(input));
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (Enabled)
            {
                spriteBatch.Draw(_texture, _bounds, Color);

                if (Font != null)
                {
                    DrawCenteredText(spriteBatch, Font, _bounds, Text, Color);
                }

                _controls.ForEach(a => a.Draw(spriteBatch));
            }
        }

        public static void DrawCenteredText(SpriteBatch batch, SpriteFont font, Rectangle rectangle, string text, Color color)
        {
            var size = font.MeasureString(text);
            var textWidth = size.X;
            var left = rectangle.Left + (rectangle.Width - textWidth) / 2;
            var top = rectangle.Top + 20;
            var topLeft = new Vector2(left, top);
            batch.DrawString(font, text, topLeft, color);
        }
    }
}

