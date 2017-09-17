namespace test.CSharpSeven
{
    internal class Actor : Performer
    {
        public string BestMovie { get; set; }
        public short Year { get; set; }

        public Actor(string name, short age, string gender, string bestMovie, short year)
        {
            Name = name;
            Age = age;
            Gender = gender;
            BestMovie = bestMovie;
            Year = year;
        }
    }
}