using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailSystem
{
    public class MailArrivedEventArgs : EventArgs
    {
        public static new readonly MailArrivedEventArgs Empty;

        static MailArrivedEventArgs()
        {
            Empty = new MailArrivedEventArgs(string.Empty, string.Empty);
        }

        public MailArrivedEventArgs() : this(string.Empty, string.Empty) {}

        public MailArrivedEventArgs(string title, string body)
        {
            Title = title;
            Body = body;
        }

        public string Title { get; private set; }

        public string Body { get; private set; }
    }
}
