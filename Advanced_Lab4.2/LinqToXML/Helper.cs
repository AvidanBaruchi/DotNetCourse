using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LinqToXML
{
    class Helper
    {
        public void ListOfProperties(XElement xml)
        {
            var query = from type in xml.Descendants("Type")
                        let typeName = (string)type.Attribute("FullName")
                        orderby typeName
                        where !type.Element("Properties").HasElements
                        select typeName;

            Console.WriteLine($"There are {query.Count()} Types without properties");

            foreach (var item in query)
            {
                Console.WriteLine(item);
            }
        }

        public void MethodsCount(XElement xml)
        {
            var query = (from method in xml.Descendants("Method")
                         select method).Count();

            Console.WriteLine($"There are {query} methods!");
        }
    }
}
