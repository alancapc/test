using System.Collections;

namespace Examples.CSharpTwo
{
    public class DaysOfTheWeek : IEnumerable
    {
        private readonly string[] _days = {"Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat"};

        public IEnumerator GetEnumerator()
        {
            return _days.GetEnumerator();
        }
    }
}