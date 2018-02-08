namespace test.Threads
{
    public interface IBankAccount
    {
        void IssueWithdraw();
        double Withdraw(double ammount);
    }
}