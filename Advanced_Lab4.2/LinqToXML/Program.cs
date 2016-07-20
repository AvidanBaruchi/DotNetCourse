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
            Helper helper = new Helper();

            var xml = 
                new XElement("Types", 
                from type in Assembly.GetAssembly(typeof(string)).GetTypes()
                where type.IsPublic && type.IsClass //&& type.Name == "Timer"//&& type.Name == "Exception"
                select new XElement("Type",
                new XAttribute("FullName", type.FullName),
                new XElement("Properties",
                from prop in type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly)
                select new XElement("Property",
                new XAttribute("Name", prop.Name),
                new XAttribute("Type", prop.PropertyType))),
                new XElement("Methods", 
                from method in type.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)
                where !method.IsSpecialName
                select new XElement("Method", 
                new XAttribute("Name", method.Name),
                new XAttribute("ReturnType", method.ReturnType.Name),
                new XElement("Parameters", 
                from param in method.GetParameters()
                select new XElement("Parameter", 
                new XAttribute("Name", param.Name),
                new XAttribute("Type", param.ParameterType.FullName ?? param.ParameterType.Name)))))));

            //Console.WriteLine(xml);
            //helper.ListOfProperties(xml);
            //helper.MethodsCount(xml);
            //helper.Statistics(xml);
            //helper.SortingTypes(xml);
            helper.GroupTypes(xml);
            Console.ReadLine();
        }
    }
}
