using System;

namespace Examples.Threads
{
    public class BankAccount : IBankAccount
    {
        private readonly object accountLock = new object();

        public BankAccount(double bal)
        {
            Balance = bal;
        }

        private double Balance { get; set; }

        public double Withdraw(double ammount)
        {
            if (Balance - ammount < 0)
            {
                Console.WriteLine($"Sorry {Balance} in Account");
                return Balance;
            }

            lock (accountLock)
            {
                if (Balance >= ammount)
                {
                    Console.WriteLine($"Removed {ammount} and {Balance - ammount} left in Account");
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