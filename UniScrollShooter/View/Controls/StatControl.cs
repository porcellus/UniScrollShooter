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
        private Rectangle _bounds;
        private Rectangle _bounds_plus;
        private Rectangle _bounds_minus;
        private Texture2D _texture_plus;
        private Texture2D _texture_minus;

        public new Vector2 Position
        {
            get { return base.Position; }

            set
            {
                base.Position = value;
                _bounds = new Rectangle((int)base.Position.X, (int)base.Position.Y, _texture_plus.Width + _texture_minus.Width, _texture_plus.Height + _texture_minus.Height);
                _bounds_plus = new Rectangle((int)base.Position.X, (int)base.Position.Y + _texture_plus.Height, _texture_plus.Width, _texture_plus.Height);
                _bounds_minus = new Rectangle((int)base.Position.X, (int)base.Position.Y, _texture_minus.Width, _texture_minus.Height);
            }
        }

        public StatControl(Texture2D texture_plus, Texture2D texture_minus, Vector2 position, string text)
            : base(position)
        {
            base.Text = text;
            _texture_plus = texture_plus;
            _texture_minus = texture_minus;
            _texture_plus = texture_plus;
            _texture_minus = texture_minus;
            _bounds = new Rectangle((int)base.Position.X, (int)base.Position.Y, _texture_plus.Width, _texture_plus.Height + _texture_minus.Height);
            _bounds_plus = new Rectangle((int)base.Position.X, (int)base.Position.Y, _texture_plus.Width, _texture_plus.Height);
            _bounds_minus = new Rectangle((int)base.Position.X, (int)base.Position.Y + texture_plus.Height, _texture_minus.Width, _texture_minus.Height);
        }

        public override void UpdateInput(InputState input)
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (Enabled)
            {
                spriteBatch.Draw(_texture_plus, _bounds_plus, Color);
                spriteBatch.Draw(_texture_minus, _bounds_minus, Color);

                if (Font != null)
                {
                    DrawCenteredText(spriteBatch, Font, _bounds, Text, Color);
                }
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
