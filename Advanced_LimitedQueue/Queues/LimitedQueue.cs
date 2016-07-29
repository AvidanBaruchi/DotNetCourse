using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Queues
{
    public class LimitedQueue<T>
    {
        private Queue<T> _queue = new Queue<T>();
        private SemaphoreSlim _semaphore = null;
        private int _maxQueueSize = 0;
        private ReaderWriterLockSlim _readerWriterLock = new ReaderWriterLockSlim();

        public LimitedQueue(int maxQueueSize)
        {
            _semaphore = new SemaphoreSlim(maxQueueSize);
        }

        public void Enque(T item)
        {
            _semaphore.Wait();
            _queue.Enqueue(item);
        }

        public T Deque()
        {
            var value = _queue.Dequeue();
            _semaphore.Release();
            return value;   
        }

        public int Count
        {
            get
            {
                return _queue.Count;
            }
        }
    }
}
