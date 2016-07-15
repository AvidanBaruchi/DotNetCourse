using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttribDemo.Classes
{
    [CodeReviewAttribute(Name = "Lazy Fox", Date = "2012-01-03", IsApproved = false)]
    [CodeReviewAttribute("AvidanB", "2016-07-12", true)]
    class A
    {
        private string Hello(string message)
        {
            return "Hello " + message;
        }
    }
}
