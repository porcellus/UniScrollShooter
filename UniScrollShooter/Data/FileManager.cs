using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Data
{
    public class FileIOException : Exception
    {
        public FileIOException() { }
    }

    public class FileManager
    {
        public FileManager() { }

        public HighscoreList LoadHighscoreList()
        {
            HighscoreList list = new HighscoreList();
            list.LoadHighscores();
            return list;
        }

        public bool IsNewRecord(int score)
        {
            HighscoreList list = new HighscoreList();
            list.LoadHighscores();
            return list.GetEntries().Min(x => x.Score) < score;
        }


        public void SaveHighscoreList(string name, int score)
        {
            HighscoreList list = new HighscoreList();
            list.LoadHighscores();
            var ordered = list.GetEntries().OrderBy(x => x.Score);
            foreach (Entry item in list.GetEntries())
            {
                if (item.Score < score)
                {
                    item.Name = name;
                    item.Score = score;
                    break;
                }
            }
            list.SaveHighscores();

        }


    }
}
