namespace Examples.Threads
{
    public class BankAccount : IBankAccount
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