using System.Threading;

namespace NetCrowdDemo.NonBlocking
{
    public class NonBlockingMultiplier : ICounter
    {
        private int counter = 1;

        public void Increase()
        {
            int valueAtTheStart;
            int intendedValue;

            do
            {
                valueAtTheStart = counter; // take 'version' at the time of speculation

                intendedValue = valueAtTheStart * 2; // do actions based on that version

                // try to put new value, if fails repeat.
            } while (Interlocked.CompareExchange(ref counter, intendedValue, valueAtTheStart) != valueAtTheStart); 
        }

        public int GetValue()
        {
            return counter;
        }
    }
}
