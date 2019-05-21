using System;
using System.Threading;

namespace NetCrowdDemo.NonBlocking
{
    public class NonBlockingMultiplierDemo : ICounter
    {
        private int counter = 1;

        public void Increase()
        {
            var tries = 0; // has nothing to do with algorithm, used for demo purposes only.

            int valueAtTheStart;
            int intendedValue;

            do
            {
                valueAtTheStart = counter; // take 'version' at the time of speculation

                intendedValue = valueAtTheStart * 2; // do actions based on that version

                tries++; // nothing to do with algorithm. Captures hom many times CAS had to repeat speculation;

            // try to put new value, if fails repeat.
            } while (Interlocked.CompareExchange(ref counter, intendedValue, valueAtTheStart) != valueAtTheStart); 

            if (tries > 1) 
            { 
                Console.WriteLine($"Speculation took {tries} times instead of 1"); 
            }
        }

        public int GetValue()
        {
            return counter;
        }
    }
}
