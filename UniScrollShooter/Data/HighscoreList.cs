using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Data
{
    public class HighscoreList
    {
        private List<Entry> _entries = new List<Entry>();

        const string path = "..\\..\\..\\..\\UniScrollShooterContent\\highscores.list";

        public List<Entry> GetEntries()
        {
            return _entries;
        }

        public HighscoreList()
        {
        }

        public void LoadHighscores()
        {
            using (StreamReader reader = new StreamReader(path, true))
            {
                while (reader.Peek() >= 0)
                {
                    string line = reader.ReadLine();
                    char[] separator = { ';' };
                    string[] splitted = line.Split(separator);
                    _entries.Add(new Entry(Convert.ToInt32(splitted[0]), splitted[1], Convert.ToInt32(splitted[2])));
                }
            }
        }

        public void SaveHighscores()
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(path))
            {
                foreach (Entry item in _entries)
                {
                    file.WriteLine(item.Rank.ToString() + ";" + item.Name + ";" + item.Score.ToString());
                }
            }
        }
    }

    public class Entry
    {
        public int Rank { get; set; }
        public string Name { get; set; }
        public int Score { get; set; }

        public Entry(int rank, string name, int score)
        {
            Rank = rank;
            Name = name;
            Score = score;
        }
    }
}
