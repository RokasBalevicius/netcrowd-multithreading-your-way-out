using System.Threading;
using System;

namespace NetCrowdDemo.Blocking
{
    public class MutexCounter : ICounter, IDisposable
    {
        private int counter = 0;

        private readonly Mutex mutex = new Mutex();

        public void Increase()
        {
            mutex.WaitOne();

            // critical section
            counter++;

            mutex.ReleaseMutex();
        }

        public int GetValue()
        {
            return counter;
        }

        public void Dispose()
        {
            mutex?.Dispose();
        }
    }
}
