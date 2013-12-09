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

            // Damage control.
            StatControl _damageControl = new StatControl(
                _texture_plus_off, 
                _texture_plus_on, 
                _texture_minus_off, 
                _texture_minus_on, 
                new Vector2(base.Position.X + 20, 160), 
                "DAMAGE", 
                _pilot.Damage);
            _damageControl.Font = this.Font;
            _damageControl.PlusClicked += (sender, args) =>
            {
                int price = DamagePrice(_pilot.Damage);
                if (_pilot.Exp - price >= 0)
                {
                    _pilot.Damage += 1;
                    _damageControl.Value += 1;
                    _pilot.Exp -= price;
                }
            };
            _damageControl.MinusClicked += (sender, args) =>
            {
                int price = DamagePrice(_pilot.Damage - 1);
                if (_pilot.Damage > 1)
                {
                    _pilot.Damage -= 1;
                    _damageControl.Value -= 1;
                    _pilot.Exp += price;
                }
            };
            _controls.Add(_damageControl);

            // Health control.
            StatControl _healthControl = new StatControl(
                _texture_plus_off,
                _texture_plus_on,
                _texture_minus_off,
                _texture_minus_on,
                new Vector2(base.Position.X + 20, 240),
                "HEALTH",
                _pilot.Health);
            _healthControl.Font = this.Font;
            _healthControl.PlusClicked += (sender, args) =>
            {
                int price = HealthPrice(_pilot.Health);
                if (_pilot.Exp - price >= 0)
                {
                    _pilot.Health += 1;
                    _healthControl.Value += 1;
                    _pilot.Exp -= price;
                }
            };
            _healthControl.MinusClicked += (sender, args) =>
            {
                int price = HealthPrice(_pilot.Health - 1);
                if (_pilot.Health > 1)
                {
                    _pilot.Health -= 1;
                    _healthControl.Value -= 1;
                    _pilot.Exp += price;
                }
            };
            _controls.Add(_healthControl);

            // Shield control.
            StatControl _shieldControl = new StatControl(
                _texture_plus_off,
                _texture_plus_on,
                _texture_minus_off,
                _texture_minus_on,
                new Vector2(base.Position.X + 20, 320),
                "SHIELD",
                _pilot.Shield);
            _shieldControl.Font = this.Font;
            _shieldControl.PlusClicked += (sender, args) =>
            {
                int price = ShieldPrice(_pilot.Shield);
                if (_pilot.Exp - price >= 0)
                {
                    _pilot.Shield += 1;
                    _shieldControl.Value += 1;
                    _pilot.Exp -= price;
                }
            };
            _shieldControl.MinusClicked += (sender, args) =>
            {
                int price = ShieldPrice(_pilot.Shield - 1);
                if (_pilot.Shield > 0)
                {
                    _pilot.Shield -= 1;
                    _shieldControl.Value -= 1;
                    _pilot.Exp += price;
                }
            };
            _controls.Add(_shieldControl);
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
                    DrawExpRow(spriteBatch, Font, _bounds, Color);
                }

                _controls.ForEach(a => a.Draw(spriteBatch));
            }
        }

        private void DrawCenteredText(SpriteBatch batch, SpriteFont font, Rectangle rectangle, string text, Color color)
        {
            var size = font.MeasureString(text);
            var textWidth = size.X;
            var left = rectangle.Left + (rectangle.Width - textWidth) / 2;
            var top = rectangle.Top + 20;
            var topLeft = new Vector2(left, top);
            batch.DrawString(font, text, topLeft, color);
        }

        private void DrawExpRow(SpriteBatch batch, SpriteFont font, Rectangle rectangle, Color color)
        {
            string textExp = "EXPERIENCE: " + _pilot.Exp;
            var size = font.MeasureString(textExp);
            var textWidth = size.X;
            var textHeight = size.Y;
            var left = rectangle.Left + 20;
            var top = rectangle.Top + 80;
            var topLeft = new Vector2(left, top);
            batch.DrawString(font, textExp, topLeft, color);
        }

        private int DamagePrice(int damage)
        {
            return (damage / 5) * 100;
        }

        private int HealthPrice(int health)
        {
            return (health / 10) * 10;
        }

        private int ShieldPrice(int shield)
        {
            return (shield / 10) * 10;
        }
    }
}
