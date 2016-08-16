using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading;

namespace Jobs
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var job = new Job("my job"))
            {
                job.AddProcessToJob(Process.Start("notepad"));
                job.AddProcessToJob(Process.Start("mspaint"));

                Console.ReadLine();
                job.Kill();
                job.Dispose();
                job.Dispose();

                Process testProcess = Process.Start("mspaint");
                testProcess.Kill();

                try
                {
                    if (testProcess != null)
                    {
                        job.AddProcessToJob(testProcess.Id); 
                    }
                }
                catch (ObjectDisposedException e)
                {
                    if (testProcess != null && !testProcess.HasExited)
                    {
                        testProcess.Kill();
                    }

                    Console.WriteLine($"Cant make actions on job object, object is disposed: {e.Message}");
                }
            }

            for (int i = 0; i < 20; i++)
            {
                var job = new Job("Job " + i);

            }

            Console.ReadLine();
        }
    }
}
