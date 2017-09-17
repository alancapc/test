namespace test.CSharpSeven
{
    internal class Programmer
    {
        public string FirstName { get; }
        public string Lastname { get; }

        public Programmer(string firstName, string lastName)
        {
            FirstName = firstName;
            Lastname = lastName;
        }

        public void Deconstruct(out string firstName, out string lastName)
        {
            firstName = FirstName;
            lastName = Lastname;
        }
    }
}