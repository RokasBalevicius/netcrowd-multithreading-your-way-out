using System.Threading;

namespace NetCrowdDemo.Blocking
{
    public class MonitorCounter : ICounter
    {
        private int counter = 0;

        private readonly object locker = new object();

        public void Increase()
        {
            Monitor.Enter(locker);

            // critical section
            counter++;

            Monitor.Exit(locker);
        }

        public int GetValue()
        {
            return counter;
        }
    }
}
