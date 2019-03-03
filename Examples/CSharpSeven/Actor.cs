namespace Examples.CSharpSeven
{
    public class Actor : Performer
    {
        public Actor(string name, short age, string gender, string bestMovie, short year)
        {
            Name = name;
            Age = age;
            Gender = gender;
            BestMovie = bestMovie;
            Year = year;
        }

        public string BestMovie { get; set; }
        public short Year { get; set; }
    }
}