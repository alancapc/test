using System;
using Utilities;
using test.Threads;
using Microsoft.Extensions.DependencyInjection;
using static test.CSharpTwo.CSharpTwo;
using static test.CSharpSeven.CSharpSeven;
using static test.DataAccess.DataAccess;
using static test.LINQwithCSharp.LinQwithCSharp;

namespace test
{
    public class Program
    {
        private static void Main()
        {
            var services = new ServiceCollection();
            services.AddInternalServices();
            services.AddUtilitiesConnector();

            var provider = services.BuildServiceProvider();
            var utility = provider.GetService<IUtility>();
            var threading = provider.GetService<IThreading>();

            #region ClassesExamples
            void UtilitiesExamples()
            {
                Console.WriteLine(utility.GetUserProfile());
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
                threading.ThreadExample();
                threading.ThreadSleepExample();
                threading.ThreadLockExample();
                threading.ThreadArgumentsExample();
            }
            // Step1: Declare TopicExamples() method here
            void ConsoleLogThis()
            {
                #region Cleared
                // Step3: Move TopicExamples() method into cleared region (i.e. below)
                UtilitiesExamples();
                CSharpTwoExamples();
                CSharpSevenExamples();
                //AddBlogToDb();
                //LinQwithCSharp();
                Threading();
                Console.Clear();
                #endregion

                // Step2: Call TopicExamples() method below

                utility.WaitUserInput();
            }
            ConsoleLogThis();
        }
    }
}
