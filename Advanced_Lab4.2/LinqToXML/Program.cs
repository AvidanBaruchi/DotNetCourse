using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LinqToXML
{
    class Program
    {
        static void Main(string[] args)
        {
            var xml = from type in Assembly.GetAssembly(typeof(string)).GetTypes()
                      where type.IsPublic && type.IsClass && type.Name == "Exception"
                      select new XElement("Type",
                      new XAttribute("FullName", type.FullName),
                      new XElement("Properties",
                      from prop in type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly)
                      select new XElement("Property",
                      new XAttribute("Name", prop.Name),
                      new XAttribute("Type", prop.PropertyType))),
                      new XElement("Methods", 
                      from method in type.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)
                      select new XElement("Method", 
                      new XAttribute("Name", method.Name),
                      new XAttribute("ReturnType", method.ReturnType.Name))));

            XElement asXml = new XElement("Types", xml);
            Console.WriteLine(asXml);
            Console.ReadLine();
        }
    }
}
