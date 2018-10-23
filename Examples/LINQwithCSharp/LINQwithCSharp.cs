using System;
using System.Collections.Generic;
using System.Linq;
using Examples.Models;

namespace Examples.LINQwithCSharp
{
    public class LinQwithCSharp
    {
        public static readonly List<Blogger> Bloggers = new List<Blogger>
        {
            new Blogger{ Firstname = "First", Surname = "Surname", Age = 40},
            new Blogger{ Firstname = "Second", Surname = "Surname", Age = 35},
            new Blogger{ Firstname = "Third", Surname = "Surname", Age = 40},
            new Blogger{ Firstname = "Fourth", Surname = "Sirname", Age = 25},
            new Blogger{ Firstname = "Fifth", Surname = "Surname", Age = 25},
            new Blogger{ Firstname = "Sixth", Surname = "Sirname", Age = 20}
        };

        public static void LambdaGroupByExample()
        {
            foreach (var grouping in Bloggers.GroupBy(b => b.Age))
            {
                Console.WriteLine($"{grouping.Key}");
                foreach (var blogger in grouping)
                    Console.WriteLine($"{blogger.Firstname} {blogger.Surname}");
            }
        }

        public static void LambdaOrderByExample()
        {
            foreach (var blogger in Bloggers.OrderBy(b => b.Age))
                Console.WriteLine($"{blogger.Firstname} {blogger.Surname} {blogger.Age}");
        }

        public static void LambdaThenByExample()
        {
            foreach (var blogger in Bloggers.OrderBy(b => b.Surname).ThenBy(b => b.Age))
                Console.WriteLine($"{blogger.Surname} {blogger.Age} {blogger.Firstname}");
        }
    }
}
