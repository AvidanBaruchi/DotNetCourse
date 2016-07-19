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
            var typesAsParameters = (from parameter in xml.Descendants("Parameter")
                                    group parameter by parameter.Attribute("Type").Value into typeToParamsArray
                                    let item = new
                                    {
                                        TypeName = typeToParamsArray.Key,
                                        Count = typeToParamsArray.Count()
                                    }
                                    orderby item.Count descending
                                    select item).FirstOrDefault();



           //from param in xml.Descendants("Parameter")
           //let typeName = param.Attribute("Type").Value
           //group param by typeName into types
           //let item = new
           //{
           //    Name = types.Key,
           //    Count = types.Count()
           //}
           //orderby item.Count
           //select item;

           Console.WriteLine($"There are {numberOfProps} properties in mscorlib.dll");
        }
    }
}
