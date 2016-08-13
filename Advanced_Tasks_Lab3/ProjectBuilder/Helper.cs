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
        public void BuildProjectsConcurrently()
        {
            var proj1 = Task.Run(() => { BuildProject("Project 1"); });
            var proj4 = proj1.ContinueWith(_ => BuildProject("Project 4"));
            var proj2 = Task.Run(() => { BuildProject("Project 2"); });
            var proj3 = Task.Run(() => { BuildProject("Project 3"); });

            var proj5 = Task.Factory.ContinueWhenAll(new Task[] { proj1, proj2, proj3 }, _ => BuildProject("Project 5"));
            var proj6 = Task.Factory.ContinueWhenAll(new Task[] { proj3, proj4 }, _ => BuildProject("Project 6"));
            Task.Factory.ContinueWhenAll(new Task[] { proj5, proj6 }, _ => BuildProject("Project 7"));
            proj5.ContinueWith(_ => BuildProject("Project 8"));
        }

        public void BuildProjectsSequentialy()
        {
            Task.Run(() => { BuildProject("Project 1"); })
                .ContinueWith(_ => BuildProject("Project 2"))
                .ContinueWith(_ => BuildProject("Project 3"))
                .ContinueWith(_ => BuildProject("Project 4"))
                .ContinueWith(_ => BuildProject("Project 5"))
                .ContinueWith(_ => BuildProject("Project 6"))
                .ContinueWith(_ => BuildProject("Project 7"))
                .ContinueWith(_ => BuildProject("Project 8"));
        }

        private void BuildProject(string projectName)
        {
            Console.WriteLine($"Building {projectName}..");
            Thread.Sleep(1000);
            Console.WriteLine($"Done Building {projectName}");
        }   
    }
}
