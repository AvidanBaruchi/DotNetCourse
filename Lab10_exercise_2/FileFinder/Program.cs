using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileFinder
{
    class Program
    {
        static void Main(string[] args)
        {
            ICollection<string> foundFiles = null;

            if(args.Length != 2)
            {
                Console.WriteLine("Please supply directory path and a search string and try again!");
                return;
            }

            if(Directory.Exists(args[0]))
            {
                foundFiles = SearchDirectoryAndDisplayFiles(args[0], args[1]);
            }
            else
            {
                Console.WriteLine("directory do not exists!");
            }
        }

        private static ICollection<string> SearchDirectoryAndDisplayFiles(string directory, string name)
        {
            List<string> foundPaths = new List<string>();
            ICollection<string> subDirectoryFoundPaths = new List<string>();

            if(Directory.Exists(directory))
            {
                foreach (string subDirectory in Directory.GetDirectories(directory))
                {
                    foreach (string file in Directory.GetFiles(subDirectory, name))
                    {
                        foundPaths.Add(file);
                    }

                    subDirectoryFoundPaths = SearchDirectoryAndDisplayFiles(subDirectory, name);
                    foundPaths.AddRange(subDirectoryFoundPaths);
                }
            }

            foreach (string file in Directory.GetFiles(directory, name))
            {
                foundPaths.Add(file);
            }

            return foundPaths;
        }
    }
}
