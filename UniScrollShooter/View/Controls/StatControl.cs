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
    class StatControl : Control
    {
        public enum ButtonStatus { Up, Down }
        private Rectangle _bounds;
        private Rectangle _bounds_plus;
        private Rectangle _bounds_minus;
        private Texture2D _texture_plus_off;
        private Texture2D _texture_minus_off;
        private Texture2D _texture_plus_on;
        private Texture2D _texture_minus_on;
        private string _text;
        private int _value;
        private MouseState _state;
        private ButtonStatus _status;

        public string Text
        {
            set { _text = value; }
            get { return _text; }
        }

        public int Value   
        {
            set { _value = value; }
            get { return _value; }
        }

        public ButtonStatus Status
        {
            get { return _status; }
            set { _status = value; }
        }

        public event EventHandler PlusClicked;
        public event EventHandler MinusClicked;
    
        public new Vector2 Position
        {
            get { return base.Position; }

            set
            {
                base.Position = value;
                _bounds = new Rectangle((int)base.Position.X, (int)base.Position.Y, _texture_plus_off.Width + _texture_minus_off.Width, _texture_plus_off.Height + _texture_minus_off.Height);
                _bounds_plus = new Rectangle((int)base.Position.X, (int)base.Position.Y + _texture_plus_off.Height, _texture_plus_off.Width, _texture_plus_off.Height);
                _bounds_minus = new Rectangle((int)base.Position.X, (int)base.Position.Y, _texture_minus_off.Width, _texture_minus_off.Height);
            }
        }

        public StatControl(Texture2D texture_plus_off, Texture2D texture_plus_on, Texture2D texture_minus_off, Texture2D texture_minus_on, Vector2 position, string text, int value)
            : base(position)
        {
            _text = text;
            _value = value;
            _texture_plus_off = texture_plus_off;
            _texture_minus_off = texture_minus_off;
            _texture_plus_on = texture_plus_on;
            _texture_minus_on = texture_minus_on;
            _bounds = new Rectangle((int)base.Position.X, (int)base.Position.Y, _texture_plus_off.Width, _texture_plus_off.Height + _texture_minus_off.Height);
            _bounds_plus = new Rectangle((int)base.Position.X + 450, (int)base.Position.Y, _texture_plus_off.Width, _texture_plus_off.Height);
            _bounds_minus = new Rectangle((int)base.Position.X + 450, (int)base.Position.Y + texture_plus_off.Height, _texture_minus_off.Width, _texture_minus_off.Height);
        }

        public override void UpdateInput(InputState input)
        {
            _state = input.MouseState;

            if (Enabled)
            {
                if (PlusContainsPos(new Vector2(input.MouseState.X, input.MouseState.Y)))
                {
                    if (input.MouseState.LeftButton == ButtonState.Released && Status == ButtonStatus.Down)
                    {
                        if (PlusClicked != null)
                        {
                            // Fire the clicked event.        
                            PlusClicked(this, EventArgs.Empty);
                        }
                        Status = ButtonStatus.Up;
                    }
                    else if (input.MouseState.LeftButton == ButtonState.Pressed)
                    {
                        Status = ButtonStatus.Down;
                    }
                }
                else if (MinusContainsPos(new Vector2(input.MouseState.X, input.MouseState.Y)))
                {
                    if (input.MouseState.LeftButton == ButtonState.Released && Status == ButtonStatus.Down)
                    {
                        if (MinusClicked != null)
                        {
                            // Fire the clicked event.        
                            MinusClicked(this, EventArgs.Empty);
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

        protected bool PlusContainsPos(Vector2 pos)
        {
            return _bounds_plus.Contains((int)pos.X, (int)pos.Y);
        }

        protected bool MinusContainsPos(Vector2 pos)
        {
            return _bounds_minus.Contains((int)pos.X, (int)pos.Y);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (Enabled)
            {
                spriteBatch.Draw(PlusContainsPos(new Vector2(_state.X, _state.Y)) ? _texture_plus_on : _texture_plus_off, _bounds_plus, Color);
                spriteBatch.Draw(MinusContainsPos(new Vector2(_state.X, _state.Y)) ? _texture_minus_on : _texture_minus_off, _bounds_minus, Color);

                if (Font != null)
                {
                    DrawText(spriteBatch, Font, _bounds, _text, Color);
                    DrawValue(spriteBatch, Font, _bounds, _value, Color);
                }
            }
        }

        public void DrawText(SpriteBatch batch, SpriteFont font, Rectangle rectangle, string text, Color color)
        {
            var size = font.MeasureString(text);
            var textWidth = size.X;
            var textHeight = size.Y;
            var left = rectangle.Left;
            var top = rectangle.Top + (rectangle.Height - textHeight) / 2;
            var topLeft = new Vector2(left, top);
            batch.DrawString(font, text, topLeft, color);
        }

        public void DrawValue(SpriteBatch batch, SpriteFont font, Rectangle rectangle, int value, Color color)
        {
            string text = value.ToString();
            var size = font.MeasureString(text);
            var textWidth = size.X;
            var textHeight = size.Y;
            var left = _bounds_minus.X - textWidth - 20;
            var top = rectangle.Top + (rectangle.Height - textHeight) / 2;
            var topLeft = new Vector2(left, top);
            batch.DrawString(font, text, topLeft, color);
        }        
    }
}
