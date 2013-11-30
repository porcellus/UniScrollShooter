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
        public FileManager() {}

        public HighscoreList LoadHighscoreList()
        {
            HighscoreList list = new HighscoreList();
            list.LoadHighscores("..\\..\\..\\..\\UniScrollShooterContent\\highscores.list");
            return list;
        }
    }
}
