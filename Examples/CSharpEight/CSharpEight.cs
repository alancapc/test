using System;
using System.Collections.Generic;

namespace Examples
{
    class CSharpEight : ICSharpEight
    {
        public void NullableReferenceTypes()
        {
            Console.WriteLine("We now have, or should have when C# 8 is released, a nullable reference type e.g. string? yay!");

            string nonNullable = null; // Warning: Assignment of null to non-nullable reference type

            string? myNullable = null;

            M(myNullable);

            void M(string? nullable)
            {
                Console.WriteLine("Nullable intent can be expressed and you will be warned when not abiding by it.");
                Console.WriteLine(nullable.Length); // Warning: Possible null reference exception
                if (nullable != null)
                {
                    Console.WriteLine($"Nullable: {nullable}, Length: {nullable.Length}"); // Ok: You won't get here if nullable reference object is null
                    Console.WriteLine($"{nonNullable}");
                }
            }
        }

        public void AsyncStreams()
        {
            CSharpEightHasntReleasedYet();
        }

        public void RagesAndIndices()
        {
            CSharpEightHasntReleasedYet();
        }

        public void DefaultImplementationsOfInterfaceMembers()
        {
            CSharpEightHasntReleasedYet();
        }

        public void RecursivePatterns()
        {
            CSharpEightHasntReleasedYet();
        }

        public void SwitchExpressions()
        {
            CSharpEightHasntReleasedYet();
        }

        public void TargetTypedNewExpressions()
        {
            CSharpEightHasntReleasedYet();
        }

        public void CSharpEightHasntReleasedYet()
        {
            Console.WriteLine("Waiting C# 8 Release or try .net core 3.0");
            Console.WriteLine("https://blogs.msdn.microsoft.com/dotnet/2018/11/12/building-c-8-0/");
            Console.WriteLine("https://blogs.msdn.microsoft.com/dotnet/2018/12/05/take-c-8-0-for-a-spin/");
            throw new System.NotImplementedException();
        }
    }
}