using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace GameLogic
{
    public class GameState
    {

        //public Data.Pilot Pilot { get; private set; }
        public Vector2 PlayerPosition { get; set; }

        
        public GameState()//Data.Pilot pilot)
        {
            ;
        }

        public GameState(GameState gameState)
        {
            //Pilot = gameState.Pilot;
            PlayerPosition = gameState.PlayerPosition;
        }
    }
}
