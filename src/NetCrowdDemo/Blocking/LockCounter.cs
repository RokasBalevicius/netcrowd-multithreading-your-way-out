
namespace NetCrowdDemo.Blocking
{
    public class LockCounter : ICounter
    {
        private int counter = 0;

        private readonly object locker = new object();

        public void Increase()
        {
            lock (locker)
            {
               // critical section
                counter++;
            }


            /* This is what we get
            
            try
            {
                Monitor.Enter(locker);

                counter++;

            } finally
            {
                Monitor.Exit(locker);
            }

            */
        }

        public int GetValue()
        {
            return counter;
        }
    }
}
