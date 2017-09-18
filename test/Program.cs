﻿using System;
using System.Collections.Generic;
using System.Linq;
using test.Models;
using static test.Utilities.Utilities;
using static test.CSharpTwo.CSharpTwo;
using static test.CSharpSeven.CSharpSeven;
using static test.DataAccess.DataAccess;

namespace test
{
    internal class Program
    {
        private static void Main()
        {
            #region ClassesExamples
            void UtilitiesExamples()
            {
                Console.WriteLine(GetUserProfile());
                Console.WriteLine($"\"{DateTime.Now}\"");
            }
            void CSharpTwoExamples()
            {
                IteratorEvenNumbers(1, 10);
                IteratorDaysOfTheWeek();
            }
            #endregion
            void CSharpSevenExamples()
            {
                #region Passed
                BinaryLiterals();
                LocalFunctions();
                LocalFunctionsRecursion();
                TupleValueTupleReturn();
                TupleDemo();
                DeconstructorTuple();
                Deconstructor();
                IsExpressionsWithPatterns();
                RefReturns();
                OutVariable();
                #endregion
            }

            void ConsoleLogThis()
            {
                #region Cleared
                UtilitiesExamples();
                CSharpTwoExamples();
                CSharpSevenExamples();
                AddBlogToDb();
                Console.Clear();
                #endregion
                var bloggers = new List<Blogger>
                {
                    new Blogger{ Firstname = "First", Surname = "Surname", Age = 40},
                    new Blogger{ Firstname = "Second", Surname = "Surname", Age = 35},
                    new Blogger{ Firstname = "Third", Surname = "Surname", Age = 40},
                    new Blogger{ Firstname = "Fourth", Surname = "Sirname", Age = 25},
                    new Blogger{ Firstname = "Fifth", Surname = "Surname", Age = 25},
                    new Blogger{ Firstname = "Sixth", Surname = "Sirname", Age = 20}
                };

                var groupQuery = bloggers.GroupBy(b => b.Age >= 30);

                foreach (var grouping in groupQuery)
                {
                    Console.WriteLine($"{grouping.Key}");
                    foreach (var blogger in grouping)
                    {
                        Console.WriteLine($" {blogger.Firstname} {blogger.Surname}");
                    }
                }

                WaitUserInput();
            }
            ConsoleLogThis();
        }
    }
}
