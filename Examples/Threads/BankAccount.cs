using System;

namespace Examples.Threads
{
    public class BankAccount : IBankAccount
    {
        private readonly object _accountLock = new object();

        public BankAccount(double bal)
        {
            Balance = bal;
        }

        private double Balance { get; set; }

        public double Withdraw(double amount)
        {
            if (Balance - amount < 0)
            {
                Console.WriteLine($"Sorry {Balance} in Account");
                return Balance;
            }

            lock (_accountLock)
            {
                if (Balance >= amount)
                {
                    Console.WriteLine($"Removed {amount} and {Balance - amount} left in Account");
                    Balance -= amount;
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