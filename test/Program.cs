using System;
using static test.Utilities;
using static test.CSharpSeven;

namespace test
{
    internal class Program
    {
        private static void Main()
        {
            #region CSharpSeven
            BinaryLiterals();
            LocalFunctions();
            LocalFunctionsTwo();
            #endregion

            #region Utilities
            Console.WriteLine(GetUserProfile());
            WaitUserInput();
            #endregion
        }
    }
}
;