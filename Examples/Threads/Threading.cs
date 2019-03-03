using System;
using System.Threading;

namespace Examples.Threads
{
    public class Threading : IThreading
    {
        public void ThreadExample()
        {
            var myThread = new Thread(Print0);

            myThread.Start();

            Print1();

            void Print1()
            {
                for (var i = 0; i < 1000; i++) Console.Write(1);
            }

            void Print0()
            {
                for (var i = 0; i < 1000; i++) Console.Write(0);
            }
        }

        public void ThreadSleepExample()
        {
            var number = 1;
            for (var i = 0; i < 10; i++)
            {
                Console.WriteLine(number);
                Thread.Sleep(1000);
                number++;
            }

            Console.WriteLine("Thread ends");
        }

        public void ThreadLockExample()
        {
            var bankAccount = new BankAccount(10);
            var threads = new Thread[15];
            Thread.CurrentThread.Name = "main";

            for (var i = 0; i < 15; i++)
            {
                var t = new Thread(bankAccount.IssueWithdraw) {Name = i.ToString()};
                threads[i] = t;
            }

            for (var i = 0; i < 15; i++)
            {
                Console.WriteLine($"Thread {threads[i].Name} Alive: {threads[i].IsAlive}");
                threads[i].Start();
                Console.WriteLine($"Thread {threads[i].Name} Alive: {threads[i].IsAlive}");
            }

            Console.WriteLine($"Current Priority : {Thread.CurrentThread.Priority}");

            Console.WriteLine($"Thread {Thread.CurrentThread.Name} ending.");
        }

        public void ThreadArgumentsExample()
        {
            var t = new Thread(() => CountTo(15));
            t.Start();

            new Thread(() =>
            {
                CountTo(5);
                CountTo(6);
            }).Start();

            void CountTo(int maxNum)
            {
                for (var i = 0; i < maxNum; i++) Console.WriteLine(i);
            }
        }
    }
}