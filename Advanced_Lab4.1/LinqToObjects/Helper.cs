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
                var interfacesInfo = 
                    from type in mscorlib.GetTypes()
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
            try
            {
                var processesInfo = from process in Process.GetProcesses()
                                    where IsSystem(process) && process.Threads.Count < 5
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
            catch (PlatformNotSupportedException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (SystemException e)
            {
                Console.WriteLine(e.Message);
            }
                
        }

        public void threads()
        {
            try
            {
                var threadsCount =
                        (from process in Process.GetProcesses()
                         select process.Threads.Count)
                        .Sum();

                Console.WriteLine("Total Number of Theards in the System: " + threadsCount);
            }
            catch (PlatformNotSupportedException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (SystemException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void CopyToMethodTest()
        {
            TestClass t1 = new TestClass(1);
            TestClass t2 = new TestClass(2);
            AnotherTestClass atc1 = new AnotherTestClass(1);
            AnotherTestClass atc2 = new AnotherTestClass(2);

            t1.GetAndSet = 1;
            t2.GetAndSet = 2;

            Console.WriteLine($"t2: {t2}");
            t1.CopyTo(t2);
            Console.WriteLine($"t2: {t2}");

            Console.WriteLine("from atc1 to atc2 - only ReadOnlyProp should update");
            Console.WriteLine($"atc2: {atc2}");
            atc1.CopyTo(atc2);
            Console.WriteLine($"atc2: {atc2}");

            Console.WriteLine("Mixing the two classes from t1 to atc2: only ReadOnlyProp should update");
            atc2 = new AnotherTestClass(2);
            Console.WriteLine($"atc2: {atc2}");
            t1.CopyTo(atc2);
            Console.WriteLine($"atc2: {atc2}");

            Console.WriteLine("Mixing the two classes from atc2 to t1: both ReadOnlyProp and GetSet should update");
            atc2 = new AnotherTestClass(2);
            Console.WriteLine($"t1: {t1}");
            atc2.CopyTo(t1);
            Console.WriteLine($"t1: {t1}");
        }

        private bool IsSystem(Process process)
        {
            try
            {
                var startTime = process.StartTime;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
