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
            var job = new Job("my job");

            job.AddProcessToJob(Process.Start("notepad"));
            job.AddProcessToJob(Process.Start("calc"));

            Console.ReadLine();
            job.Kill();
            job.Kill();

            Console.ReadLine();
        }
    }
}
