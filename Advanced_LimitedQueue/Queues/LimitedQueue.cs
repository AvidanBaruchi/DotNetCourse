using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Queues
{
    public class LimitedQueue<T> : IDisposable
    {
        private Queue<T> _queue = new Queue<T>();
        private SemaphoreSlim _semaphore = null;
        private ReaderWriterLockSlim _readerWriterLock = new ReaderWriterLockSlim();

        public LimitedQueue(int maxQueueSize)
        {
            _semaphore = new SemaphoreSlim(maxQueueSize);
        }

        public void Enque(T item)
        {
            _semaphore.Wait();
            _readerWriterLock.EnterWriteLock();
            _queue.Enqueue(item);
            _readerWriterLock.ExitWriteLock();
        }

        public T Deque()
        {
            try
            {
                _readerWriterLock.EnterWriteLock();
                var value = _queue.Dequeue();
                _semaphore.Release();
                return value;
            }
            finally
            {
                _readerWriterLock.ExitWriteLock();
            }

        }

        public int Count
        {
            get
            {
                _readerWriterLock.EnterReadLock();
                var count = _queue.Count;
                _readerWriterLock.ExitReadLock();
                return count;
            }
        }

        public void Dispose()
        {
            _semaphore.Dispose();
            _readerWriterLock.Dispose();
        }
    }
}
