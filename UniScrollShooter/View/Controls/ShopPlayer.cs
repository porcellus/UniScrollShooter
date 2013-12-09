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
    class ShopPlayer : Control
    {
        private Rectangle _bounds;
        private Texture2D _texture;
        private Texture2D _texture_plus_off;
        private Texture2D _texture_plus_on;
        private Texture2D _texture_minus_off;
        private Texture2D _texture_minus_on;
        private Pilot _pilot;
        private List<Control> _controls;

        public new Vector2 Position
        {
            get { return base.Position; }
            set
            {
                base.Position = value;

                _bounds = new Rectangle((int)base.Position.X, (int)base.Position.Y, _texture.Width, _texture.Height);
            }
        }

        public ShopPlayer(Texture2D texture, Texture2D texture_plus_off, Texture2D texture_plus_on, Texture2D texture_minus_off, Texture2D texture_minus_on, SpriteFont font, Vector2 position, string text, Pilot pilot)
            : base(position)
        {
            base.Text = text;
            base.Font = font;
            _texture = texture;
            _pilot = pilot;
            _bounds = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
            _texture_plus_off = texture_plus_off;
            _texture_plus_on = texture_plus_on;
            _texture_minus_off = texture_minus_off;
            _texture_minus_on = texture_minus_on;
            _controls = new List<Control>();

            StatControl _testControl = new StatControl(
                _texture_plus_off, 
                _texture_plus_on, 
                _texture_minus_off, 
                _texture_minus_on, 
                new Vector2(100, 100), 
                "STAT", 
                100);
            _testControl.Font = this.Font;
            _testControl.PlusClicked += (sender, args) =>
            {
                // Itt a pilóta megfelelő tulajdonságát fogjuk majd növelni.
                _testControl.Value += 1;
            };
            _testControl.MinusClicked += (sender, args) =>
            {
                // Itt a pilóta megfelelő tulajdonságát fogjuk majd csökkenteni.
                _testControl.Value -= 1;
            };
            _controls.Add(_testControl);
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
