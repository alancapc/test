namespace test.CSharpSeven
{
    internal class Musician : Performer
    {
        public string Interest { get; }
        public string Format { get; }

        public Musician(string name, short age, string gender, string interest, string format)
        {
            Name = name;
            Age = age;
            Gender = gender;
            Interest = interest;
            Format = format;
        }
    }
}