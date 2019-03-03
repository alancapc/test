namespace Examples.CSharpSeven
{
    public class Programmer
    {
        public Programmer(string firstName, string lastName)
        {
            FirstName = firstName;
            Lastname = lastName;
        }

        public string FirstName { get; }
        public string Lastname { get; }

        public void Deconstruct(out string firstName, out string lastName)
        {
            firstName = FirstName;
            lastName = Lastname;
        }
    }
}