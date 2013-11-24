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
    class Button : Control
    {
        public enum ButtonStatus { Up, Down}
        #region Fields
        Texture2D texture_on;
        Texture2D texture_off;
        Rectangle bounds;
        ButtonStatus _status;
        MouseState _state;
        #endregion

        public ButtonStatus Status
        {
            get { return _status; }
            set { _status = value; }
        }

        // Gets fired when the button is clicked.
        public event EventHandler Clicked;

        public new Vector2 Position
        {
            get { return base.Position; }

            set
            {
                base.Position = value;
                
                bounds = new Rectangle((int)base.Position.X, (int)base.Position.Y, texture_off.Width, texture_off.Height);
            }
        }

        public Button(Texture2D texture_on, Texture2D texture_off, Vector2 position, string text) : base(position)
        {
            base.Text = text;
            this.texture_on = texture_on;
            this.texture_off = texture_off;
            this.Status = ButtonStatus.Up;

            bounds = new Rectangle((int)position.X, (int)position.Y, texture_off.Width, texture_off.Height);
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
                        if (Clicked != null)
                        {
                            // Fire the clicked event.        
                            Clicked(this, EventArgs.Empty);
                        }
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

        protected bool ContainsPos(Vector2 pos)
        {
            return bounds.Contains((int)pos.X, (int)pos.Y);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (Enabled)
            {
                if (ContainsPos(new Vector2(_state.X, _state.Y)))
                {
                    spriteBatch.Draw(texture_on, bounds, Color);
                }
                else
                {
                    spriteBatch.Draw(texture_off, bounds, Color);
                }

                if (Font != null)
                {
                    DrawCenteredText(spriteBatch, Font, bounds, Text, Color);
                }
            }

        }

        public static void DrawCenteredText(SpriteBatch batch, SpriteFont font, Rectangle rectangle, string text, Color color)
        {
            var size = font.MeasureString(text);
            var textWidth = text.Count() * 17;
            var left = rectangle.Left + (rectangle.Width - textWidth) / 2;
            var top = rectangle.Top + 20;
            var topLeft = new Vector2(left, top);
            batch.DrawString(font, text, topLeft, color);
        }
    }
}
