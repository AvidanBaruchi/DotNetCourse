using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Queues
{
    public class Helper
    {
        public void TestLimitedQueue()
        {
            LimitedQueue<int> queue = new LimitedQueue<int>(10);

            var randomQueueCount = new Random(Guid.NewGuid().GetHashCode()).Next(1, 100);
            Console.WriteLine($"There are {randomQueueCount} Tasks trying to Enqueue");

            for (int i = 1; i <= randomQueueCount; i++)
            {
                var item = i;
                Task.Run(() =>
                {
                    queue.Enque(item);
                    Console.WriteLine($"Added {item} to queue");
                    Console.WriteLine($"Count is: {queue.Count}");
                });
            }

            var randomDequeueCount = new Random(Guid.NewGuid().GetHashCode()).Next(1, 100);
            Console.WriteLine($"There are {randomDequeueCount} Tasks trying to Dequeue");

            for (int i = 0; i < randomDequeueCount; i++)
            {
                Task.Run(() =>
                {
                    try
                    {
                        var dequeueItem = queue.Deque();
                        Console.WriteLine($"removed {dequeueItem} from queue");

                        Console.WriteLine($"Count is: {queue.Count}");
                    }
                    catch (InvalidOperationException e)
                    {
                        Console.WriteLine(e.Message);
                    }
                });
            }
        }
    }
}
