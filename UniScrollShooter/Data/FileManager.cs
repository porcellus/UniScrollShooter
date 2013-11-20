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

        public Map LoadMapFromFile(String name)
        {
            try
            {
                Map map = new Map();
                StreamReader reader = new StreamReader(name);
                /*for (Int32 i = 0; i < 10; ++i)
                {
                    String[] numbers = reader.ReadLine().Split(' ');
                    ScoreBoard.Add(new ScoreBoardItem(numbers[0], Int32.Parse(numbers[1]), Int32.Parse(numbers[2])));
                }*/
                reader.Close();
                return map;
            }
            catch
            {
                throw new FileIOException();
            }
        }
    }
}
