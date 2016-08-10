using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectBuilder
{
    class Helper
    {
        public void BuildAndExecuteTasks()
        {
            //var a = Task.Run(DoSleep);


            var proj1 = Task.Run(() => { DoSleep(); });
            var proj4 = proj1.ContinueWith(DoSleep);
            var proj2 = Task.Run(() => { DoSleep(); });
            var proj3 = Task.Run(() => { DoSleep(); });

            var proj5 = Task.Factory.ContinueWhenAll(new Task[] { proj1, proj2, proj3 }, _ => { DoSleep(); });
            var proj6 = Task.Factory.ContinueWhenAll(new Task[] {proj3, proj4}, _ => { DoSleep(); });
            var proj7 = Task.Factory.ContinueWhenAll(new Task[] { proj5, proj6 }, _ => { DoSleep(); });
            var proj8 = proj5.ContinueWith(DoSleep);

            //var projects = Enumerable.Range(1, 8).Select(_ => new Task(DoSleep)).ToList();

            //foreach (var project in projects)
            //{
            //    project.Start();
            //}
        }

        private void DoSleep(Task src)
        {         
            Console.WriteLine($"{Task.CurrentId} is Running");
            Thread.Sleep(1000);
            Console.WriteLine($"{Task.CurrentId} is Done");
        }

        private void DoSleep()
        {
            DoSleep(null);
        }
    }
}
