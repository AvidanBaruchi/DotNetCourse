using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomAwaiter
{
    class Helper
    {
        public async void TestIntAwaiter()
        {
            Console.WriteLine("Waiting for Integer");
            await 3000;
            Console.WriteLine("Done Waiting for Integer");
        }

        public async void TestProcessAwaiter()
        {
            try
            {
                Console.WriteLine("Waiting for Process");
                await Process.Start("notepad");
                Console.WriteLine("Done Waiting for Process");
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine($"File not Found: {e.Message}");
            }
            catch (Win32Exception e)
            {
                Console.WriteLine($"Windows Error: {e.Message}");
            }
            catch (NullReferenceException e)
            {
                Console.WriteLine($"No Process Found: {e.Message}");
            }
        }
    }
}
