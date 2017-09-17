﻿using System;
using System.Collections.Generic;

namespace test.CSharpSeven
{
    public class CSharpSeven
    {
        public static void BinaryLiterals()
        {
            Console.WriteLine(50);
            Console.WriteLine(0xFFDB);
            Console.WriteLine(0b1111_1111_1111);
            Console.WriteLine(1_000_0000);
        }
        public static void LocalFunctions()
        {
            MyNumbers();

            void MyNumbers()
            {
                BinaryLiterals();
            }

            int AddTen(int number)
            {
                return number + 10;
            }

            const int start = 6;
            Console.WriteLine($"total of 10 + {start} = " + AddTen(start));
        }
        public static void LocalFunctionsRecursion()
        {
            const int myValue = 1;
            int Calc(int number) => (number < 2) ? myValue : Calc(number - 1) + Calc(number - 2);
            Console.WriteLine(Calc(8));
        }
        public IEnumerable<T> Filter<T>(IEnumerable<T> source, Func<T, bool> filter)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (filter == null) throw new ArgumentNullException(nameof(filter));

            return Iterator();

            IEnumerable<T> Iterator()
            {
                foreach (var element in source)
                {
                    if (filter(element))
                    {
                        yield return element;
                    }
                }
            }
        }
        public static void TupleValueTupleReturn()
        {
            var result = ReturnTuple();

            (int sum, int count) ReturnTuple()
            {
                return (1, 2);
            }

            Console.WriteLine($"result.sum: {result.sum}\n" +
                              $"result.count: {result.count}\n" +
                              $"result: {result}");
        }
        public static void TupleDemo()
        {
            var tupleSample = new TupleSamples();
            var person = tupleSample.GetNewTuple();
            Console.WriteLine("C# 7 Tuple - Author " +
                              $"{person.name} " +
                              $"{person.title} " +
                              $"{person.year}");
        }
        public static void DeconstructorTuple()
        {
            var tupleSample = new TupleSamples();
            var(authorName, bookTitle, pubYear) = tupleSample.GetNewTuple();

            Console.WriteLine($"Author: {authorName} \nBook: {bookTitle} Year: {pubYear}");
            
        }
        public static void Deconstructor()
        {
            var programmer = new Programmer("Alan", "Costa");
            var(firstName, lastName) = programmer;
            Console.WriteLine(firstName);
            Console.WriteLine(lastName);
        }
        public static void IsExpressionsWithPatterns()
        {
            var newActor = new Actor("Eddie Murphy", 54, "Male", "Coming to America", 2017);
            var newMusician = new Musician("Jen", 25, "Female", "Singing", "Pop");

            IsWithPattern();
            SwitchStatementWithPatterns();
            void IsWithPattern()
            {
                // ReSharper disable once PatternAlwaysOfType
                if (newActor is Performer actorCastedAsPerformer)
                {
                    Console.WriteLine($"This actor {actorCastedAsPerformer.Name} is a performer.");
                }
                Console.WriteLine("This actor is not a musician");
            }

            void SwitchStatementWithPatterns()
            {
                switch (newMusician)
                {
                    // ReSharper disable once PatternAlwaysOfType
                    case Performer performer when (performer.Age == 33):
                        Console.WriteLine($"The performer {performer.Name}");
                        break;
                    // ReSharper disable once PatternAlwaysOfType
                    case Musician musician when (musician.Age == 25):
                        Console.WriteLine($"The performer {musician.Name}");
                        break;
                    // ReSharper disable once PatternAlwaysOfType
                    case Musician _:
                        Console.WriteLine("The Musician is unknown");
                        break;
                    default:
                        Console.WriteLine("Not Found");
                        break;
                    case null:
                        throw new ArgumentNullException(nameof(newMusician));
                }

            }
        }
    }
}