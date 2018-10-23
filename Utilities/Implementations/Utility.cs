﻿namespace Utilities.Implementations
{
    using System;
    using System.IO;
    using Interfaces;

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
                while (!Console.KeyAvailable) { }
            } while (Console.ReadKey(true).Key != ConsoleKey.Escape);
        }
    }
}