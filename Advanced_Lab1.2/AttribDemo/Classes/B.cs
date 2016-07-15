using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttribDemo.Classes
{
    [CodeReviewAttribute(Name = "AvidanB", Date = "2016-07-12", IsApproved = true)]
    class B
    {
        public static string Hello(string message)
        {
            return "Bonjour " + message;
        }
    }
}
