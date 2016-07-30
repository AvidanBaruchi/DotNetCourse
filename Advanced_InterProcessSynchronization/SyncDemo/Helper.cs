using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SyncDemo
{
    public class Helper
    {
        private string _filePath = @"c:/temp/data.txt";
        private Mutex SyncFileMutex = new Mutex(false, "SyncFileMutex");

        public void WriteProcessIdToFile()
        {
            int processId = Process.GetCurrentProcess().Id;
            string text = $"Process  {"[" + processId + "]",-10}Wrote Here";
            bool isDirectoryExist = CreateDirectoryIfNotExist();
            bool hasAccessToFile = HasAccessToFile();

            if(!isDirectoryExist || !hasAccessToFile)
            {
                Console.WriteLine("Aborting");
                return;
            }

            for (int i = 0; i < 1000; i++)
            {
                try
                {
                    SyncFileMutex.WaitOne();
                    using (StreamWriter writer = File.AppendText(_filePath))
                    {
                        writer.WriteLine(text);
                    }
                }
                catch (AbandonedMutexException e)
                {
                    Console.WriteLine($"Mutex was Abandoned: {e.Message}");
                }
                finally
                {
                    SyncFileMutex.ReleaseMutex();
                }
            }

            Console.WriteLine($"Process  {"[" + processId + "]",-10}is Done!");
        }

        private bool CreateDirectoryIfNotExist()
        {
            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(_filePath));
                return true;
            }
            catch (UnauthorizedAccessException e)
            {
                Console.WriteLine($"Unauthorized Access to Directory {Path.GetDirectoryName(_filePath)}: " + e.Message);
                return false;
            }
        }

        private bool HasAccessToFile()
        {
            try
            {
                SyncFileMutex.WaitOne();
                using (var check = File.AppendText(_filePath))
                {
                    return true;
                }
            }
            catch (UnauthorizedAccessException e)
            {
                Console.WriteLine($"Unauthorized Access to File {_filePath}: " + e.Message);
                return false;
            }
            finally
            {
                SyncFileMutex.ReleaseMutex();
            }
        }
    }
}
