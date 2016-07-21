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
            var query = (from type in xml.Descendants("Type")
                        let typeName = (string)type.Attribute("FullName")
                        orderby typeName
                        where !type.Element("Properties").HasElements
                        select typeName).ToList();

            Console.WriteLine($"There are {query.Count} Types without properties");

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

        public void Statistics(XElement xml)
        {
            int numberOfProps = xml.Descendants("Property").Count();
            var typeAsParameters = (from parameter in xml.Descendants("Parameter")
                                    group parameter by parameter.Attribute("Type").Value into typeToParamsArray
                                    let item = new
                                    {
                                        TypeName = typeToParamsArray.Key,
                                        Count = typeToParamsArray.Count()
                                    }
                                    orderby item.Count descending
                                    select item).FirstOrDefault();

            Console.WriteLine($"There are {numberOfProps} properties in mscorlib.dll");
            Console.WriteLine($"The most common type as parameter is {typeAsParameters.TypeName} with {typeAsParameters.Count} occurences");
        }

        public void SortingTypes(XElement xml)
        {
            var query = new XElement("Types", 
                        from type in xml.Descendants("Type")
                        let numberOfMethods = type.Descendants("Method").ToList().Count
                        let numberOfProps = type.Descendants("Property").ToList().Count
                        orderby numberOfMethods descending
                        select new XElement("Type",
                        new XAttribute("Name", type.Attribute("FullName").Value),
                        new XAttribute("MethodsCount", numberOfMethods),
                        new XAttribute("PropertiesCount", numberOfProps)));

            Console.WriteLine(query);
        }

        public void GroupTypes(XElement xml)
        {
            var query = (
                 from type in xml.Descendants("Type")
                 let numberOfMethods = type.Descendants("Method").ToList().Count
                 group type by numberOfMethods into methodsCountGroup
                 orderby methodsCountGroup.Key descending
                 select methodsCountGroup)
                .Select(group => new
                {
                    Key = group.Key,
                    List = group.OrderBy(type => type.Attribute("FullName").Value)
                });

            foreach (var item in query.Where(item => item.Key == 0))
            {
                foreach (var type in item.List)
                {
                    Console.WriteLine(type);
                }
            }
        }
    }
}
