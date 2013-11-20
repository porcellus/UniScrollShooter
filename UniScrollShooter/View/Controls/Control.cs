using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using View.ScreenManagement;

namespace View.Controls
{
    public class Control
    {
        #region Fields
        string text;

        string name;

        bool enabled;

        Color color;

        Vector2 position;

        Vector2 size;

        SpriteFont font;
        #endregion

        #region Properties
        public string Text
        {

            get { return text; }

            set { text = value; }

        }

        public string Name
        {

            get { return name; }

            set { name = value; }

        }

        public bool Enabled
        {

            get { return enabled; }

            set { enabled = value; }

        }

        public Color Color
        {

            get { return color; }

            set { color = value; }

        }

        public Vector2 Position
        {

            get { return position; }

            set { position = value; }

        }

        public Vector2 Size
        {

            get { return size; }

            set { size = value; }

        }

        public SpriteFont Font
        {

            get { return font; }

            set { font = value; }

        }
        #endregion

        public Control() : this(Vector2.Zero)
        {
        }

        public Control(Vector2 position)
        {
            Position = position;
            text = " ";
            name = " ";
            enabled = true;
            color = Color.White;
        }

        public virtual void Update(double elapsedTime)
        {
        }

        public virtual void UpdateInput(InputState input)
        {
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
        }
    }
}
