using System;
using System.Collections.Generic;

namespace test
{
    public class CSharpSeven
    {
        public static void BinaryLiterals()
        {
            Console.WriteLine(50);
            Console.WriteLine(0xFFDB);
            Console.WriteLine(0b1111_1111_1111);
            Console.WriteLine(1_000_0000);
        }

        public static void LocalFunctions()
        {
            MyNumbers();

            void MyNumbers()
            {
                BinaryLiterals();
            }

            int AddTen(int number)
            {
                return number + 10;
            }

            const int start = 6;
            Console.WriteLine($"total of 10 + {start} = " + AddTen(start));
        }

        public static void LocalFunctionsRecursion()
        {
            int myValue = 1;
            int Calc(int number) => (number < 2) ? myValue : Calc(number - 1) + Calc(number - 2);
            Console.WriteLine(Calc(8));
        }

        public IEnumerable<T> Filter<T>(IEnumerable<T> source, Func<T, bool> filter)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (filter == null) throw new ArgumentNullException(nameof(filter));

            return Iterator();

            IEnumerable<T> Iterator()
            {
                foreach (var element in source)
                {
                    if (filter(element))
                    {
                        yield return element;
                    }
                }
            }
        }
    }
}
;