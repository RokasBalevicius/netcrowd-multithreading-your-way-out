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
                // tick count is internationally high, so that it is very visible how cpu usage spikes.
                // in reality we want to keep this value low, so that spin wait can do multiple tries during the time slice.
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
