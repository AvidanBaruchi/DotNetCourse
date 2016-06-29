using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericApp
{
    class Program
    {
        static void Main(string[] args)
        {
            MultiDictionary<int, string> dictionaries = new MultiDictionary<int, string>();

            dictionaries.Add(1, "one");
            dictionaries.Add(2, "two");
            dictionaries.Add(3, "three");
            dictionaries.Add(1, "ich");
            dictionaries.Add(2, "nee");
            dictionaries.Add(3, "sun");

            dictionaries.Add(4, "four");
            dictionaries.Add(4, "shee");

            foreach (var pair in dictionaries)
            {
                Console.WriteLine(pair);
            }

            Console.WriteLine();
            MyTest();
            Console.ReadLine();
        }

        private static void MyTest()
        {
            MultiDictionary<int, string> multi = new MultiDictionary<int, string>();

            Console.WriteLine("My Test");

            multi.Add(1, "shalom");
            multi.Add(1, "shalom"); // duplicate key-value - not inserted!
            multi.Add(2, "lalala");
            multi.Add(2, "ooooo");
            multi.Add(2, "pppp");
            multi.Add(3, "Hello World!");

            multi.Add(4, null); // null value is uniqe too, so only 1 null value is allowed for each key
            multi.Add(4, null);

            multi.Remove(1, "shalom");
            multi.Remove(1, "SHALOM"); // not exists, so not removed!

            foreach (var item in multi)
            {
                Console.WriteLine(item);
            }

            multi.Remove(2);
            //multi.Clear();
            Console.WriteLine();

            foreach (var item in multi)
            {
                Console.WriteLine(item);
            }
        }
    }
}
