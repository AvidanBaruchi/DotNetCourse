using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttribDemo.Classes
{
    [CodeReviewAttribute(Name = "JohnDoe", Date = "2000-01-01", IsApproved = true)]
    class C
    {    
        public string Hello(string message)
        {
            return "Nihau " + message;
        }
    }
}
