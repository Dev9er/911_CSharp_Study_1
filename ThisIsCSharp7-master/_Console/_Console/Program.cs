using System;
using System.Threading;

namespace WaitPulse
{
    class Counter
    {
        const int LOOP_COUNT = 1000;

        readonly object lockObject;
        bool isLocked = false;

        private int countWork;
        public int Count
        {
            get { return countWork; }
        }

        public Counter()
        {
            lockObject = new object();
            countWork = 0;
        }

        public void Increase()
        {
            int loopCount = LOOP_COUNT;

            while (loopCount-- > 0)
            {
                lock (lockObject)
                {
                    //while (count > 0 || lockedCount == true)
                    //    Monitor.Wait(thisLock);
                    if (countWork > 0 || isLocked)
                    {
                        Monitor.Wait(lockObject);
                    }
                    isLocked = true;
                    countWork++;
                    isLocked = false;

                    Monitor.Pulse(lockObject);
                }
            }
        }

        public void Decrease()
        {
            int loopCount = LOOP_COUNT;

            while (loopCount-- > 0)
            {
                lock (lockObject)
                {
                    //while (count < 0 || lockedCount == true)
                    //    Monitor.Wait(thisLock);
                    if (countWork < 0 || isLocked)
                    {
                        Monitor.Wait(lockObject);
                    }
                    isLocked = true;
                    countWork--;
                    isLocked = false;

                    Monitor.Pulse(lockObject);
                }
            }
        }
    }

    class MainApp
    {
        static void Main(string[] args)
        {
            Counter counter = new Counter();

            Thread incThread = new Thread(
                new ThreadStart(counter.Increase));
            Thread decThread = new Thread(
                new ThreadStart(counter.Decrease));

            incThread.Start();
            decThread.Start();

            incThread.Join();
            decThread.Join();

            Console.WriteLine(counter.Count);
        }
    }
}