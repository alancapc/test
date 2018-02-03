using System;
using static test.Utilities.Utilities;
using static test.CSharpTwo.CSharpTwo;
using static test.CSharpSeven.CSharpSeven;
using static test.DataAccess.DataAccess;
using static test.LINQwithCSharp.LinQwithCSharp;
using static test.Threads.Threading;

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
            void LinQwithCSharp()
            {
                LambdaThenByExample();
                LambdaOrderByExample();
                LambdaGroupByExample();
            }
            void Threading()
            {
                ThreadExample();
                ThreadSleepExample();
                ThreadLockExample();
                ThreadArgumentsExample();
            }
            // Step1: Declare TopicExamples() method here
            void ConsoleLogThis()
            {
                #region Cleared
                // Step3: Move TopicExamples() method into cleared region (i.e. below)
                UtilitiesExamples();
                CSharpTwoExamples();
                CSharpSevenExamples();
                AddBlogToDb();
                LinQwithCSharp();
                Threading();
                Console.Clear();
                #endregion

                // Step2: Call TopicExamples() method below

                WaitUserInput();
            }
            ConsoleLogThis();
        }
    }
}
