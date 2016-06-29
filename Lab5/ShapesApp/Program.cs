using ShapeLib;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShapesApp
{
    class Program
    {
        static void Main(string[] args)
        {
            ShapeManager manager = new ShapeManager();
            StringBuilder builder = new StringBuilder();
            Rectangle r1 = new Rectangle(2, 5);
            ArrayList compareables = new ArrayList();

            //// Display All

            r1.Color = ConsoleColor.Blue;
            Console.WriteLine("DisplayAll: ");
            Console.WriteLine();
            manager.Add(r1);
            manager.Add(new Ellipse(1, 3));
            manager.Add(new Circle(3, ConsoleColor.Green));
            manager.Add(new Rectangle(4, 5, ConsoleColor.Cyan));
            manager.DisplayAll();

            //// IPersist

            Console.WriteLine();
            Console.ResetColor();
            Console.WriteLine("Persistants: (Calling Save Method on ShapeManager) {0}", System.Environment.NewLine);
            manager.Save(builder);
            Console.WriteLine(builder.ToString());

            //// IComparable

            Console.WriteLine("Testing Compareables: {0}", System.Environment.NewLine);
            compareables.Add(new Rectangle(4, 5.1));
            compareables.Add(new Rectangle(4, 5));
            compareables.Add(new Rectangle(4, 4));
            //compareables.Add(null);
            // Note: from MSDN - By definition, any object compares greater than (or follows) null, 
            // and two null references compare equal to each other.
            // i did not handled it!
            Console.WriteLine("Before Sort: ");

            foreach (var item in compareables)
            {
                Console.WriteLine(item.ToString());
            }

            Console.WriteLine("After Sort: ");
            compareables.Sort();

            foreach (var item in compareables)
            {
                Console.WriteLine(item.ToString());
            }

            Console.ReadKey();
        }
    }
}
