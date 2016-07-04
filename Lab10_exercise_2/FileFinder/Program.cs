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
            ICollection<FileInfo> foundFiles = null;

            if(args.Length != 2)
            {
                Console.WriteLine("Please supply directory path and a search string and try again!");
                return;
            }

            if(Directory.Exists(args[0]))
            {
                foundFiles = SearchFilesInDirectory(args[0], args[1]);

                foreach (FileInfo file in foundFiles)
                {
                    Console.WriteLine("Name: {0}, Length: {1}", file.Name, file.Length);
                }
            }
            else
            {
                Console.WriteLine("directory do not exist!");
            }

            Console.WriteLine("done!");
            Console.ReadLine();
        }

        private static ICollection<FileInfo> SearchFilesInDirectory(string directory, string fileName)
        {
            var foundPaths = new List<FileInfo>();
            ICollection<FileInfo> subDirectoryPaths = new List<FileInfo>();

            foreach (string path in Directory.GetFiles(directory, "*" + fileName + "*"))
            {
                foundPaths.Add(new FileInfo(path));
            }

            foreach (string directoryPath in Directory.GetDirectories(directory))
            {
                try
                {
                    subDirectoryPaths = SearchFilesInDirectory(directoryPath, fileName);
                    foundPaths.AddRange(subDirectoryPaths);
                }
                catch (UnauthorizedAccessException e)
                {
                    Console.WriteLine("[Error] Unauthorized Access: " + e.Message);
                }   
            }

            return foundPaths;
        }
    }
}
