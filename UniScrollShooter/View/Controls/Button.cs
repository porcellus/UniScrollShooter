using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;
using View.ScreenManagement;

namespace View.Controls
{
    class Button : Control
    {
        public enum ButtonStatus { Up, Down, Clicked}
        #region Fields
        Texture2D texture;
        Texture2D touchOverlay;
        Rectangle bounds;
        ButtonStatus _status = ButtonStatus.Up;
        #endregion

        public ButtonStatus Status
        {
            get { return _status; }
            set { _status = value; }
        }
        // Gets fired when the button is clicked or down

        public event EventHandler Clicked;

        public event EventHandler Down;
        public new Vector2 Position
        {

            get { return base.Position; }

            set
            {

                base.Position = value;

                bounds = new Rectangle((int)base.Position.X, (int)base.Position.Y, texture.Width, texture.Height);

            }
        }

        public Button(Texture2D texture, Texture2D touchedOverlay, Vector2 position, string text): base(position)
        {
            base.Text = text;
            this.texture = texture;
            this.touchOverlay = touchedOverlay;

            bounds = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
        }

        public override void UpdateInput(InputState input)
        {

            if (Enabled)
            {

                foreach (TouchLocation tl in input.TouchState)
                {

                    if (ContainsPos(tl.Position))
                    {

                        if (tl.State == TouchLocationState.Pressed)
                        {

                            Status = ButtonStatus.Clicked;

                            if (Clicked != null)
                            {

                                // Fire the clicked event.        

                                Clicked(this, EventArgs.Empty);

                            }

                        }

                        else
                        {

                            Status = ButtonStatus.Down;

                            if (Down != null)
                            {

                                // Fire the pressed down event.        

                                Down(this, EventArgs.Empty);

                            }

                        }

                    }

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

                spriteBatch.Draw(texture, bounds, Color);

                if (Status == ButtonStatus.Down)
                {

                    spriteBatch.Draw(touchOverlay, bounds, Color);

                }

                if (Font != null)
                {
                    DrawCenteredText(spriteBatch, Font, bounds, Text, Color);
                }

                Status = ButtonStatus.Up;

            }

        }
        public static void DrawCenteredText(SpriteBatch batch, SpriteFont font, Rectangle rectangle, string text, Color color)
        {
            var size = font.MeasureString(text);
            var topLeft = new Vector2(rectangle.Center.X, rectangle.Center.Y - size.Y * 0.5f);
            batch.DrawString(font, text, topLeft, color);
        }
    }
}
