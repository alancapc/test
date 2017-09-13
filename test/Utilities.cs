using System;
using System.IO;

namespace test
{
    public class Utilities
    {
        public static string GetUserProfile()
        {
            return Path.GetFileName(Environment.GetEnvironmentVariable("USERPROFILE"));
        }
        public static void WaitUserInput()
        {
            do
            {
                while (!Console.KeyAvailable)
                {

                }
            } while (Console.ReadKey(true).Key != ConsoleKey.Escape);
        }
    }
}
;