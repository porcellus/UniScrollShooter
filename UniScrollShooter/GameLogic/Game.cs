﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Collections.Concurrent;
using System.Threading;
using Data.FixedReferences;
using Microsoft.Xna.Framework;
using Data;

namespace GameLogic
{
    public enum GameEventType
    {
        LaserFired, EnemyDestroyed,
    }

    public struct Input
    {
        public Vector2 InputPos;

        public bool FirePressed { get; set; }
    }

    public class Game: IDisposable
    {
        public GameState CurrState { get; private set; }
        public Input Input { get; set; }
        private Pilot _pilot;
        public List<Enemy> enemies;
        public List<Bullet> bullets;
        private double _cooldown; // nem itt kene legyen
        private Random _rnd;
        private Thread _gameThread;
        private int _exiting;
        private bool _paused;
        public ConcurrentQueue<GameEventType> Events { get; private set; }
        
        //TODO(nektek): kellene, hogy megkapjam startban paramétereknél a spritek méretét
        //TODO: pálya létrehozása kezelése(majd)

        public void Start()
        {
            _pilot = new Pilot();
            _pilot.Init(10.0, 10.0, 172, 54);

            enemies = new List<Enemy>();
            bullets = new List<Bullet>();

            _cooldown = 0;
            _rnd = new Random();

            var nState = new GameState {PlayerPosition = new Vector2(5,20)};

            Events = new ConcurrentQueue<GameEventType>();

            CurrState = nState;

            _gameThread = new Thread(GameLoop);
            _gameThread.IsBackground = true;
            _gameThread.Start();

            _exiting = 0;
        }

        private void GameLoop()
        {
            var lastTime = DateTime.Now;
            while (_exiting == 0)
            {
                var elapsedTime = (DateTime.Now - lastTime).TotalMilliseconds;
                lastTime = DateTime.Now;
                if (!_paused)
                {
                    var dx = (Input.InputPos.X - CurrState.PlayerPosition.X)/1000* elapsedTime;
                    var dy = (Input.InputPos.Y - CurrState.PlayerPosition.Y)/1000* elapsedTime;

                    var nState = new GameState
                        {
                            PlayerPosition =
                                new Vector2((float) (CurrState.PlayerPosition.X + dx), (float) (CurrState.PlayerPosition.Y + dy)),
                        };

                    if (_cooldown > 0) _cooldown -= elapsedTime;
                    else if (Input.FirePressed)
                    {
                        CreateNewBullet();
                        Events.Enqueue(GameEventType.LaserFired);
                        _cooldown = 250;
                    }

                    _pilot.setPosition(_pilot.ship.posX + dx, _pilot.ship.posY + dy);

                    if (_rnd.Next(0, 100000) == 1 && enemies.Count(enemy => enemy.posX > 1500) < 10)
                    {
                        CreateNewEnemy();
                    }
                    Update(nState, elapsedTime);
                    CollisionCheck();

                    CurrState = nState;
                }
            }
            ++_exiting;
        }

        #region Update
        /// <summary>
        /// Az ellenségek és lövedékek mozgatása, törlése, stb
        /// </summary>
        public void Update(GameState nState, double elapsedTime)
        {
            for (int i = enemies.Count - 1; i >= 0; i--)
            {
                if (enemies[i].health <= 0)
                {
                    enemies.RemoveAt(i);
                    Events.Enqueue(GameEventType.EnemyDestroyed);
                } else
                    enemies[i].Move(elapsedTime);
                // TODO: ellenőrzés pályán vagyunk-e még
            }
            for (int i = 0; i < bullets.Count; ++i)
            {
                if (!bullets[i].active)
                    bullets.RemoveAt(i);
                else
                    bullets[i].Move(elapsedTime);
                // TODO: ellenőrzés pályán vagyunk-e még
            }
            if (_pilot.ship.health <= 0)
            {
                //Exception dobás lessz
            }
        }
        #endregion

        #region CollisionCheck
        /// <summary>
        /// Ütközések ellenőrzése, élet, pontok, aktivitás beállítása
        /// </summary>
        public void CollisionCheck()
        {
            //a hajónk és az ellenségek ütközésének ellenőrzése
            Rectangle rectangle1;
            Rectangle rectangle2;

            rectangle1 = new Rectangle((int)_pilot.ship.posX,
            (int)_pilot.ship.posY,
            _pilot.ship.width,
            _pilot.ship.height);
            #region ellenség vs mi ütközés
            for (int i = 0; i < enemies.Count; i++)
            {
                rectangle2 = new Rectangle((int)enemies[i].posX,
                (int)enemies[i].posY,
                enemies[i].width,
                enemies[i].height);

                if (rectangle1.Intersects(rectangle2))
                {
                    _pilot.ship.DoDamage(enemies[i].type.damage);
                    enemies[i].health = 0;
                    _pilot.exp += enemies[i].type.value;

                    if (_pilot.ship.health <= 0)
                    { // valami 
                    }
                }
            }
            #endregion

            #region ellenség vs lövedék ütközés
            for (int i = 0; i < bullets.Count; i++)
            {
                for (int j = 0; j < enemies.Count; j++)
                {
                    rectangle1 = new Rectangle((int)bullets[i].posX -
                    bullets[i].width / 2, (int)bullets[i].posY -
                    bullets[i].height / 2, bullets[i].width, bullets[i].height);

                    rectangle2 = new Rectangle((int)enemies[j].posX - enemies[j].width / 2,
                    (int)enemies[j].posY - enemies[j].height / 2,
                    enemies[j].width, enemies[j].height);

                    if (rectangle1.Intersects(rectangle2))
                    {
                        enemies[j].health -= bullets[i].damage;
                        bullets[i].active = false;
                    }
                }
            }
            #endregion

        }
        #endregion

        #region Create*
        //új lövedék létrehozása, pl.: kattintásra kellene szerintem
        public void CreateNewBullet()
        {
            //x,y koordináták(pilot elé teszi), méretei, sebzés mértéke(a pilot hajójából)
            bullets.Add(new Bullet(_pilot.ship.posX + _pilot.ship.width, _pilot.ship.posY + _pilot.ship.height / 2f, 65, 21, _pilot.ship.damage));
            Events.Enqueue(GameEventType.LaserFired);

        }

        //új ellenség
        public void CreateNewEnemy()
        {
            //típus, koordináták, méretek
            enemies.Add(new Enemy(0, 2000, _rnd.Next(100,700), 128, 61));
        }
        #endregion

        public void Dispose()
        {
            _exiting = 1;
        }
    }
}