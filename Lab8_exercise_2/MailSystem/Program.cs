using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MailSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            MailManager manager = new MailManager();
            Timer timer = null;

            manager.MailArrived += HandleMailArrived;
            Console.WriteLine("Start Mailings!");
            manager.SimulateMailArrived();

            timer = new Timer((object state) => 
            {
                manager.SimulateMailArrived();
            }, 
            null, 0, 1000);

            Thread.Sleep(10000);
            //Console.ReadLine();
        }

        private static void HandleMailArrived(object sender, MailArrivedEventArgs eventArgs)
        {
            if (eventArgs != MailArrivedEventArgs.Empty)
            {
                Console.WriteLine("New Mail:{3} FROM: {0}{3} {1}{3} {2}{3}",
                    sender.GetType().Name,
                    eventArgs.Title,
                    eventArgs.Body,
                    System.Environment.NewLine);
            }
        }
    }
}
