using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Data.FixedReferences;

namespace Data
{
    public class MapGenerator
    {
        private Int32 _mobcountBase = 14;
        private Double _timeBase = 750;
        private Dictionary<EnemyType,Double> _probs;
        private Int32 _level;
        private Double _ellapsedTime;
        private Int32 _mobsOnLevel;
        public event EventHandler<EventArgs> LevelUp;

        public MapGenerator(Int32 level)
        {
            _level = level;
            _probs = new Dictionary<EnemyType, double>();
            LevelInit();
            _ellapsedTime = 0;
            _mobsOnLevel = 0;
        }
        public Int32 level { get { return _level; } }

        private Double Probablity(EnemyType kind)
        {
            Double result = 0.0;
            //lineáris csökken, 90->0
            if(kind==EnemyType.Small)
                result = 100 - (10 * _level);
            //másik kettő alapján a maradék valószínűség, az 5. körig nő, majd csökken, 10->26->0
            if(kind==EnemyType.Medium)
                result = (10-_level) * _level + 1;
            //exponenciálisan nő, 0->99
            if(kind==EnemyType.Big)
                result = (_level*_level) - 1;

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

        public EnemyType NewEnemyType()
        {
            var rand = new Random(DateTime.Now.Millisecond);
            var r = rand.NextDouble();
            Double sum = 0;
            
            foreach(var prob in _probs)
            {
                sum += prob.Value;
                if (r <= sum)
                {
                    ++_mobsOnLevel;
                    _ellapsedTime = 0;
                    return prob.Key;
                }
            }
            ++_mobsOnLevel;
            _ellapsedTime = 0;
            return EnemyType.Small;
        }

        public Boolean AbleToCreateNewEnemy(Double ellapsedtime)
        {
            if (_mobsOnLevel < MaxCountOfMobsOfLevel())
            {
                _ellapsedTime += ellapsedtime;
                return _ellapsedTime >= SpawnTime();
            }

            _ellapsedTime = 0;
            LevelInit();
            LevelUp(this, new EventArgs());
            return false;
        }

        private void LevelInit()
        {
            _probs.Clear();
            _probs.Add(EnemyType.Small,Probablity(EnemyType.Small));
            _probs.Add(EnemyType.Medium,Probablity(EnemyType.Medium));
            _probs.Add(EnemyType.Big,Probablity(EnemyType.Big));
        }
    }
}
