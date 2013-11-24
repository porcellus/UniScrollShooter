using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using Data.FixedReferences;
using Microsoft.Xna.Framework;
using Data;

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
        private Pilot _pilot;
        private List<Enemy> enemies;

        public void Start()
        {
            var t = new Timer(Heartbeat, null, 0, 10);
            var nState = new GameState {PlayerPosition = new Vector2(5,20)};

            CurrState = nState;

            _pilot = new Pilot();
            _pilot.Init(10.0, 10.0, 50, 100);
            
            enemies = new List<Enemy>();
            ///*pl új ellenség hozzáadása*/enemies.Add(new Enemy(0, 100, 100, 30, 20));
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

            _pilot.setPosition(_pilot.ship.posX + dx, _pilot.ship.posY + dy);
            Update();
            CollisionCheck();
        }

        public void Update()
        {
            for (int i = enemies.Count - 1; i >= 0; i--)
            {
                enemies[i].Move();
                // TODO: ellenőrzés pályán vagyunk-e még
            }
            if (_pilot.ship.health <= 0)
            {
                //valami
            }
        }

        public void CollisionCheck()
        {
            //a hajónk és az ellenségek ütközésének ellenőrzése
            Rectangle rectangle1;
            Rectangle rectangle2;

            rectangle1 = new Rectangle((int)_pilot.ship.posX,
            (int)_pilot.ship.posY,
            _pilot.ship.width,
            _pilot.ship.height);

            for (int i = 0; i < enemies.Count; i++)
            {
                rectangle2 = new Rectangle((int)enemies[i].PosX,
                (int)enemies[i].PosY,
                enemies[i].Width,
                enemies[i].Height);

                if (rectangle1.Intersects(rectangle2))
                {
                    _pilot.ship.DoDamage(enemies[i].type.Damage);
                    enemies[i].health = 0;
                    enemies.RemoveAt(i);
                    _pilot.exp += enemies[i].type.Value;

                    if (_pilot.ship.health <= 0)
                    { // valami 
                    }
                }
            }
        }

    }
}