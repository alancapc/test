using System;

namespace test
{
    internal partial class Program
    {
        private static void Main()
        {
            Console.WriteLine(Utilities.GetUserProfile()); do
            {
                while (!Console.KeyAvailable)
                {

                }
            } while (Console.ReadKey(true).Key != ConsoleKey.Escape);
        }
    }
}
;