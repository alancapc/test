using System;
using static test.Utilities;
using static test.CSharpSeven;
using static test.CSharpTwo.CSharpTwo;

namespace test
{
    internal class Program
    {
        private static void Main()
        {
            void UtilitiesExamples()
            {
                Console.WriteLine(GetUserProfile());
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
            }
            void ConsoleLogThis()
            {
                UtilitiesExamples();
                CSharpTwoExamples();
                CSharpSevenExamples();
                Console.Clear();

                WaitUserInput();
            }

            ConsoleLogThis();
        }
    }
}
;