using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class MapGenerator
    {
        private Int32 _mobcountBase = 140;
        private Double _timeBase = 750;
        private List<Double> _probs;
        private Int32 _level;
        private Double _ellapsedTime;
        private Int32 _mobsOnLevel;
        public event EventHandler<EventArgs> LevelUp;

        public MapGenerator()
        {
            _level = 1;
            _probs = new List<Double>();
            LevelInit();
            _ellapsedTime = 0;
            _mobsOnLevel = 0;
        }
        public Int32 level { get { return _level; } }

        private Double Probablity(EnemyKind kind)
        {
            Double result = 0.0;
            switch (kind)
            {
                //lineáris csökken, 90->0
                case EnemyKind.Small:
                    result = 100 - (10 * _level);
                    break;
                //másik kettő alapján a maradék valószínűség, az 5. körig nő, majd csökken, 10->26->0
                case EnemyKind.Medium:
                    result = (10-_level) * _level + 1;
                    break;
                //exponenciálisan nő, 0->99
                case EnemyKind.Big:
                    result = (_level*_level) - 1;
                    break;
            }

            return result/100;
        }

        private Int32 MaxCountOfMobsOfLevel()
        {
            return _mobcountBase + _level * 10;
        }

        private Double SpawnTime()
        {
            return _timeBase - (_level * 50);
        }

        public EnemyKind NewEnemyType()
        {
            Random rand = new Random(DateTime.Now.Millisecond);
            Double r = rand.NextDouble();
            Double sum = 0;
            for (Int32 i = _probs.Count-1; i >= 0; --i)
            {
                sum += _probs[i];
                if (r <= sum)
                {
                    ++_mobsOnLevel;
                    _ellapsedTime = 0;
                    return (EnemyKind)i;
                }
            }
            ++_mobsOnLevel;
            _ellapsedTime = 0;
            return EnemyKind.Small;
        }

        public Boolean AbleToCreateNewEnemy(Double ellapsedtime)
        {
            if (_mobsOnLevel < MaxCountOfMobsOfLevel())
            {
                _ellapsedTime += ellapsedtime;
                return _ellapsedTime >= SpawnTime();
            }
            else
            {
                _ellapsedTime = 0;
                ++_level;
                LevelInit();
                LevelUp(this, new EventArgs());
            }
            return false;
        }

        private void LevelInit()
        {
            _probs.Clear();
            _probs.Add(Probablity(EnemyKind.Small));
            _probs.Add(Probablity(EnemyKind.Medium));
            _probs.Add(Probablity(EnemyKind.Big));
        }
    }
}
