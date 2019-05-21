using System.Threading;

namespace NetCrowdDemo.Spinning
{
    public class YieldCounter : ICounter
    {
        private int counter = 0;

        private static int locker = 0;

        public void Increase()
        {
            // get lock
            while(Interlocked.CompareExchange(ref locker, 1, 0) != 0)
            {
                Thread.Yield();
            }

            // critical section
            counter++;

            // release the lock
            Interlocked.Decrement(ref locker);
        }

        public int GetValue()
        {
            return counter;
        }
    }
}
