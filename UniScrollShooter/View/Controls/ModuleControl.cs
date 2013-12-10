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
    class ModuleControl : Control
    {
        private Rectangle _bounds;
        private Rectangle _bounds_buy;
        private Texture2D _texture_buy_off;
        private Texture2D _texture_buy_on;
        private Module _module;
        private MouseState _state;
        private ButtonStatus _status;

        public SpriteFont HintFont;

        public enum ButtonStatus { Up, Down }

        public ButtonStatus Status
        {
            get { return _status; }
            set { _status = value; }
        }

        public event EventHandler BuyClicked;

        public new Vector2 Position
        {
            get { return base.Position; }

            set
            {
                base.Position = value;
                _bounds = new Rectangle(
                    (int)base.Position.X,
                    (int)base.Position.Y,
                    _texture_buy_off.Width,
                    _texture_buy_off.Height + 20);
                _bounds_buy = new Rectangle(
                    (int)base.Position.X + 450,
                    (int)base.Position.Y,
                    _texture_buy_off.Width,
                    _texture_buy_off.Height);
            }
        }

        public ModuleControl(Texture2D texture_buy_off, Texture2D texture_buy_on, Vector2 position, Module module)
            : base(position)
        {
            _texture_buy_off = texture_buy_off;
            _texture_buy_on = texture_buy_on;
            _bounds = new Rectangle(
                (int)position.X,
                (int)position.Y,
                _texture_buy_off.Width,
                120);
            _bounds_buy = new Rectangle(
                (int)position.X + 450,
                (int)position.Y,
                _texture_buy_off.Width,
                _texture_buy_off.Height);
            _module = module;
        }

        public override void UpdateInput(InputState input)
        {
            _state = input.MouseState;

            if (Enabled)
            {
                if (ContainsPos(new Vector2(input.MouseState.X, input.MouseState.Y)))
                {
                    if (input.MouseState.LeftButton == ButtonState.Released && Status == ButtonStatus.Down)
                    {
                        if (BuyClicked != null)
                        {
                            BuyClicked(this, EventArgs.Empty);
                        }
                        Status = ButtonStatus.Up;
                    }
                    else if (input.MouseState.LeftButton == ButtonState.Pressed)
                    {
                        Status = ButtonStatus.Down;
                    }
                }
                else
                {
                    Status = ButtonStatus.Up;
                }
            }
        }

        private bool ContainsPos(Vector2 pos)
        {
            return _bounds_buy.Contains((int)pos.X, (int)pos.Y);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (Enabled)
            {
                spriteBatch.Draw(
                    ContainsPos(new Vector2(_state.X, _state.Y)) ? _texture_buy_on : _texture_buy_off, _bounds_buy, Color);

                if (Font != null)
                {
                    DrawKind(spriteBatch, Font, _bounds, _module.kind.ToString() + " - " + "Upgrade to level " + (_module.size + 1), Color);
                    DrawHint(spriteBatch, HintFont, _bounds, "Current level:\n    " + _module.Hint(), Color.Yellow);
                    DrawNextHerald(spriteBatch, HintFont, _bounds, "Next level:\n    " + _module.NextLevelHerald(), Color.LimeGreen);
                    //DrawText(spriteBatch, Font, _bounds, _module.Hint(), Color);
                }
            }
        }

        private void DrawKind(SpriteBatch batch, SpriteFont font, Rectangle rectangle, string text, Color color)
        {
            var left = rectangle.Left;
            var top = rectangle.Top;
            var topLeft = new Vector2(left, top);
            batch.DrawString(font, text, topLeft, color);
        }

        private void DrawHint(SpriteBatch batch, SpriteFont font, Rectangle rectangle, string text, Color color)
        {
            var size = font.MeasureString(text);
            var textWidth = size.X;
            var textHeight = size.Y;
            var left = rectangle.Left;
            var top = rectangle.Top + 40;
            var topLeft = new Vector2(left, top);
            batch.DrawString(font, text, topLeft, color);
        }

        private void DrawNextHerald(SpriteBatch batch, SpriteFont font, Rectangle rectangle, string text, Color color)
        {
            var size = font.MeasureString(text);
            var textWidth = size.X;
            var textHeight = size.Y;
            var left = rectangle.Left;
            var top = rectangle.Top + 80;
            var topLeft = new Vector2(left, top);
            batch.DrawString(font, text, topLeft, color);
        }
    }
}
