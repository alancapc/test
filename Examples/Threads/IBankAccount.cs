namespace Examples.Threads
{
    public interface IBankAccount
    {
        void IssueWithdraw();
        double Withdraw(double amount);
    }
}