using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using Data.FixedReferences;
using Microsoft.Xna.Framework;

namespace GameLogic
{
    public struct Input
    {
        public Vector2 InputPos;
    }

    public class Game
    {
        public GameState CurrState { get; private set; }
        public Input Input { get; set; }


        public void Start()
        {
            var t = new Timer(Heartbeat, null, 0, 10);
            var nState = new GameState {PlayerPosition = new Vector2(5,20)};

            CurrState = nState;
        }

        private void Heartbeat(object state)
        {
            var dx = (Input.InputPos.X - CurrState.PlayerPosition.X)/100;
            var dy = (Input.InputPos.Y - CurrState.PlayerPosition.Y)/100;

            var nState = new GameState
                {
                    PlayerPosition = new Vector2(CurrState.PlayerPosition.X + dx, CurrState.PlayerPosition.Y + dy)
                };
            CurrState = nState;
        }
    }
}