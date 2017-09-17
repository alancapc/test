using System;
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
                Console.Clear();
                #endregion
                AddBlogToDb( );
                WaitUserInput();
            }
            ConsoleLogThis();
        }
    }
}
