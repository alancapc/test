namespace test.Threads
{
    using System.Threading;
    public class Threading
    {
        public static void ThreadExample()
        {
            Thread myThread = new Thread(Print0);

            myThread.Start();

            Print1();

            void Print1()
            {
                for (int i = 0; i < 1000; i++)
                {
                    System.Console.Write(1);
                }
            }

            void Print0()
            {
                for (int i = 0; i < 1000; i++)
                {
                    System.Console.Write(0);
                }
            }
        }

        public static void ThreadSleepExample()
        {
            var number = 1;
            for (int i = 0; i < 10; i++)
            {
                System.Console.WriteLine(number);
                Thread.Sleep(1000);
                number++;
            }
            System.Console.WriteLine("Thread ends");
        }

        public static void ThreadLockExample()
        {
            BankAccount bankAccount = new BankAccount(10);
            Thread[] threads = new Thread[15];
            Thread.CurrentThread.Name = "main";

            for (int i = 0; i < 15; i++)
            {
                Thread t = new Thread(new ThreadStart(bankAccount.IssueWithdraw));
                t.Name = i.ToString();
                threads[i] = t;
            }

            for (int i = 0; i < 15; i++)
            {
                System.Console.WriteLine($"Thread {threads[i].Name} Alive: {threads[i].IsAlive}");
                threads[i].Start();
                System.Console.WriteLine($"Thread {threads[i].Name} Alive: {threads[i].IsAlive}");
            }

            System.Console.WriteLine($"Current Priority : {Thread.CurrentThread.Priority}");

            System.Console.WriteLine($"Thread {Thread.CurrentThread.Name} ending.");
        }

        public static void ThreadArgumentsExample()
        {
            Thread t = new Thread(() => CountTo(15));
            t.Start();

            new Thread(() => {
                CountTo(5);
                CountTo(6);
            }).Start();

            void CountTo(int maxNum)
            {
                for (int i = 0; i < maxNum; i++)
                {
                    System.Console.WriteLine(i);
                }
            }
        }
    }

    public class BankAccount
    {
        private object accountLock = new object();
        double Balance { get; set; }

        public BankAccount(double bal)
        {
            Balance = bal;
        }
        public double Withdraw(double ammount)
        {
            if ((Balance - ammount) < 0)
            {
                System.Console.WriteLine($"Sorry {Balance} in Account");
                return Balance;
            }

            lock (accountLock)
            {
                if (Balance >= ammount)
                {
                    System.Console.WriteLine($"Removed {ammount} and {(Balance - ammount)} left in Account");
                    Balance -= ammount;
                }
                return Balance;
            }
        }
        public void IssueWithdraw()
        {
            Withdraw(1);
        }
    }
}
