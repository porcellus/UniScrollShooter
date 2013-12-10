#region File Description
//-----------------------------------------------------------------------------
// BackgroundScreen.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using GameLogic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using View.Controls;
using View.ScreenManagement;
using Data;

#endregion

namespace View.Screens
{
    /// <summary>
    /// The background screen sits behind all the other menu screens.
    /// It draws a background image that remains fixed in place regardless
    /// of whatever transitions the screens on top of it may be doing.
    /// </summary>
    class GameScreen: Screen
    {
        #region Fields

        ContentManager _content;

        Texture2D _shipTexture;
        private Texture2D _starTexture;
        private List<Vector2> _stars; // a hatterben elrepulo csillagok
        private GameLogic.Game _game;
        private Pilot _pilot;

        private Timer _musicTimer;
        private List<string> _bgMusicList;
        private int _bgMusicIndex;

        #endregion

        #region Initialization


        /// <summary>
        /// Constructor.
        /// </summary>
        public GameScreen(Pilot pilot)
        {
            TransitionOnTime = TimeSpan.FromSeconds(0.5);
            TransitionOffTime = TimeSpan.FromSeconds(0.5);
            _stars = new List<Vector2>();
            _pilot = pilot;
        }


        /// <summary>
        /// Loads graphics content for this screen. 
        /// </summary>
        public override void LoadContent()
        {
            if (_content == null)
                _content = new ContentManager(ScreenManager.Game.Services, "Content");

            Viewport viewport = ScreenManager.GraphicsDevice.Viewport;
            var fullscreen = new Rectangle(0, 0, viewport.Width, viewport.Height);

            _shipTexture = _content.Load<Texture2D>("spaceship");
            _starTexture = _content.Load<Texture2D>("star1");
            _content.Load<SoundEffect>("Sounds/laser");

            _game = new GameLogic.Game(_pilot); 

            var rnd = new Random();
            for (int i = 0; i < 100; ++i)
                _stars.Add(new Vector2(rnd.Next(0, 125)/100f, rnd.Next(0, 100)/100f));
            _game.Start();

            _bgMusicList = new List<string> {"Sounds/01"};
            _bgMusicIndex = 0;
            /*
            _musicTimer = new Timer(state =>
            {
                                                _bgMusicIndex = (_bgMusicIndex + 1) % _bgMusicList.Count;
                                                _content.Load<SoundEffect>(_bgMusicList[_bgMusicIndex]).Play();
                                                _musicTimer.Change((int)_content.Load<SoundEffect>(_bgMusicList[_bgMusicIndex]).Duration.TotalMilliseconds,
                                                               (int)_content.Load<SoundEffect>(_bgMusicList[_bgMusicIndex]).Duration.TotalMilliseconds);
            }, null, 0, (int)_content.Load<SoundEffect>(_bgMusicList[_bgMusicIndex]).Duration.TotalMilliseconds);
             */
        }


        /// <summary>
        /// Unloads graphics content for this screen.
        /// </summary>  
        public override void UnloadContent()
        {
            _content.Unload();
        }

        #endregion

        #region Update and Draw

        /// <summary>
        /// Updates background objects
        /// </summary>
        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
 	        base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);

            for (int i = 0; i < _stars.Count; ++i)
                _stars[i] = new Vector2((float) (_stars[i].X - gameTime.ElapsedGameTime.TotalSeconds/10), _stars[i].Y);

            _stars.RemoveAll(a => a.X < 0);
            var rnd = new Random();
            if (_stars.Count < 150 && rnd.Next(0, 100) > 70)
            {
                _stars.Add(new Vector2(1 + rnd.Next(0, 25) / 100f, rnd.Next(0, 100) / 100f));
            }

