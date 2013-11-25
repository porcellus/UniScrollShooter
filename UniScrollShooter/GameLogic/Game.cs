using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using Data;
using Data.FixedReferences;
using Microsoft.Xna.Framework;

namespace GameLogic
{
    public struct Input
    {
        public Vector2 InputPos;
        public bool PauseToggePressed;
        public bool ExitButtonPressed;
    }

    public class Game : IDisposable
    {
        public GameState CurrState { get; private set; }
        public Input Input { get; set; }

        public event EventHandler GameEnded;
        public bool Paused { get; private set; }

        private Thread _gameLoopThread;
        private int _exiting = 0;

        public void Start()
        {
            //majd azert ide vhonnan venni kell a pilotat.
            var nState = new GameState//(new Pilot())
                                        {PlayerPosition = new Vector2(5,20)};

            CurrState = nState;

            _gameLoopThread = new Thread(GameLoop) {IsBackground = true};
            _gameLoopThread.Start();
        }

        public void Stop()
        {
            _exiting = 1;
        }

        public void Pause()
        {
            Paused = true;
        }

        public void UnPause()
        {
            Paused = false;
        }

        private void GameLoop()
        {
            var now = DateTime.Now;
            var screenPosition = 0;
            while (_exiting == 0)
            {
                var elapsedTime = (float)(DateTime.Now - now).TotalMilliseconds;
                now = DateTime.Now;

                if (Input.ExitButtonPressed) _exiting = 1;
                if (Input.PauseToggePressed) Paused = !Paused;

                if (!Paused&&_exiting == 0)
                {
                    var dx = (Input.InputPos.X - CurrState.PlayerPosition.X)*elapsedTime/1000;
                    var dy = (Input.InputPos.Y - CurrState.PlayerPosition.Y)*elapsedTime/1000;

                    var nState = new GameState(CurrState)
                        {
                            PlayerPosition =
                                new Vector2(CurrState.PlayerPosition.X + dx, CurrState.PlayerPosition.Y + dy)
                        };
                    CurrState = nState;
                }
            }
            _exiting++;
            if (GameEnded != null) GameEnded(this,new EventArgs());
        }

        public void Dispose()
        {
            _exiting = 1;
        }
    }
}