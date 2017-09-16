using System;
using static test.Utilities;
using static test.CSharpSeven;
using static test.CSharpTwo.CSharpTwo;

namespace test
{
    internal class Program
    {
        private static void Min()
        {
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
            void CSharpSevenExamples()
            {
                BinaryLiterals();
                LocalFunctions();
                LocalFunctionsRecursion();
                TupleValueTupleReturn();
                TupleDemo();
                DeconstructorTuple();
                Deconstructor();
            }
            void ConsoleLogThis()
            {
                CSharpSevenExamples();
                CSharpTwoExamples();
                Console.Clear();

                UtilitiesExamples();
                WaitUserInput();
            }
            ConsoleLogThis();
        }
    }
}
