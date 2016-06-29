using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailSystem
{
    public class MailManager
    {
        public event EventHandler<MailArrivedEventArgs> MailArrived;

        protected virtual void OnMailArrived(MailArrivedEventArgs args)
        {
            if(args != null)
            {
                MailArrived.Invoke(this, args);
            }
        }

        public void SimulateMailArrived()
        {
            int number = (new Random()).Next(0, 1000);

            OnMailArrived(new MailArrivedEventArgs("Title " + number.ToString(),
                    "Body " + number.ToString()));
        }
    }
}
