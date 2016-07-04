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

            if (args.Length == 2)
            {
                if (Directory.Exists(args[0]))
                {
                    foundFiles = SearchFilesInDirectory(args[0], args[1]);
                    Console.WriteLine("done!");

                    foreach (FileInfo file in foundFiles)
                    {
                        Console.WriteLine($"Name: {file.Name}, Length: {file.Length}");
                    }
                }
                else
                {
                    Console.WriteLine("directory do not exist!");
                } 
            }
            else
            {
                Console.WriteLine("Please supply directory path and a search string and try again!");
            }

            Console.ReadLine();
        }

        private static ICollection<FileInfo> SearchFilesInDirectory(string directory, string fileName)
        {
            var foundPaths = new List<FileInfo>();
            ICollection<FileInfo> subDirectoryPaths = new List<FileInfo>();

            foreach (string path in Directory.EnumerateFiles(directory, "*" + fileName + "*"))
            {
                try
                {
                    foundPaths.Add(new FileInfo(path));
                }
                catch (UnauthorizedAccessException e)
                {
                    Console.WriteLine("[Error] Unauthorized Access: " + e.Message);
                }
                catch(PathTooLongException e)
                {
                    Console.WriteLine("[Error] Path Too Long: " + e.Message);
                }
            }

            foreach (string directoryPath in Directory.EnumerateDirectories(directory))
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
                catch (PathTooLongException e)
                {
                    Console.WriteLine("[Error] Path Too Long: " + e.Message);
                }
            }

            return foundPaths;
        }
    }
}
