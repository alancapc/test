using System;
using System.IO;

namespace test
{
    internal partial class Program
    {
        public class Utilities
        {
            public Utilities()
            {
            }

            public static string GetUserProfile()
            {
                return Path.GetFileName(Environment.GetEnvironmentVariable("USERPROFILE"));
            }
        }
    }
}
;