using System;
using System.Collections.Generic;
using System.Threading;
using NetCrowdDemo.Blocking;
using NetCrowdDemo.Intro;
using NetCrowdDemo.NonBlocking;
using NetCrowdDemo.Spinning;

namespace NetCrowdDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            /* Intro */

            // Not thread safe
            // RunForever<NotSafeCounter>();

            // Atomic
            // RunForever<AtomicCounter>();



            /* Spin */

            // Spin wait
            // RunForever<SpinWaitCounter>();

            // Yield wait
            // RunForever<YieldCounter>();

            // Sleep wait
            // RunForever<ThreadSleepCounter>();



            /* Blocking  */

            // Mutex lock
            // RunForever<MutexCounter>();

            // Monitor lock
            // RunForever<MonitorCounter>();

            // Lock
            // RunForever<LockCounter>();



            /* Non blocking algorithms */

            // Non blocking Multiplier
             RunForever<NonBlockingMultiplierDemo>(32768);
        }

        private static void RunForever<T>(int expectedValue = 15) where T : ICounter, new()
        {
            while (true)
            {
                // create instance and run one iteration
                ICounter counter = new T();
                RunOneIterationInParallel(counter, expectedValue);

                // dispose if needed
                if (counter is IDisposable)
                {
                    ((IDisposable)counter).Dispose();
                }

                // pause for a bit, so pesky humans can keep up
                Thread.Sleep(200);
            }
        }

        private static void RunOneIterationInParallel(ICounter counter, int expectedValue)
        {
            var threads = new List<Thread>();

            // run 15 threads, one call per thread. 15 calls in total
            for (int i = 0; i < 15; i++)
            {
                var thread = new Thread(() => { Thread.Sleep(3); counter.Increase(); });
                threads.Add(thread);
            }

            // start all threads
            foreach (var thread in threads)
            {
                thread.Start();
            }

            // wait for all to finish
            foreach(var thread in threads)
            { 
                thread.Join(); 
            }

            // write value
            Console.WriteLine(counter.GetValue());

            // handle if value is bad
            if (counter.GetValue() != expectedValue)
            {
                Console.WriteLine("Got bad value");
                Console.ReadKey();
            }

        }
    }
}
