using System;
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
        public static void RefReturns()
        {
            TestRefOutput();
            TestRefStorage();

            void TestRefOutput()
            {
                long total = 0;
                AddByRef(15, 10, ref total);
                Console.WriteLine(total);

                void AddByRef(long firstNumber, long secondNumber, ref long totalPointer)
                {
                    if (totalPointer <= -1) throw new ArgumentOutOfRangeException(nameof(totalPointer));
                    totalPointer = firstNumber + secondNumber;
                }
            }

            void TestRefStorage()
            {
                string[] actors = {"Ben Affleck", "Julia Roberts", "Tom Cruise", "Jennifer Lawrence"};
                const int positionInArray = 2;
                ref var actor3 = ref new CSharpSeven().FindActor(positionInArray, actors);
                Console.WriteLine($"Original actor:{actor3}");
                actor3 = "Dwayne Johnson";
                Console.WriteLine($"Replacing actor with :{actors[positionInArray]}");
            }
        }
        public ref string FindActor(int index, string[] names)
        {
            if (names.Length > 0)
                return ref names[index];
            throw new IndexOutOfRangeException($"{nameof(index)} not found.");
        }

        public static void OutVariable()
        {
            CreateName(out var newForename, out var newSurname);
            void CreateName(out string firstName, out string secondName)
            {
                firstName = "Alan";
                secondName = "Costa";
            }

            Console.WriteLine($"What's up {newForename} {newSurname}");
        }
    }
}