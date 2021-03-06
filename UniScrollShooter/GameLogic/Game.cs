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
        LaserFired, EnemyDestroyed, LevelEnd, PlayerDead
    }

    public struct GameEventData
    {
        public DateTime time;
        public Vector2 pos;
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
        private MapGenerator _mapGen;
        private Boolean _waitForLevelEnd;
        public ConcurrentQueue<KeyValuePair<GameEventType,GameEventData>> Events { get; private set; }

        public Game(Pilot pilot)
        {
            _pilot = pilot;
        }

        public void Start()
        {
            enemies = new List<Enemy>();
            bullets = new List<Bullet>();

            _mapGen = new MapGenerator(_pilot.level);
            _mapGen.LevelUp += new EventHandler<EventArgs>(LevelUpHandler);

            _cooldown = 0;
            _rnd = new Random();

            var nState = new GameState {PlayerPosition = new Vector2(5,20)};

            Events = new ConcurrentQueue<KeyValuePair<GameEventType, GameEventData>>();

            CurrState = nState;
            _waitForLevelEnd = false;

            _pilot.SetPosition(Input.InputPos.X, Input.InputPos.Y);

            _gameThread = new Thread(GameLoop);
            _gameThread.IsBackground = true;
            _gameThread.Start();
    
            _exiting = 0;
        }

        public void ContinueGame()
        {
            _waitForLevelEnd = false;
            _exiting = 0;
        }

        private void LevelUpHandler(object sender, EventArgs e)
        {
            _waitForLevelEnd = true;
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
                        Events.Enqueue(new KeyValuePair<GameEventType, GameEventData>(GameEventType.LaserFired, new GameEventData{ time = DateTime.Now}));
                        _cooldown = 250;
                    }

                    _pilot.SetPosition(_pilot.PosX + dx, _pilot.PosY + dy);

                    /*if (_rnd.Next(0, 100000) == 1 && enemies.Count(enemy => enemy.posX > 1500) < 10)
                    {
                        CreateNewEnemy();
                    }*/
                    if (_mapGen.AbleToCreateNewEnemy(elapsedTime) /*&& enemies.Count(enemy => enemy.posX < 1500) < 4*/ && !_waitForLevelEnd)
                    {
                        CreateNewEnemy(_mapGen.NewEnemyType());
                    }
                    else { 
                        if (enemies.Count != 0) 
                        { } }
                    Update(nState, elapsedTime);
                    CollisionCheck();

                    CurrState = nState;
                }
            }
            //++_exiting;
        }

        #region Update
        /// <summary>
        /// Az ellenségek és lövedékek mozgatása, törlése, stb
        /// </summary>
        private void Update(GameState nState, double elapsedTime)
        {
            for (int i = enemies.Count - 1; i >= 0; i--)
            {
                if (enemies[i].Health <= 0)
                {
                    //akkor kapunk ha lelőttünk egy ellenséget
                    _pilot.Money += enemies[i].value;
                    Events.Enqueue(new KeyValuePair<GameEventType, GameEventData>(GameEventType.EnemyDestroyed, 
                                new GameEventData { time = DateTime.Now, pos = new Vector2((float) enemies[i].PosX,(float) enemies[i].PosY)}));
                    enemies.RemoveAt(i);
                } else
                {
                    enemies[i].Move(elapsedTime);
                    if (enemies[i].PosX < 0)
                        enemies.RemoveAt(i);
                }
                // TODO: ellenőrzés pályán vagyunk-e még
            }
            for (int i = 0; i < bullets.Count; ++i)
            { 
                if (!bullets[i].active || bullets[i].PosX>2000)
                    bullets.RemoveAt(i);
                else
                    bullets[i].Move(elapsedTime);
                // TODO: ellenőrzés pályán vagyunk-e még
            }
            //játékos vége
            if (_pilot.Health <= 0)
            {
                enemies.Clear();
                bullets.Clear();
                Events.Enqueue(new KeyValuePair<GameEventType, GameEventData>(GameEventType.PlayerDead, new GameEventData { time = DateTime.Now }));
                _exiting++;
            }
            //pálya vége
            if (enemies.Count == 0 && _waitForLevelEnd)
            {
                enemies.Clear();
                bullets.Clear();
                _exiting++;
                if (_pilot.level != 10)
                {
                    ++_pilot.level;
                    Events.Enqueue(new KeyValuePair<GameEventType, GameEventData>(GameEventType.LevelEnd, new GameEventData { time = DateTime.Now }));
                } else
                    Events.Enqueue(new KeyValuePair<GameEventType, GameEventData>(GameEventType.PlayerDead, new GameEventData { time = DateTime.Now }));
            }
            
        }
        #endregion

        #region CollisionCheck
        /// <summary>
        /// Ütközések ellenőrzése, élet, pontok, aktivitás beállítása
        /// </summary>
        private void CollisionCheck()
        {
            //a hajónk és az ellenségek ütközésének ellenőrzése
            Rectangle rectangle1;
            Rectangle rectangle2;

            rectangle1 = new Rectangle((int)_pilot.PosX - _pilot.Width/2,
            (int)_pilot.PosY-_pilot.Height/2,
            _pilot.Width,
            _pilot.Height);
            #region ellenség vs mi ütközés
            for (int i = 0; i < enemies.Count; i++)
            {
                rectangle2 = new Rectangle((int)enemies[i].PosX,
                (int)enemies[i].PosY,
                enemies[i].Width,
                enemies[i].Height);

                if (rectangle1.Intersects(rectangle2))
                {
                    _pilot.DamageOnShip(enemies[i].Damage);
                    enemies[i].Health = 0;
                }
            }
            #endregion

            #region ellenség vs lövedék ütközés
            for (int i = 0; i < bullets.Count; i++)
            {
                for (int j = 0; j < enemies.Count; j++)
                {
                    rectangle1 = new Rectangle(
                        (int)bullets[i].PosX - bullets[i].Width / 2,
                        (int)bullets[i].PosY - bullets[i].Height / 2,
                        bullets[i].Width, bullets[i].Height);

                    rectangle2 = new Rectangle(
                        (int)enemies[j].PosX - enemies[j].Width / 2,
                        (int)enemies[j].PosY - enemies[j].Height / 2,
                        enemies[j].Width, enemies[j].Height);

                    if (rectangle1.Intersects(rectangle2))
                    {
                        enemies[j].Health -= bullets[i].Damage;
                        bullets[i].active = false;
                        //akkor kapunk tapasztalatot, ha eltaláltunk egy ellenséget
                        _pilot.Exp += 1;
                    }
                }
            }
            #endregion

        }
        #endregion

        #region Create*
        //új lövedék létrehozása, pl.: kattintásra kellene szerintem
        private void CreateNewBullet()
        {
            //x,y koordináták(pilot elé teszi), méretei, sebzés mértéke(a pilot hajójából)
            bullets.Add(new Bullet(_pilot.PosX, _pilot.PosY + _pilot.Height / 2f - 12, 65, 21, _pilot.Damage, _pilot.BulletKind));
            Events.Enqueue(new KeyValuePair<GameEventType, GameEventData>(GameEventType.LaserFired, new GameEventData { time = DateTime.Now }));

        }

        //új ellenség
        private void CreateNewEnemy(EnemyType type)
        {
            //típus, koordináták, méretek
            enemies.Add(new Enemy(2000+_rnd.Next(-100,100), _rnd.Next(100,700), type));
        }
        #endregion

        public void Dispose()
        {
            _exiting = 1;
        }

        public Pilot pilot { get { return _pilot; } }
        public Int32 level { get { return _mapGen.level; } }
    }
}