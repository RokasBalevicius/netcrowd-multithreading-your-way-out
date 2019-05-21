using System.Threading;

namespace NetCrowdDemo.Intro
{
    public class AtomicCounter : ICounter
    {
        private int counter = 0;

        public void Increase()
        {
            Interlocked.Increment(ref counter);
        }

        public int GetValue()
        {
            return counter;
        }
    }
}
