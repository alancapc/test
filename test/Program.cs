using System;
using static test.Utilities;
using static test.CSharpSeven;

namespace test
{
    internal class Program
    {
        private static void Main()
        {
            void CSharpSevenExample()
            {
                BinaryLiterals();
                LocalFunctions();
                LocalFunctionsRecursion();
            }

            void UtilitiesExample()
            {
                Console.WriteLine(GetUserProfile());
            }

            void ConsoleLogThis()
            {
                CSharpSevenExample();
                Console.Clear();

                UtilitiesExample();
                WaitUserInput();
            }

            ConsoleLogThis();
        }
    }
}
;