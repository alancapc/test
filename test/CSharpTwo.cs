﻿using System;

namespace test
{
    public class CSharpTwo
    {
        public static void IteratorEvenNumbers(int first, int last)
        {
            foreach (int number in EvenSequenceIterator(first, last))
                Console.Write(number + " ");
            Console.ReadKey();

            System.Collections.Generic.IEnumerable<int>
            EvenSequenceIterator(int firstNumber, int lastNumber)
            {
                // Yield even numbers in the range.  
                for (int number = firstNumber; number <= lastNumber; number++)
                {
                    if (number % 2 == 0)
                        yield return number;
                }
            }
        }
    }
}