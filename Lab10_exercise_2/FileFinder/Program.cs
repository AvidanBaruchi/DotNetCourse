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
            // TODO: Rearrange the recursive method: recursively access directories, then search current directory files.
            ICollection<FileInfo> foundFiles = null;

            if(args.Length != 2)
            {
                Console.WriteLine("Please supply directory path and a search string and try again!");
                return;
            }

            if(Directory.Exists(args[0]))
            {
                foundFiles = SearchDirectoryAndDisplayFiles(args[0], args[1]);

                foreach (FileInfo file in foundFiles)
                {
                    Console.WriteLine("Name: {0}, Length: {1}", file.Name, file.Length);
                }
            }
            else
            {
                Console.WriteLine("directory do not exists!");
            }

            Console.ReadLine();
        }

        private static ICollection<FileInfo> SearchDirectoryAndDisplayFiles(string directory, string name)
        {
            List<FileInfo> foundPaths = new List<FileInfo>();
            ICollection<FileInfo> subDirectoryFoundPaths = new List<FileInfo>();

            if(Directory.Exists(directory))
            {
                foreach (string subDirectory in Directory.GetDirectories(directory))
                {
                    foreach (string file in Directory.GetFiles(subDirectory))
                    {
                        if(Path.GetFileName(file).Contains(name))
                        {
                            foundPaths.Add(new FileInfo(file));
                        }
                    }

                    subDirectoryFoundPaths = SearchDirectoryAndDisplayFiles(subDirectory, name);
                    foundPaths.AddRange(subDirectoryFoundPaths);
                }
            }

            foreach (string file in Directory.GetFiles(directory, name))
            {
                foundPaths.Add(new FileInfo(file));
            }

            return foundPaths;
        }
    }
}
