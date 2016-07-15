using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AttribDemo.Classes;
using System.IO;

namespace AttribDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Helper helper = new Helper();
                string message = "All Code reviews are approved!";
                bool answer = helper.AnalayzeAssembly(Assembly.GetExecutingAssembly());

                if (!answer)
                {
                    message = "Not " + message;
                }

                Console.WriteLine(message);
            }
            catch (ReflectionTypeLoadException e)
            {
                Console.WriteLine($"Could not acces assembly types: {e.Message}");
            }

            Console.ReadLine();
        }
    }
}
