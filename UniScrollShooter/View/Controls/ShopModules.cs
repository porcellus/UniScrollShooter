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
        private Pilot _pilot;
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

        public ShopModules(Texture2D texture, Texture2D texture_buy_off, Texture2D texture_buy_on, SpriteFont font, SpriteFont hintFont, Vector2 position, string text, Pilot pilot)
            : base(position)
        {
            base.Text = text;
            base.Font = font;
            _texture = texture;
            _texture_buy_off = texture_buy_off;
            _texture_buy_on = texture_buy_on;
            _fm = new FileManager();
            _pilot = pilot;
            _bounds = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
            _controls = new List<Control>();

            int count = 0;
            foreach (Module module in _pilot.Ship.GetModuleList())
            {
                Module currentModule = _pilot.Ship.GetModuleList()[count];
                ModuleControl _moduleControl = new ModuleControl(
                    _texture_buy_off,
                    _texture_buy_on,
                    new Vector2(base.Position.X + 20, 160 + 140 * count),
                    currentModule);
                _moduleControl.Font = Font;
                _moduleControl.BuyClicked += (sender, args) =>
                {
                    Int32 price = currentModule.Price;
                    if (_pilot.Money >= price && currentModule.size < 3)
                    {
                        _pilot.UpgradeModuleOfShip(currentModule.kind);
                        _pilot.Money -= price;
                    }
                };
                _moduleControl.HintFont = hintFont;
                _controls.Add(_moduleControl);
                count++;
            }
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
                    DrawMoneyRow(spriteBatch, Font, _bounds, Color);
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

        private void DrawMoneyRow(SpriteBatch batch, SpriteFont font, Rectangle rectangle, Color color)
        {
            string textExp = "MONEY: " + _pilot.Money;
            var size = font.MeasureString(textExp);
            var textWidth = size.X;
            var textHeight = size.Y;
            var left = rectangle.Left + 20;
            var top = rectangle.Top + 80;
            var topLeft = new Vector2(left, top);
            batch.DrawString(font, textExp, topLeft, color);
        }
    }
}

