﻿using System;
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
        private Texture2D _texture_plus;
        private Texture2D _texture_minus;
        private FileManager _fm;
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

        public ShopPlayer(Texture2D texture, Texture2D texture_plus, Texture2D texture_minus, Vector2 position, string text, Pilot pilot)
            : base(position)
        {
            base.Text = text;
            _texture = texture;
            _fm = new FileManager();
            _pilot = pilot;
            _bounds = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
            _texture_plus = texture_plus;
            _texture_minus = texture_minus;
            _controls = new List<Control>();

            StatControl _testControl = new StatControl(_texture_plus, _texture_minus, new Vector2(100, 100), "STAT");
            _controls.Add(_testControl);
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

        public static void DrawListText(SpriteBatch batch, SpriteFont font, Rectangle rectangle, Color color, int rank, string name, int score)
        {
            var size = font.MeasureString(name);
            var textWidth = size.X;
            var left = rectangle.Left + 10;
            var top = rectangle.Top + rank * 40 + 45;
            var topLeft = new Vector2(left, top);
            batch.DrawString(font, rank + ".  " + name, topLeft, color);
            batch.DrawString(font, score.ToString(), new Vector2(topLeft.X + 400, topLeft.Y), color);
        }
    }
}