using System;
using System.IO;
using Utilities.Interfaces;

namespace Utilities.Implementations
{
    public class Utility : IUtility
    {
        string IUtility.GetUserProfile()
        {
            return Path.GetFileName(Environment.GetEnvironmentVariable("USERPROFILE"));
        }

        void IUtility.WaitUserInput()
        {
            Console.WriteLine("Pres any key to leave...");
            do
            {
                while (!Console.KeyAvailable)
                {
                }
            } while (Console.ReadKey(true).Key != ConsoleKey.Escape);
        }
    }
}