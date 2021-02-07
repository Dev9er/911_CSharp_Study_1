﻿
/* ================= 6.6.3 System.Threading.Interlocked ================= */

using System;
using System.Threading;

class Program
{
    static void Main(string[] args)
    {
        MyData data = new MyData();

        Thread t1 = new Thread(threadFunc);
        Thread t2 = new Thread(threadFunc);

        t1.Start(data);
        t2.Start(data);

        t1.Join();
        t2.Join();

        Console.WriteLine(data.Number);
    }

    static void threadFunc(object inst)
    {
        MyData data = inst as MyData;

        for (int i = 0; i < 1000000; i++)
        {
            //lock (data)
            {
                data.Increment();
            }
        }
    }
}

class MyData
{
    int number = 0;

    public int Number { get { return number; } }

    public void Increment()
    {
        Interlocked.Increment(ref number);
    }
}

