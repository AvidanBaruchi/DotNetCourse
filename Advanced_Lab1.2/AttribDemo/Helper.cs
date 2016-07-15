using AttribDemo.Classes;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace AttribDemo
{
    class Helper
    {
        public bool AnalayzeAssembly(Assembly assembly)
        {
            Type[] types = assembly?.GetTypes();
            IEnumerable<CodeReviewAttribute> reviewAttributes = null;
            bool answer = true;

            if (types != null)
            {
                foreach (var type in types)
                {
                    if (type.IsDefined(typeof(CodeReviewAttribute), false))
                    {
                        reviewAttributes = type.GetCustomAttributes<CodeReviewAttribute>();

                        foreach (var attribute in reviewAttributes)
                        {
                            Console.WriteLine(
$@"Code Review Attribute in {type.FullName}:
  Name: {attribute.Name}, Date: {attribute.Date}, Is Approved: {attribute.IsApproved}");
                            answer = answer && attribute.IsApproved;
                        }
                    }
                }
            }
            else {
                throw new ArgumentNullException("assembly", "assembly must be initialized!");
            }

            return answer;
        }
    }
}
