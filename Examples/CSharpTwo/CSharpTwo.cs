using System;

namespace Examples.CSharpTwo
{
    public class CSharpTwo
    {
        public static void IteratorEvenNumbers(int first, int last)
        {
            foreach (var number in EvenSequenceIterator(first, last))
                Console.Write(number + " ");

            System.Collections.Generic.IEnumerable<int>
            EvenSequenceIterator(int firstNumber, int lastNumber)
            {
                for (var number = firstNumber; number <= lastNumber; number++)
                {
                    if (number % 2 == 0)
                        yield return number;
                }
            }
        }
        public static void IteratorDaysOfTheWeek()
        {
            var days = new DaysOfTheWeek();

            foreach (string day in days)
            {
                Console.Write(day + " ");
            }
        }
    }
}