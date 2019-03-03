namespace Examples.CSharpSeven
{
    public class Musician : Performer
    {
        public Musician(string name, short age, string gender, string interest, string format)
        {
            Name = name;
            Age = age;
            Gender = gender;
            Interest = interest;
            Format = format;
        }

        public string Interest { get; }
        public string Format { get; }
    }
}