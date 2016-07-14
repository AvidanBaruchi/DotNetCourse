using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personnel
{
    class Helper
    {
        public List<string> ReadData(string path)
        {
            List<string> data = new List<string>();

            using (FileStream fileStream = new FileStream(path, FileMode.Open,
                FileAccess.Read, FileShare.None))
            {
                using (StreamReader reader = new StreamReader(fileStream))
                {
                    string currentLine = null;

                    while (reader.Peek() >= 0)
                    {
                        currentLine = reader.ReadLine();
                        data.Add(currentLine.Trim());
                    }
                }
            }

            return data;
        }
    }
}
