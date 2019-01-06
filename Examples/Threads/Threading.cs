namespace Examples.Threads
{
    using System.Threading;

    public class Threading : IThreading
    {
        public void ThreadExample()
        {
            var myThread = new Thread(Print0);

            myThread.Start();

            Print1();

            void Print1()
            {
                for (var i = 0; i < 1000; i++)
                {
                    System.Console.Write(1);
                }
            }

            void Print0()
            {
                for (var i = 0; i < 1000; i++)
                {
                    System.Console.Write(0);
                }
            }
        }

        public void ThreadSleepExample()
        {
            var number = 1;
            for (var i = 0; i < 10; i++)
            {
                System.Console.WriteLine(number);
                Thread.Sleep(1000);
                number++;
            }
            System.Console.WriteLine("Thread ends");
        }

        public void ThreadLockExample()
        {
            var bankAccount = new BankAccount(10);
            var threads = new Thread[15];
            Thread.CurrentThread.Name = "main";

            for (var i = 0; i < 15; i++)
            {
                var t = new Thread(new ThreadStart(bankAccount.IssueWithdraw));
                t.Name = i.ToString();
                threads[i] = t;
            }

            for (var i = 0; i < 15; i++)
            {
                System.Console.WriteLine($"Thread {threads[i].Name} Alive: {threads[i].IsAlive}");
                threads[i].Start();
                System.Console.WriteLine($"Thread {threads[i].Name} Alive: {threads[i].IsAlive}");
            }

            System.Console.WriteLine($"Current Priority : {Thread.CurrentThread.Priority}");

            System.Console.WriteLine($"Thread {Thread.CurrentThread.Name} ending.");
        }

        public void ThreadArgumentsExample()
        {
            var t = new Thread(() => CountTo(15));
            t.Start();

            new Thread(() => {
                CountTo(5);
                CountTo(6);
            }).Start();

            void CountTo(int maxNum)
            {
                for (var i = 0; i < maxNum; i++)
                {
                    System.Console.WriteLine(i);
                }
            }
        }
    }
}
