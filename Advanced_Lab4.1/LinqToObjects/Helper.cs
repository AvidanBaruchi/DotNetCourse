﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LinqToObjects
{
    class Helper
    {
        public void interfaces()
        {
            Assembly mscorlib = Assembly.GetAssembly(typeof(int));

            try
            {
                var interfacesInfo = from type in mscorlib.GetTypes()
                                     where type.IsInterface && type.IsPublic
                                     orderby type.Name
                                     select new
                                     {
                                         Type = type.Name,
                                         methodsNumber = type.GetMethods().Length
                                     };
                foreach (var item in interfacesInfo)
                {
                    Console.WriteLine($"Interface Name: {item.Type} Has {item.methodsNumber} methods");
                }
            }
            catch (ReflectionTypeLoadException e)
            {
                Console.WriteLine("Cannot load assembly: " + e.Message);
            }
        }

        public void processes()
        {
            var processesInfo = from process in Process.GetProcesses()
                            where isSystem(process) && process.Threads.Count < 5
                            orderby process.Id ascending
                            group process by process.BasePriority;

            foreach (var priorityGroup in processesInfo)
            {
                Console.WriteLine("Base Priority: " + priorityGroup.Key);

                foreach (var process in priorityGroup)
                {
                    Console.WriteLine($"  Name: {process.ProcessName}, ID: {process.Id}, Start Time: {process.StartTime}");
                }
            }
                
        }

        private bool isSystem(Process process)
        {
            try
            {
                var starttime = process.StartTime;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void threads()
        {

        }
    }
}