            GameEventType ev;
            while (_game.Events.TryDequeue(out ev))
            {
                switch (ev)
                {
                    case GameEventType.LaserFired:
                        //_content.Load<SoundEffect>("Sounds/laser").Play();
                        break;
                    case GameEventType.EnemyDestroyed:
                        //_content.Load<SoundEffect>("Sounds/explosion_small").Play();
                        break;
                    case GameEventType.LevelEnd:
                        ScreenManager.AddScreen(new ShopScreen(_pilot), ControllingPlayer);
                        ScreenManager.RemoveScreen(this);
                        return;
                    case GameEventType.PlayerDead:
                        GetPlayerScreen gScreen = new GetPlayerScreen();
                        gScreen.Score = _game.pilot.Score;

                        ScreenManager.AddScreen(gScreen, ControllingPlayer);
                        ScreenManager.RemoveScreen(this);   
                        return;
                }
            }
        }

        /// <summary>
        /// Handles input and passes it on to it's controls.
        /// </summary>
        public override void HandleInput(InputState input)
        {
            base.HandleInput(input);

            _game.Input =
                new Input
                {
                    InputPos = new Vector2(input.MouseState.X, input.MouseState.Y),
                    FirePressed = input.CurrentKeyboardStates[0].IsKeyDown(Keys.Space) ||
                                  input.MouseState.LeftButton == ButtonState.Pressed
                };
            if (input.CurrentKeyboardStates[0].IsKeyDown(Keys.P) &&
                !input.LastKeyboardStates[0].IsKeyDown(Keys.P))
            {
                //_game.TogglePause();
            }

            if (input.CurrentKeyboardStates[0].IsKeyDown(Keys.Escape) &&
                !input.LastKeyboardStates[0].IsKeyDown(Keys.Escape))
            {
                //_game.Stop();
                _game.Dispose();
                ScreenManager.RemoveScreen(this);
                ScreenManager.AddScreen(new GameMenu(), ControllingPlayer);
            }
        }

        private static void drawCentered(SpriteBatch spriteBatch, Texture2D texture, Vector2 pos, float rotation = 0, float scale = 1)
        {
            var cx = texture.Width/2;
            var cy = texture.Height/2;

            var cpos = new Vector2(pos.X - cx, pos.Y - cy);

            spriteBatch.Draw(texture, cpos, texture.Bounds, Color.White, rotation, new Vector2(0,0), scale, SpriteEffects.None, 0);
        }

        private DateTime lastDraw;

        /// <summary>
        /// Draws the screen calling every controls Draw.
        /// </summary>
        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;
            Viewport viewport = ScreenManager.GraphicsDevice.Viewport;
            var fullscreen = new Rectangle(0, 0, viewport.Width, viewport.Height);

            spriteBatch.Begin();
            //var blankSp = _content.Load<Texture2D>("blank");
            var enemySpSmall = _content.Load<Texture2D>("enemy_spaceship");
            var enemySpMedium = _content.Load<Texture2D>("alien2");
            var enemySpBig = _content.Load<Texture2D>("alien1");
            var lsRedSp = _content.Load<Texture2D>("laserbeam_red");
            var lsBlueSp = _content.Load<Texture2D>("laserbeam_blue");
            //spriteBatch.Draw(blankSp, fullscreen, Color.Black);

            foreach (var star in _stars)
                spriteBatch.Draw(_starTexture, new Vector2(star.X*fullscreen.Width, star.Y*fullscreen.Height),
                                 Color.White);

            foreach (var enemy in _game.enemies.Where(en => en.PosX > 0 && en.PosY > 0 && en.PosX < fullscreen.Width && en.PosY < fullscreen.Height).ToList())
            {
                if(enemy.Type == Data.FixedReferences.EnemyType.Small)
                    drawCentered(spriteBatch, enemySpSmall, new Vector2((float)enemy.PosX, (float)enemy.PosY));
                else if (enemy.Type == Data.FixedReferences.EnemyType.Medium)
                    drawCentered(spriteBatch, enemySpMedium, new Vector2((float)enemy.PosX, (float)enemy.PosY), 0, 1.5f);
                else if (enemy.Type == Data.FixedReferences.EnemyType.Big)
                    drawCentered(spriteBatch, enemySpBig, new Vector2((float)enemy.PosX, (float)enemy.PosY));
            }


            foreach (var bullet in _game.bullets.Where(bul => bul.PosX > 0 && bul.PosY > 0 && bul.PosX < fullscreen.Width && bul.PosY < fullscreen.Height).ToList())
                drawCentered(spriteBatch, lsRedSp, new Vector2((float)bullet.PosX, (float)bullet.PosY),
                                    bullet.vy > 0 ? (float) bullet.vx / (float) bullet.vy : 0);

            //tulajdons�g ki�r�sok (�let, pajzs, tapasztalat, p�nz, szint)
            var s = _content.Load<SpriteFont>("menufont");
            spriteBatch.DrawString(s, "Health: " + _game.pilot.Health, new Vector2(viewport.TitleSafeArea.X, viewport.TitleSafeArea.Y), Color.White);
            spriteBatch.DrawString(s, "Shield: " + _game.pilot.Shield, new Vector2(viewport.TitleSafeArea.X, viewport.TitleSafeArea.Y + 30), Color.White);

            spriteBatch.DrawString(s, "Experience: " + _game.pilot.Exp, new Vector2(viewport.TitleSafeArea.X, viewport.TitleSafeArea.Y + viewport.TitleSafeArea.Height - 40), Color.Gray);
            spriteBatch.DrawString(s, "Money: " + _game.pilot.Money, new Vector2(viewport.TitleSafeArea.X + viewport.TitleSafeArea.Width / 2, viewport.TitleSafeArea.Y + viewport.TitleSafeArea.Height - 40), Color.Gray);

            spriteBatch.DrawString(s, "Level: " + _game.level, new Vector2(viewport.TitleSafeArea.X + viewport.TitleSafeArea.Width / 2, viewport.TitleSafeArea.Y), Color.WhiteSmoke);
            //

            var shipCenter = new Vector2(_shipTexture.Width / 2f, _shipTexture.Height / 2f);
            spriteBatch.Draw(_shipTexture, _game.CurrState.PlayerPosition - shipCenter, Color.White);
            spriteBatch.End();
        }

        #endregion
    }
}
