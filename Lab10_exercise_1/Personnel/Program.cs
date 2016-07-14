using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personnel
{
    class Program
    {
        static void Main(string[] args)
        {
            Helper helper = new Helper();
            List<string> names = null;

            try
            {
                names = helper.ReadData(@"txtFile.txt");
            }
            catch(ArgumentException e)
            {
                Console.WriteLine("Argument ERROR: " + e.Message);
            }
            catch (IOException e)
            {
                Console.WriteLine("IO ERROR: " + e.Message);
            }

            if (names != null)
            {
                foreach (string name in names)
                {
                    Console.WriteLine(name);
                } 
            }

            Console.ReadLine();
        }
    }
}
