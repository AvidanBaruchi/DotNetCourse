using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personnel
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> names = null;

            try
            {
                names = ReadData(@"C:\Users\CodeValue\Documents\Visual Studio 2015\Projects\DotnetCourse\Lab10_exercise_1\Personnel\txtFile.txt");
            }
            catch(ArgumentException e)
            {
                Console.WriteLine("Argument ERROR: " + e.Message);
            }
            catch (IOException e)
            {
                Console.WriteLine("IO ERROR: " + e.Message);
            }

            if (names != null)
            {
                foreach (string name in names)
                {
                    Console.WriteLine(name);
                } 
            }

            Console.ReadLine();
        }

        private static List<string> ReadData(string path)
        {
            List<string> data = new List<string>();

            using (FileStream fileStream = new FileStream(path, FileMode.Open, 
                FileAccess.Read, FileShare.None))
            {
                StreamReader reader = new StreamReader(fileStream);
                string currentLine = null;

                while (reader.Peek() >= 0)
                {
                    currentLine = reader.ReadLine();
                    data.Add(currentLine.Trim());
                }
            }
            
            return data;
        }
    }
}
