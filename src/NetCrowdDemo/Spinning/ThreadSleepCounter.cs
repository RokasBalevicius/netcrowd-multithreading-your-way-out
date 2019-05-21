using System.Threading;

namespace NetCrowdDemo.Spinning
{
    public class ThreadSleepCounter : ICounter
    {
        private int counter = 0;

        private static int locker = 0;

        public void Increase()
        {
            // get lock
            while(Interlocked.CompareExchange(ref locker, 1, 0) != 0)
            {
                Thread.Sleep(100);
            }

            // critical section
            counter++;

            // release the lock (good)
            Interlocked.Decrement(ref locker);
        }

        public int GetValue()
        {
            return counter;
        }
    }
}
