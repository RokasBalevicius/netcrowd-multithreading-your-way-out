using System.Threading;

namespace NetCrowdDemo.Spinning
{
    public class SpinWaitCounter : ICounter
    {
        private int counter = 0;

        private static int locker = 0;

        public void Increase()
        {
            // get lock
            while(Interlocked.CompareExchange(ref locker, 1, 0) != 0)
            {
                Thread.SpinWait(1000 * 1000 * 10);
            }

            // critical section
            counter++;
             
            // release the lock (bad)
            //locker = 0;

            // release the lock (good)
            Interlocked.Decrement(ref locker);
        }

        public int GetValue()
        {
            return counter;
        }
    }
}
