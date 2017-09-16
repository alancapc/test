using System;

namespace test
{
    class Actor : Performer
    {
        public string BestMovie { get; set; }
        public Int16 Year { get; set; }

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