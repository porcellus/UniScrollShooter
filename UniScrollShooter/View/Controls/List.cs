using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using View.ScreenManagement;

namespace View.Controls
{
    class List : Control
    {
        private Rectangle _bounds;
        private Texture2D _texture;

        public new Vector2 Position
        {
            get { return base.Position; }

            set
            {
                base.Position = value;

                _bounds = new Rectangle((int)base.Position.X, (int)base.Position.Y, _texture.Width, _texture.Height);
            }
        }

        public List(Texture2D texture, Vector2 position, string text)
            : base(position)
        {
            base.Text = text;
            _texture = texture;
            _bounds = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
        }

        public override void UpdateInput(InputState input)
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (Enabled)
            {
                spriteBatch.Draw(_texture, _bounds, Color);

                if (Font != null)
                {
                    DrawCenteredText(spriteBatch, Font, _bounds, Text, Color);
                    DrawListText(spriteBatch, Font, _bounds, Color, 1, "VALAKI", 5000);
                    DrawListText(spriteBatch, Font, _bounds, Color, 2, "VALAKI", 4000);
                    DrawListText(spriteBatch, Font, _bounds, Color, 3, "VALAKI", 3000);
                    DrawListText(spriteBatch, Font, _bounds, Color, 4, "VALAKI", 2000);
                    DrawListText(spriteBatch, Font, _bounds, Color, 5, "VALAKI", 1000);
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

        public static void DrawListText(SpriteBatch batch, SpriteFont font, Rectangle rectangle, Color color, int rank, string name, int score)
        {
            var size = font.MeasureString(name);
            var textWidth = size.X;
            var left = rectangle.Left + 10;
            var top = rectangle.Top + rank * 40 + 45;
            var topLeft = new Vector2(left, top);
            batch.DrawString(font, rank + ".  " + name, topLeft, color);
            batch.DrawString(font, score.ToString(), new Vector2(topLeft.X + 240, topLeft.Y), color);
        }
    }
}
